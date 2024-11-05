using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class PatientManagementViewModel : INotifyPropertyChanged  //always want to use changed, not changing
    //always want ViewModels to be public because it's their job for other things to ask them to do something or provide some data
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") 
        //the parameter tells maui which one of the properties should be investigated, tells what to refresh
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PatientViewModel? SelectedPatient { get; set; }

        public string? Query { get; set; }

        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                var retVal = new ObservableCollection<PatientViewModel>(
                    PatientServiceProxy
                    .Current
                    .Patients
                    .Where(p => p != null)                     //where is a saftey belt that tells it to only grab things that are not null from the patient service proxy 
                    .Where(p => p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Take(100)
                    .Select(p => new PatientViewModel(p))    //select is saying take each one of those things from where and make a new PatientViewModel out of that thing
                    );
                
                return retVal;
                //ObservableCollection rasies its own property notification events, 
            }
        }

        public void Delete()
        {
            if(SelectedPatient == null)
            {
                return;
            }
            PatientServiceProxy.Current.DeletePatient(SelectedPatient.Id);

            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));  //makes it so if the name is changed, it will produce a complation error, this is best practice

            //NotifyPropertyChanged("Patients");
            //the string "Patients" is a magic string because we don't necessarily understand what it is 
            //if public ObservableCollection<Patient> Patients wasn't there or the name was changed, Delete and Create will work on the back end, but won't work on the fornt end
            //won't get compliation error either since the string "Patients" would still be a legitimate string but would not be pointing to the correct property 
        }
    }

    
}

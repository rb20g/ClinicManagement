﻿using System;
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
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Patient? SelectedPatient { get; set; }
        public ObservableCollection<Patient> Patients
        {
            get
            {
                return new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }
    }

    
}

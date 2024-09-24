using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
namespace App.Clinic.Views;


[QueryProperty(nameof(PatientId), "patientId")]  
//a descripter, looks for a route parameter named "patientId" and it's going to grab whatever's after the equal sign and put that into the property called PatientId
public partial class PatientView : ContentPage
{ 
	public PatientView()
	{
		InitializeComponent();
		//BindingContext = new Patient();
		//this means the BindingContext of the patient view IS the PatientView
		//now BindingContext = new Patient(), the entry of a new patient will bind that entry directly to the Model and that model is going to be something we can send to the ServiceProxy
		//will only every be called one time 
	}
	public int PatientId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Patients");
    }

	private void AddClicked(object sender, EventArgs e)
	{
		var patientToAdd = BindingContext as Patient;

        if (patientToAdd != null)
		{
            PatientServiceProxy
            .Current
            .AddOrUpdatePatient(patientToAdd);
        }
        Shell.Current.GoToAsync("//Patients");
    }

	private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		//TODO task: this really needs to be in a view model, below is not best practice
		if(PatientId > 0)
		{
            BindingContext = PatientServiceProxy.Current
				.Patients.FirstOrDefault(p => p.Id == PatientId); 
			//FirstOrDefault() makes it so the first patient in the list appears in the text box when first going to add or edit on PatientView page
            //Allowed us to delegate where binding actually happened, will automatically refresh the contents of the BindingContext that will be bound by the name
        }
		else
		{
			BindingContext = new Patient();
		}

    }
}

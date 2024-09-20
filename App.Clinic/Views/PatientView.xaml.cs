using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
namespace App.Clinic.Views;


[QueryProperty(nameof(PatientId), "patientId")]
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

	private void PatientView_NavigatedTo(object sender, NavigationEventArgs e)
	{
		BindingContext = new Patient(); 
		//allowed us to delegate where binding actually happened, will automatically refresh the contents of the BindingContext that will be bound by the name

	}
}

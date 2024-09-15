using Library.Clinic.Models;
using Library.Clinic.Services;
namespace App.Clinic.Views;

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
		BindingContext = this;
		//
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Patient");
    }

	private void AddClicked(object sender, EventArgs e)
	{
		var patientToAdd = BindingContext as Patient;

        if (patientToAdd != null)
		{
            PatientServiceProxy
            .Current
            .AddPatient(BindingContext as Patient);
        }
        Shell.Current.GoToAsync("//Patient");
    }
}
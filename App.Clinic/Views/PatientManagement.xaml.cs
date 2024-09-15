
using Library.Clinic.Models;
using Library.Clinic.Services;
namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage
{
	public List<Patient> Patients
	{
		get
		{
			return PatientServiceProxy.Current.Patients;
		}
	}

	public PatientManagement()
	{
		InitializeComponent();
		//another way to modify the view is through binding, and in this case binding context
		BindingContext = this;
		//all pointers in C# are smart pointers, will give value when needed and acts a pointer when needed 
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
}
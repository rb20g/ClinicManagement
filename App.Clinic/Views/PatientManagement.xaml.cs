
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using Xamarin.Google.Crypto.Tink.Signature;
using App.Clinic.ViewModels;
namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage // after : is interfaces 
//INotifyProperyChanged will help us figure out when a property on the BindingContext has actually changed, such that we can refresh and sort it out
{

	public PatientManagement()
	{
		InitializeComponent();
		//another way to modify the view is through binding, and in this case binding context
		BindingContext = new PatientManagementViewModel();
		//all pointers in C# are smart pointers, will give value when needed and acts a pointer when needed 
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//PatientDetails");
    }

	private void PatientManagement_NavigatedTo(object sender, NavigatedToEventArgs e) 
	{
		(BindingContext as PatientManagementViewModel)?.Refresh(); 
		//want to separate view from everything else, we get to the view model by setting BindingConext as PatientManagementViewModel
		//will produce null if not a PatientManagementViewModel 
		//NotifyPropertyChanged("Patients"); this will raise the PropertyChanged event, but it wont work cause it will raise the hidden PropertyChanged event on the base class instead
	}

}

//MVVM is an archetecterial model, stands for Model View View Model, 
//Example on Mills GitHub, PracticePanther/PP.MAUI/ViewModels/ClientViewViewModel.cs
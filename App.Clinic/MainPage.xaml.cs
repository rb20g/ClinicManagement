
namespace App.Clinic
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PatientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Patients"); 
            //Current allows us access to one instance even though we don't know how to build an instance of App.Shell
            //GoToAsync() is how we are going to go from one point on the navigation or visual tree to the next point

            //feature branches should only last a day, should be named with the issue number and a descripter 

        }

    }

}

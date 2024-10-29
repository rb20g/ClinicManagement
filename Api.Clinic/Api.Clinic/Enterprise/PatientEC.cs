using Api.Clinic.Database;
using Library.Clinic.Models;

namespace Api.Clinic.Enterprise
{
    public class PatientEC   // Enterprise Controller aka., an actual web service 
    {
        public PatientEC() { }

        public IEnumerable<Patient> Patients   // NO STATIC FUNCTIONS IN ENTERPRISES, making the ability to do behaviors static
        {
            get
            {
                return FakeDatabase.Patients;
            }
            
        }

        public Patient? GetById(int id)
        {
            return FakeDatabase.Patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient? Delete(int id)
        {
            var patientToDelete = FakeDatabase.Patients.FirstOrDefault(p => p.Id == id);
            if (patientToDelete != null)
            {
                FakeDatabase.Patients.Remove(patientToDelete);
            }

            return patientToDelete;   // return deleted object for undo function
        }

        public Patient? AddOrUpdate(Patient? patient)
        {
            return FakeDatabase.AddOrUpdate(patient);
        }
    }
}

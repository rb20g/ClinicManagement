using Api.Clinic.Database;
using Library.Clinic.Models;
using Library.Clinic.DTO;

namespace Api.Clinic.Enterprise
{
    public class PatientEC   // Enterprise Controller aka., an actual web service 
    {
        public PatientEC() { }

        public IEnumerable<PatientDTO> Patients   // NO STATIC FUNCTIONS IN ENTERPRISES, making the ability to do behaviors static
        {
            get
            {
                return FakeDatabase.Patients.Take(100).Select(p => new PatientDTO(p));
            }
            
        }

        public PatientDTO? GetById(int id)
        {
            var patient = FakeDatabase
                .Patients
                .FirstOrDefault(p => p.Id == id);
            if(patient != null)
            {
                return new PatientDTO(patient);
            }
            return null;
        }

        public PatientDTO? Delete(int id)
        {
            var patientToDelete = FakeDatabase.Patients.FirstOrDefault(p => p.Id == id);
            if (patientToDelete != null)
            {
                FakeDatabase.Patients.Remove(patientToDelete);
                return new PatientDTO(patientToDelete); // return deleted object for undo function
            }

            return null;  
        }

        public Patient? AddOrUpdate(PatientDTO? patient)
        {
            if (patient == null)
            {
                return null;
            }
            return FakeDatabase.AddOrUpdatePatient(new Patient(patient));
        }
    }
}

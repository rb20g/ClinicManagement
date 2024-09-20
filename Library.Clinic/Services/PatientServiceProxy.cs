using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services   //behavior role, where the behavior for the data goes
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();

        public static PatientServiceProxy Current  //do this to deal with static 
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            instance = null;

            Patients = new List<Patient>
            {
                new Patient{Id = 1, Name = "John Doe"}
                , new Patient{Id = 2, Name = "Jane Doe"}
            };
        }
        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private List<Patient> patients;
        //field version: List<Patient> patients;
        //singleton
        public List<Patient> Patients
        {
            get
            {
                return patients;
                //make sure hitting break point
            }
            private set
            {
                if (patients != value)
                {
                    patients = value;
                }
            }
        }
        /*public PatientServiceProxy() 
        {
            patients = new List<Patient>();  //need to add patients into on master list, static is one way to do it
        }*/

        public void AddOrUpdatePatient(Patient patient) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;              //if added the patient before, will add it again, but if never added patient before, the ID is always going to be 0, actually assign it a new 0 
                isAdd = true;
            }
            if (isAdd)
            {
                patients.Add(patient);
            }
        }

        public void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);  //this grabs the first patient with this idea (and there should only be 1 since they're unique) or default
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }

        }
    }
}

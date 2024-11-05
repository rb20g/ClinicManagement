using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var patientsData = new WebRequestHandler().Get("/Patient").Result;   // all you need is /Patient because the rest is already in the WebRequestHandler

            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();

                /*new List<Patient>
            {
                new Patient{Id = 1, Name = "John Doe"}
                , new Patient{Id = 2, Name = "Jane Doe"}
            };*/
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

        private List<PatientDTO> patients;
        //field version: List<Patient> patients;
        //singleton
        public List<PatientDTO> Patients
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


        public async Task<PatientDTO?> AddOrUpdatePatient(PatientDTO patient) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            /*bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;              //if added the patient before, will add it again, but if never added patient before, the ID is always going to be 0, actually assign it a new 0 
                isAdd = true;
                //this explicitly tells when something should be added or not, we go into the function with an assumption that this is an updated
                //if we are proven wrong (that is if the Id is something 0 or less than 0) we are going to assign an Id, and at the point of 
                //assigning a Id, we know that this is an Add, and we need to put a new thing into the list
            }
            if (isAdd)  //need isAdd if statement since as we go on updates are going to get more complicated and could mess up the id setting 
            {
                Patients.Add(patient);
            }*/

            var payload = await new WebRequestHandler().Post("/patient", patient);
            var newPatient = JsonConvert.DeserializeObject<PatientDTO>(payload);
            if (newPatient != null && newPatient.Id > 0 && patient.Id == 0)
            {
                //new patient to be added to the list
                Patients.Add(newPatient);
            }
            else if (newPatient != null && patient != null && patient.Id > 0 && patient.Id == newPatient.Id)
            {
                //edit, exchange the object in the list
                var currentPatient = Patients.FirstOrDefault(p => p.Id == newPatient.Id);
                var index = Patients.Count;
                if (currentPatient != null)
                {
                    index = Patients.IndexOf(currentPatient);
                    Patients.RemoveAt(index);
                }
                Patients.Insert(index, newPatient);
            }

            return newPatient;
        }

        public async void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);

            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);   //client side

                await new WebRequestHandler().Delete($"/Patient/{id}"); //server side
            }
        }
    }
}

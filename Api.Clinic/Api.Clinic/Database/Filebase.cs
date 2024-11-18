using Library.Clinic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase     // Not the Filebase that we are going to want to use, first need to install Newtonsoft.Json
    {
        private string _root;
        private string _patientRoot;
        //private string _appointmentRoot;
        //private string _todoRoot;
        private Filebase _instance;


        public Filebase Current   //singleton, need to put lock in place so only takes in one instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()   
        {
            _root = @"C:\temp";                                                     //@ makes it a literal so \t doesn't get read as a tab
            _patientRoot = $"{_root}\\Patients";
            /*
            // if we wanted to store all of the things in our application, we would have more than three file paths
            _root = "C:\temp";                                                       //Represents the database, where all our files are going to be stored

            _appointmentRoot = $"{_root}\\Appointments";                             //Appointments that go into a calendar

                                                                                     //Root for our appointments and root for our todo, both basically a table that lives inside the root database

            _todoRoot = $"{_root}\\ToDos";                                           //todo's that go into our tasks list
            //would have one of these's roots for all our application models, physicians, patients, etc
            */
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

        public Patient AddOrUpdate(Patient patient)
        {
            //set up a new Id if one doesn't already exist
            if(patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
            }

            //go to the right place]
            string path = $"{_patientRoot}\\{patient.Id}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));    // easy for this line of code to get deadlocked because someone is going to be reading
                                                                              // every single product(i.e., patient) at the exact same time as somebody else tries to do it
                                                                              // databases track connection ID's, if two collied and deadlock each other, the deadlock graph will figure out who dies
                                                                              // tracking deadlocks is a mess, but databases do it for free

            //return the item, which now has an id
            return patient;
        }

        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Patient>
                        (File.ReadAllText(patientFile.FullName)); //Use Patient and not PatientDTO since we need the actual data 
                    if(patient != null)
                    {
                        _patients.Add(patient);
                    }
                }
                return _patients;
            }
        }



        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            if (File.Exists(type))
            {
                //blow it up
                File.Delete(id);
            }
            return true;
        }
    }


    // ------------------- FAKE MODEL FILES, REPLACE THESE WITH A REFERENCE TO YOUR MODELS -------- //
    
}

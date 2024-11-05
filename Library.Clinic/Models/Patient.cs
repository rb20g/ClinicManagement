using Library.Clinic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
    //Models are like a row in a data table, where this row will separate the name, birthday, address, and ssn of the patients into separate columns
{
    public class Patient
    {
        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        //TODO: Remove this and put it on a ViewModel instead
        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }
        public int Id { get; set; }
        //Guid puts in a hash function, don't have to check for collisions, easier to troubleshoot with in ID's 

        public string? Name { get; set; }     //a property is a member of the class that provides an abstraction to set (write) and get (read) the calue of a private field

        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }

        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
        }

        public Patient(PatientDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Birthday = p.Birthday;
            Address = p.Address;
            SSN = p.SSN;
        }
    }
}
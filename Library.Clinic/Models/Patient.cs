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
        public int Id { get; set; } 
        //Guid puts in a hash function, don't have to check for collisons, easier to troubleshoot with in ID's 
        private string? name;  //field, data member 
        public string Name     //a property is a member of the class that provides an abstraction to set (write) and get (read) the calue of a private field
        {
            get
            {
                return name ?? string.Empty;
            }

            set
            {
                name = value;
            }
        }

        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }

        public Patient()
        {
            name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
        }
    }
}
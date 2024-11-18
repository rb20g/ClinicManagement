using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public enum SortChoiceEnum       //Enums scale out pretty quickly, say if we want to sort by every element in the Patient class, we would need a Ascending and Descending for everyone of them
    {
        None,
        NameAscending,
        NameDescending
    }
}

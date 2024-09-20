using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;
//C# has no consept of scoop resolution (write the code and run it), is always object oriented 
namespace MyApp
    //namespace: lives inside that physical assembly file, groups items inside the file that do the same thing
        //virtual grouping of things inside a physical file, namespaces are pieces of assembly
    //assembly: C# projects will ultimatley compile into an assembly, is a physical file
{
    internal class Program
    {
        //Dependencies tab contains all of our libraries the program is using
        //Solution files work as makefile 
        static void Main(string[] args)
        {
            //List<string> patients = new List<string>();
            
            bool isContinue = true;
            do
            {
                Console.WriteLine("Select an option.");
                Console.WriteLine("A. Add a Patient");   //WriteLine = cout in C++
                Console.WriteLine("Q. Quit");

                string input = Console.ReadLine() ?? string.Empty;
                //ReadLine = cin, var  = auto in C++                                                                
                //Null reference exception error: Segmentation fault of C#                                                            
                //Explicit Nulllability gives indicators (like the green underline) that tells us at run time bad stuff might happen
                //not runtime safe code
                //Null coalescing: ?? at end, encapsulates an if statement, if console.readline is null give string.empty, if not null give input of ReadLine
                //var length = input?.Length ?? -1;
                //Null propagation: Expect length to be an integer (number of characters in the string) or to be null (only when input is null)
                //safest way to deal with something you know could be null (when returning a single value)

                //char choice = char.Parse(input); //how to change string to char, but only if string is single character


                if (char.TryParse(input, out char choice)) //reference parameter in C++, can only use choice inside this if line
                {
                    //Console.WriteLine($"{choice} is a char"); //fixes problem with missing space
                    switch (choice)
                    {
                        case 'a':
                        case 'A':
                            var name = Console.ReadLine();  //need to decouple the list from the application
                            var newPatient = new Patient { Name = name ?? string.Empty };
                            PatientServiceProxy.Current.AddOrUpdatePatient(newPatient);
                            break;

                        case 'd':
                        case 'D':
                            PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}.{x.Name}"));
                            int selectedPatient = int.Parse(Console.ReadLine() ?? "-1");
                            PatientServiceProxy.Current.DeletePatient(selectedPatient);
                            break;

                        case 'u':
                        case 'U':
                            break;

                        case 'r':
                        case 'R':
                            break;

                        case 'q':
                        case 'Q':
                            isContinue = false;
                            break;

                        default:
                            Console.WriteLine("That is an invalid choice!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(choice + " is not a char");
                }
            } while (isContinue);  //enumerated loop, safest way to iterate through a list, but will break if add or move a patient from patients in this loop (changes enumeration)

            foreach (var patient in PatientServiceProxy.Current.Patients)
            {
                Console.WriteLine($"{patient}");
            }

            PatientServiceProxy.Current.Patients.Where(x => x.Name.Contains("John")).ToList().ForEach(Console.WriteLine);
        }
    }
}


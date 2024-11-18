using Api.Clinic.Database;
using Api.Clinic.Enterprise;
using Library.Clinic.Models;
using Library.Clinic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger; 

        public PatientController(ILogger<PatientController> logger)   
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PatientDTO> Get()   // IEnumerable: I means this is an interface that represents an abstract class, class that has a list of functions/list/properties that aren't implemented until someone uses them 
                                            // This at it's core is a list of patients, IEnurmable contains the data of some concrete property
        {
            return new PatientEC().Patients;
        }

        [HttpGet("{id}")]  // route parameter, id must be used as an argument in GetById
        public PatientDTO? GetById(int id)
        {
            return new PatientEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public PatientDTO? Delete(int id)
        {
            return new PatientEC().Delete(id);
        }

        [HttpPost("Search")]
        public List<PatientDTO> Search([FromBody] Query q)
        {
            return new PatientEC().Search(q?.Content ?? string.Empty)?.ToList() ?? new List<PatientDTO>();
        }

        [HttpPost]
        public Patient? AddOrUpdate([FromBody] PatientDTO? patient)  //FromBody: goes to the payload of the post request and automatically deserializes whatever is in the payload and slots it into the function parameter
        {
            return new PatientEC().AddOrUpdate(patient);
        }
    }
}

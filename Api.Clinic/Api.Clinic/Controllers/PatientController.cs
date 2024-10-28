﻿
using Library.Clinic.Models;
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
        public IEnumerable<Patient> Get()
        {
            return new List<Patient>
            {
                new Patient{Id = 1, Name = "John Doe"}
                , new Patient{Id = 2, Name = "Jane Doe"}
            };
        }
    }
}

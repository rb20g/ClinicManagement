using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]          // This decorator is saying that this is not a class that is complied and linked, this is a class that sits around on a server and waits for somebody to ask it questions
    [Route("[controller]")]  // This is how to get to that class, the brackets are removing the string literal from the name of the class, which is why it is currently just "WeatherForecast"
    public class WeatherForecastController : ControllerBase   
    // ControllerBase: our class has to be inherited by ControllerBase, such that all of the inner working (like standing up and making it listen on a web socket) are done for you
    // Common Issues: 1. if you run it and the thing that you aspect is not listed, that means that the ApiController decorator is not at the top of your class, or the class has been made private
    //                2. if you don't see the correct path at the beginning of the function, that means you don't have the route portion or have done something weird on it (Ex. added a string to it, or spelling mistakes)
    //                3. if you don't see the function, that means you are missing the httpGet or httpPost above the function
    //                4. if you see a bunch of weird terms, you probably set the route of that function to something that is unexpected
    // (BIGGEST ONE)  5. if you run the application and instead of getting a webpage, you get all caps red screaming text in a giant stack trace, what that almost always means is that you have taken a route and assigned it to TWO functions 
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;     // used in every controller to stand up the web API, the component that actually allows us to send logs
                                                                         // know which service by the template, name of the class is mapped to the service

        public WeatherForecastController(ILogger<WeatherForecastController> logger)   // injection part, conversion constructor from WeatherForecastController ILogger to a WeatherForecastController,
                                                                                      // always called implicitly 
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast")]   // first thing to look at when getting an error, am I using the correct function action
        // the name ("GetWeatherForecast") is metadata that is passed in for better use of swagger, the name has no bearing on the route itself
        // without name it becomes part of the route 

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

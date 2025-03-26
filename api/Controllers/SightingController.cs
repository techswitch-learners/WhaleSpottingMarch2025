using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Database;
namespace WhaleSpottingBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class SightingController : ControllerBase
{
    private readonly ILogger<SightingController> _logger;
    public SightingController(ILogger<SightingController> logger)
    {
        // _logger = logger;
    }
        
    // POST: api/createSighting
    [HttpPost("createSighting")]
    public void CreateSighting([FromBody]CreateSightingRequest newSighting) {
        Console.WriteLine("Processing POST request from frontend");
         Ok(newSighting);
    }
}



public class CreateSightingRequest{
    public string Species { get; set; }
    public string Description { get; set; }
    // public DateTime SightingDate { get; set; }
    // public DateTime ReportDate;
    public int Quantity { get; set; } 
    public double Latitude{get;set;}
    public double Longitude{get;set;}
    public string ImgSrc { get; set; }   
}
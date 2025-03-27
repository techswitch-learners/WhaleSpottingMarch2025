using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
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
    // [EnableCors("MyAllowSpecificOrigins")]
    public IActionResult CreateSighting() {
        Console.WriteLine("Processing POST request from frontend");
         return Ok("In Create sighting endpoint.");
    }
}

public class CreateSightingRequest{
    public string Species { get; set; }
    public string Description { get; set; }
    // public DateTime SightingDate { get; set; }
    // public DateTime ReportDate;
    public string Quantity { get; set; } 
    public string Latitude{get;set;}
    public string Longitude{get;set;}
    // [JsonPropertyName("imgSrc")]
    public string ImageSource { get; set; }   
}
using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.RequestModels;

public class RegistrationModel
{
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
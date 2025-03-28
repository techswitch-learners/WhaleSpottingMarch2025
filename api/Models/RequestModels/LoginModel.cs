using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.RequestModels;

public class LoginModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
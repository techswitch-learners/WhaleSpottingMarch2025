using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.RequestModels;

public class LoginModel
{
    public string Email {get;set;}
    public string Password {get;set;}
}
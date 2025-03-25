using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpottingBackend.Models.RequestModels;

public class RegistrationModel
{
    public string Email {get;set;}
    public string Password {get;set;}
    public string Name {get;set;}
     public string UserName {get;set;}
}
using Microsoft.AspNetCore.Identity;

namespace WhaleSpottingBackend.Models.DatabaseModels;

public class User : IdentityUser
{
    public string? Name { get; set; }
}
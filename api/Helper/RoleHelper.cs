using Microsoft.AspNetCore.Identity;
public static class RoleHelper
{
    public static async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "User", "Guest"};
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
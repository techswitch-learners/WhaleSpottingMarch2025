using Microsoft.AspNetCore.Identity;
using WhaleSpottingBackend.Models.DatabaseModels;
public static class InitialDBDataSetup
{
    public static async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static async Task CreateDefaultAdminUser(UserManager<User> userManager)
    {
        var user = new User
        {
            Name = "whale_spotting",
            UserName = "whale_spotting",
            Email = "whale_spotting@gmail.com"
        };

        var AdminUsers = await userManager.GetUsersInRoleAsync("Admin");
        if (!AdminUsers.Any())
        {
            var result = await userManager.CreateAsync(user, "Whale_spotting1");
            var adminUser = await userManager.FindByNameAsync(user.UserName);
            if (adminUser != null)
            {
                result = await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Database;

class WhaleSpottingDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;
    public WhaleSpottingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:WhaleSpottingDb"]);
    }
    public DbSet<Sighting> Sighting { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Species> Species { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add roles on startup
            // using (var scope = app.Services.CreateScope())
            // {
            //     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //     await RoleHelper.EnsureRolesCreated(roleManager);
            // }
        }
}

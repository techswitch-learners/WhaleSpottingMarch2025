
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Database;

class WhaleSpottingDbContext : IdentityDbContext
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
}

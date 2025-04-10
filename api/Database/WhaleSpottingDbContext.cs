
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Helper;
using WhaleSpottingBackend.Models.DatabaseModels;

namespace WhaleSpottingBackend.Database;

public class WhaleSpottingDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;
    public WhaleSpottingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:WhaleSpottingDb"],
        x => x.UseNetTopologySuite());
    }

    public DbSet<Sighting> Sighting { get; set; }
    public DbSet<LocationModel> Location { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<SightingReview> SightingReview { get; set; }
}
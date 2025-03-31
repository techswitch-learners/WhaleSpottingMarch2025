
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:WhaleSpottingDb"]);
    }
    public DbSet<Sighting> Sighting { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Species> Species { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        Species blueWhale = new Species(1, "Blue Whale");
        Species belugaWhale = new Species(2, "Beluga Whale");

        modelBuilder.Entity<Species>().HasData(
            blueWhale,
            belugaWhale
        );

        Location blueWhaleLocation = new Location(1, 41.9028, -60.0000);
        Location belugaWhaleLocation = new Location(2, 69.169371, -174.813427);

        modelBuilder.Entity<Location>().HasData(
            blueWhaleLocation,
            belugaWhaleLocation
        );

        List<Sighting> sightings = [];
        for (int i = 1; i <= 20; i++)
        {
            sightings.Add(new Sighting(
                i,
                (i % 2) + 1,
                $"Details of Sighting {i}",
                new DateTime(2024, 3, i, 13, 21, 33, DateTimeKind.Utc),
                new DateTime(2024, 3, i + 1, 13, 21, 33, DateTimeKind.Utc),
                1,
                (i % 2) + 1,
                "https://images.google.com/"
            ));
        }

        modelBuilder.Entity<Sighting>().HasData(
            sightings
        );
    }
}

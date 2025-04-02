
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Species blueWhale = new Species(1, "Blue Whale");
        Species humpbackWhale = new Species(2, "Humpback Whale");
        Species orca = new Species(3, "Orca");
        Species spermWhale = new Species(4, "Sperm Whale");
        Species finWhale = new Species(5, "Fin Whale");
        Species minkeWhale = new Species(6, "Minke Whale");
        Species belugaWhale = new Species(7, "Beluga Whale");
        Species grayWhale = new Species(8, "Gray Whale");
        Species rightWhale = new Species(9, "Right Whale");
        Species bowheadWhale = new Species(10, "Bowhead Whale");
        Species unknown = new Species(11, "Unknown");

        modelBuilder.Entity<Species>().HasData(
            blueWhale,
            humpbackWhale,
            orca,
            spermWhale,
            finWhale,
            minkeWhale,
            belugaWhale,
            grayWhale,
            rightWhale,
            bowheadWhale,
            unknown
        );

        Location blueWhaleLocation = new Location(1, 41.9028, -60.0000);
        Location humpbackWhaleLocation = new Location(2, 57.808243, -146.412739);
        Location orcaLocation = new Location(3, 34.468535, -130.063914);
        Location spermWhaleLocation = new Location(4, 73.149204, -148.475577);
        Location finWhaleLocation = new Location(5, 36.547852, -72.010735);
        Location minkeWhaleLocation = new Location(6, 52.122217, -50.741203);
        Location belugaWhaleLocation = new Location(7, 69.169371, -174.813427);
        Location grayWhaleLocation = new Location(8, 55.443528, -138.983393);
        Location rightWhaleLocation = new Location(9, 44.420668, -56.366203);
        Location bowheadWhaleLocation = new Location(10, 83.249778, -106.815422);

        modelBuilder.Entity<Location>().HasData(
            blueWhaleLocation,
            humpbackWhaleLocation,
            orcaLocation,
            spermWhaleLocation,
            finWhaleLocation,
            minkeWhaleLocation,
            belugaWhaleLocation,
            grayWhaleLocation,
            rightWhaleLocation,
            bowheadWhaleLocation
        );

        List<Sighting> sightings = [];
        for (int i = 0; i < 20; i++)
        {
            sightings.Add(new Sighting()
            {
                Id = i + 1,
                SpeciesId = (i % 10) + 1,
                Description = $"Details of Sighting {i + 1}",
                SightingDate = new DateTime(2024, 3, i + 1, 13, 21, 33, DateTimeKind.Utc),
                ReportDate = new DateTime(2024, 3, i + 2, 13, 21, 33, DateTimeKind.Utc),
                Quantity = 1,
                LocationId = (i % 10) + 1,
                ImageSource = "https://images.google.com/"
            });
        }

        modelBuilder.Entity<Sighting>().HasData(
            sightings
        );
    }
}

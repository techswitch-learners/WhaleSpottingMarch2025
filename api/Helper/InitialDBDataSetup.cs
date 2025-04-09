using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Helper;
using WhaleSpottingBackend.Database;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
public static class InitialDBDataSetup
{
   // private static readonly WhaleSpottingDbContext _context;
  //  public static string? userId{get;set;}
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
       
    public static async Task CreateDefaultNonAdminUser(UserManager<User> userManager)
    {
        var Users = await userManager.GetUsersInRoleAsync("User");

        if (!Users.Any())
        {
            for(var noOfDefaultUsers = 1 ;noOfDefaultUsers < 3 ; noOfDefaultUsers ++)
            {
                var user = new User
                    {
                    Name = "whale_spotting_user_" + noOfDefaultUsers,
                    UserName = "whale_spotting_user_" + noOfDefaultUsers,
                    Email = "whale_spotting_user_"+noOfDefaultUsers+"@gmail.com"
                    };
       
                    var result = await userManager.CreateAsync(user, "Whale_spotting1");
                    var User = await userManager.FindByNameAsync(user.UserName);
                    if (User != null)
                    {
                        result = await userManager.AddToRoleAsync(User, "User");
                    }
            }
        } 
 
    }

    public static async Task SeedData(WhaleSpottingDbContext _context,UserManager<User> userManager)
    {
        
      
   

         if (_context.Species.Any())
            {
                return;   // DB has been seeded
            }

       // SeedData(modelBuilder);
        var speciesList = new Species[]
        {
        new Species(1, "Blue Whale"),
        new Species(2, "Humpback Whale"),
        new Species(3, "Orca"),
        new Species(4, "Sperm Whale"),
        new Species(5, "Fin Whale"),
        new Species(6, "Minke Whale"),
        new Species(7, "Beluga Whale"),
        new Species(8, "Gray Whale"),
        new Species(9, "Right Whale"),
        new Species(10, "Bowhead Whale"),
        new Species(11, "Unknown")
        };
        foreach(Species species in speciesList)
        {
            _context.Species.Add(species);
        }
        _context.SaveChanges();


   var LocationList = new LocationModel[]
        {
       
        new LocationModel(1, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(41.9028, -60.0000)),
       
        new LocationModel(2, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(57.808243, -146.412739)),
       
        new LocationModel(3, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(34.468535, -130.063914)),
     
        new LocationModel(4, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(73.149204, -148.475577)),
       
        new LocationModel(5, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(36.547852, -72.010735)),
        
        new LocationModel(6, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(52.122217, -50.741203)),
       
        new LocationModel(7, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(69.169371, -174.813427)),
    
        new LocationModel(8, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(55.443528, -138.983393)),
        
        new LocationModel(9, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(44.420668, -56.366203)),
        
        new LocationModel(10, SpatialCoordinatesHelper.ConvertLatLonToSpatialCoordinates(83.249778, -106.815422))
        };

        foreach(LocationModel location in LocationList)
        {
            _context.Location.Add(location);
        }
        _context.SaveChanges();

        List<Sighting> sightings = [];
      
       
        var User =  await userManager.FindByNameAsync("whale_spotting_user_1");
        string userId = User.Id;
        
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
                ImageSource = i % 2 == 0 ? "http://localhost:5067/images/blue-whale.jpg" : "http://localhost:5067/images/orca-whale.jpg",
               // PostedById = postedById == "" ?  "f1bcb202-0e7a-479e-b19f-c54e1297f552" : postedById
                // PostedById = "f1bcb202-0e7a-479e-b19f-c54e1297f552"
                PostedById = userId
            });
        }
        
        foreach(Sighting sighting in sightings)
        {
            _context.Sighting.Add(sighting);
        }
        _context.SaveChanges();
    }
}
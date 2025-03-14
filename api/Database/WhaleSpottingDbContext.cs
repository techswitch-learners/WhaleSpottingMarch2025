
using Microsoft.EntityFrameworkCore;

namespace WhaleSpottingBackend.Database;

class WhaleSpottingDbContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=whale_spotting;User Id=whale_spotting;Password=whale_spotting;");
    }
}

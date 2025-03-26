using Microsoft.AspNetCore.Identity;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Repositories;
using WhaleSpottingBackend.Models.DatabaseModels;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<WhaleSpottingDbContext>();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<WhaleSpottingDbContext>();

builder.Services.AddControllers();
builder.Services.AddTransient<ISightingRepository, SightingRepository>();
builder.Services.AddTransient<ISpeciesRepository, SpeciesRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        var routeTemplate = apiDesc.RelativePath;
        if (routeTemplate.Contains("api/"))
            return true;
        return false;
    });
});



var app = builder.Build();
// Create default roles and admin user if not created
 using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleHelper.EnsureRolesCreated(roleManager);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await RoleHelper.CreateDefaultAdminUser(userManager);
    }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapIdentityApi<User>();
app.MapControllers();

app.Run();

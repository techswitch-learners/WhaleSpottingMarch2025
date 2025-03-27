using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Repositories;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
     options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5174")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
        });
    // options.AddPolicy(MyAllowSpecificOrigins,
    //                       policy =>
    //                       {
    //                           policy.WithOrigins("http://localhost:5174")
    //                                             //   .AllowAnyHeader()
    //                                             .AllowAnyMethod()
    //                                             .WithHeaders(HeaderNames.ContentType, "application/json");
    //                       });
});

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
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Create default roles and admin user if not created
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await InitialDBDataSetup.EnsureRolesCreated(roleManager);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await InitialDBDataSetup.CreateDefaultAdminUser(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using WhaleSpottingBackend.Database;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Repositories;
using WhaleSpottingBackend.Services;

var builder = WebApplication.CreateBuilder(args);
var weatherServiceApiKey = builder.Configuration["WhaleSpotting:WeatherServiceApiKey"];
var weatherServiceUrl = builder.Configuration["WeatherServiceAPI:BaseUrl"];

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddDefaultPolicy(
           policy =>
           {
               policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .AllowAnyHeader();
           });
    }
});

// Add services to the container.
builder.Services.AddDbContext<WhaleSpottingDbContext>();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<WhaleSpottingDbContext>();

builder.Services.AddControllers();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddTransient<ISightingRepository, SightingRepository>();
builder.Services.AddTransient<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
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
    await InitialDBDataSetup.CreateDefaultNonAdminUser(userManager);

    var context = scope.ServiceProvider.GetRequiredService<WhaleSpottingDbContext>();
    InitialDBDataSetup.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.UseStaticFiles();

app.Run();

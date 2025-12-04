using Microsoft.EntityFrameworkCore;
using Petanque.Services.Interfaces;
using Petanque.Services.Services;
using Petanque.Storage;
using Petanque.Storage.Interfaces;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

/** Define the license we use for QuestPDF nugget package */
QuestPDF.Settings.License = LicenseType.Community;

/** Configure CORS to allow requests from the frontend application */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

/** Find all controllers and add them to the service collection */
builder.Services.AddControllers();

/** Configure the database context to use MySQL */
var connectionString = builder.Configuration.GetConnectionString("LocalMySQL");
builder.Services.AddDbContext<Id312896PetanqueContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

/** Configure logging to use console output */
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

/** Add all repositories and services to the dependency injection container */
// ------------- REPOS -------------
builder.Services.AddScoped<ISpelerRepository, SpelerRepository>();
builder.Services.AddScoped<ISpelverdelingRepository, SpelverdelingRepository>();
builder.Services.AddScoped<IAanwezigheidRepository, AanwezigheidRepository>();
builder.Services.AddScoped<ISpelRepository, SpelRepository>();
builder.Services.AddScoped<ISpeeldagRepository, SpeeldagRepository>();
builder.Services.AddScoped<IDagKlassementRepository, DagKlassementRepository>();
builder.Services.AddScoped<ISeizoenKlassementRepository, SeizoenKlassementRepository>();
builder.Services.AddScoped<ISeizoenRepository, SeizoenRepository>();

// ------------- SERVICES -------------
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IDagKlassementService, DagKlassementService>();
builder.Services.AddScoped<ISpelverdelingService, SpelverdelingService>();
builder.Services.AddScoped<IAanwezigheidService, AanwezigheidService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<ISpeeldagService, SpeeldagService>();
builder.Services.AddScoped<IDagKlassementPDFService, DagKlassementPDFService>();
builder.Services.AddScoped<ISpelverdelingPDFService, SpelverdelingPDFService>();
builder.Services.AddScoped<ISeizoensKlassementPDFService, SeizoensKlassementPDFService>();
builder.Services.AddScoped<ISeizoensService, SeizoensService>();

/** Build the app */
var app = builder.Build();

/** Apply CORS config to allow frontend access */
app.UseCors("AllowFrontend");

/** Map controllers, enable HTTPS redirection and routing */
app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();

/** Test database connection and apply migrations (DISABLED) */
/*var dbcontext = new Id312896PetanqueContext();
dbcontext.TestConnection();
dbcontext.Migration1();*/

/** Run the application */
app.Run();
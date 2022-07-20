using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Model;
using GrowthTracker.BackEnd.Orchestrator;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using Microsoft.EntityFrameworkCore;
using IngenuityNow.GrowthTracker.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GrowthTrackerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GrowthTrackerConnection")));
builder.Services.AddScoped<IUnitOfWork<IGrowthTrackerContext>, UnitOfWork<GrowthTrackerContext>>();
builder.Services.AddScoped<ICompetencyOrchestrator, CompetencyOrchestrator>();
builder.Services.AddScoped<IDataService<Competency>, DataService<Competency>>();
builder.Services.AddScoped<IGenericRepository<Competency>, GenericRepository<Competency, GrowthTrackerContext>>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//app.MapGet("/weatherforecast", async (ICompetencyOrchestrator orchestrator) =>
//{
//    var c = new AddCompetencyDto("Writing Code", "Technical Skills", "Quality & testing");
//    c.Level1Description = "Writes code with testability, readability, edge cases, and errors in mind.";
//    c.Level2Description = "Consistently writes functions that are easily testable, easily understood by other developers, and accounts for edge cases and errors. Uses docstrings effectively.";
//    c.Level3Description = "Consistently writes production-ready code that is easily testable, easily understood by other developers, and accounts for edge cases and errors. Understands when it is appropriate to leave comments, but biases towards self-documenting code.";
//    c.Level4Description = "see E3";
//    c.Level5Description = "see E3";
//    c.Level6Description = "see E3";
//    var result = await orchestrator.AddCompetencyAsync(c);
//    return result.ToHttpResult();
//})
//.WithName("GetWeatherForecast");

app.MapPost("/loadCompetencies", async (ICompetencyOrchestrator orchestrator) =>
{
    var c = new AddCompetencyDto("Writing Code", "Technical Skills", "Quality & testing");
    c.Level1Description = "Writes code with testability, readability, edge cases, and errors in mind.";
    c.Level2Description = "Consistently writes functions that are easily testable, easily understood by other developers, and accounts for edge cases and errors. Uses docstrings effectively.";
    c.Level3Description = "Consistently writes production-ready code that is easily testable, easily understood by other developers, and accounts for edge cases and errors. Understands when it is appropriate to leave comments, but biases towards self-documenting code.";
    c.Level4Description = "see E3";
    c.Level5Description = "see E3";
    c.Level6Description = "see E3";
    var result = await orchestrator.AddCompetencyAsync(c);
    return result.ToHttpResult();
})
.WithName("LoadCompetencies");

app.MapDelete("/clearCompetencies", async (ICompetencyOrchestrator orchestrator) =>
{
    var result = await orchestrator.ClearAllCompetencies();
    return result.ToHttpResult();
})
.WithName("ClearCompetencies");

app.MapPost("/loadFromExcel", async (ICompetencyOrchestrator orchestrator) =>
{
    var result = await orchestrator.LoadFromExcel();
    return result.ToHttpResult();
})
.WithName("LoadFromExcel");


app.Run();

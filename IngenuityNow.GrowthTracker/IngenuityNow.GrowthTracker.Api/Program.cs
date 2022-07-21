using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Model;
using GrowthTracker.BackEnd.Orchestrator;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using Microsoft.EntityFrameworkCore;
using IngenuityNow.GrowthTracker.Api;
using GrowthTracker.BackEnd.Mini.Data;

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

builder.Services.AddScoped<ITeamMemberOrchestrator, TeamMemberOrchestrator>();
builder.Services.AddScoped<IDataService<TeamMember>, DataService<TeamMember>>();
builder.Services.AddScoped<IGenericRepository<TeamMember>, GenericRepository<TeamMember, GrowthTrackerContext>>();

builder.Services.AddScoped<IDataService<TeamMemberCompetency>, DataService<TeamMemberCompetency>>();
builder.Services.AddScoped<IGenericRepository<TeamMemberCompetency>, GenericRepository<TeamMemberCompetency, GrowthTrackerContext>>();

builder.Services.AddScoped<ITeamMemberCompetencyDataService, TeamMemberCompetencyDataService>();

builder.Services.AddScoped<IRoleOrchestrator, RoleOrchestrator>();
builder.Services.AddScoped<IDataService<Role>, DataService<Role>>();
builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role, GrowthTrackerContext>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/addTeamMember", async (AddTeamMemberDto teamMember, ITeamMemberOrchestrator orchestrator) =>
{
    var result = await orchestrator.AddTeamMemberAsync(teamMember);
    return result.ToHttpResult();
})
.WithName("AddTeamMember");

app.MapPost("/setTeamMemberCompetencyLevel", async (SetTeamMemberCompetencyLevelDto level, ITeamMemberOrchestrator orchestrator) =>
{
    var result = await orchestrator.SetTeamMemberCompetencyLevel(level);
    return result.ToHttpResult();
})
.WithName("SetTeamMemberCompetencyLevel");

app.MapPost("/addRole", async (AddRoleDto role, IRoleOrchestrator orchestrator) =>
{
    var result = await orchestrator.AddRoleAsync(role);
    return result.ToHttpResult();
})
.WithName("AddRole");


app.MapGet("/getCompetenciesForTeamMember", async (int teamMemberId, ITeamMemberOrchestrator orchestrator) =>
{
    var result = await orchestrator.GetCompetenciesForTeamMember(teamMemberId);
    return result.ToHttpResult();
})
.WithName("GetCompetenciesForTeamMember");


app.MapPost("/addCompetency", async (AddCompetencyDto competency, ICompetencyOrchestrator orchestrator) =>
{
    var result = await orchestrator.AddCompetencyAsync(competency);
    return result.ToHttpResult();
})
.WithName("AddCompetency");

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

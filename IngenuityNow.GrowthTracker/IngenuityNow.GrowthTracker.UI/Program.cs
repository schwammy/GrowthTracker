using GrowthTracker.BackEnd.Integration.Excel;
using GrowthTracker.BackEnd.Mini.Data;
using GrowthTracker.BackEnd.Model;
using GrowthTracker.BackEnd.Orchestrator;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.GrowthTracker.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<GrowthTrackerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GrowthTrackerConnection")));
builder.Services.AddScoped<IUnitOfWork<IGrowthTrackerContext>, UnitOfWork<GrowthTrackerContext>>();
builder.Services.AddScoped<ICompetencyOrchestrator, CompetencyOrchestrator>();
builder.Services.AddScoped<IDataService<Competency>, DataService<Competency>>();
builder.Services.AddScoped<IGenericRepository<Competency>, GenericRepository<Competency, GrowthTrackerContext>>();

builder.Services.AddScoped<IExcelReader, ExcelReader>();

builder.Services.AddScoped<ITeamMemberOrchestrator, TeamMemberOrchestrator>();
builder.Services.AddScoped<ITeamMemberDataService, TeamMemberDataService>();
builder.Services.AddScoped<IDataService<TeamMember>, DataService<TeamMember>>();
builder.Services.AddScoped<IGenericRepository<TeamMember>, GenericRepository<TeamMember, GrowthTrackerContext>>();

builder.Services.AddScoped<IDataService<TeamMemberCompetency>, DataService<TeamMemberCompetency>>();
builder.Services.AddScoped<IGenericRepository<TeamMemberCompetency>, GenericRepository<TeamMemberCompetency, GrowthTrackerContext>>();

builder.Services.AddScoped<ITeamMemberCompetencyDataService, TeamMemberCompetencyDataService>();

builder.Services.AddScoped<IRoleOrchestrator, RoleOrchestrator>();
builder.Services.AddScoped<IDataService<Role>, DataService<Role>>();
builder.Services.AddScoped<IDataService<RoleCompetency>, DataService<RoleCompetency>>();
builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role, GrowthTrackerContext>>();
builder.Services.AddScoped<IGenericRepository<RoleCompetency>, GenericRepository<RoleCompetency, GrowthTrackerContext>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

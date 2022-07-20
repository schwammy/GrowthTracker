using IngenuityNow.Common.Data;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GrowthTracker.BackEnd.Model;

public interface IGrowthTrackerContext : IDbContext, IWriteableDbContext
{
    DbSet<Competency>? Competencies { get; set; }
    IDbConnection? DbConnection { get; set; }
    DbSet<TeamMemberCompetency>? TeamMemberCompetencies { get; set; }
    DbSet<TeamMember>? TeamMembers { get; set; }
}

public class GrowthTrackerContext : DbContext, IGrowthTrackerContext
{
    public DbSet<TeamMember>? TeamMembers { get; set; }
    public DbSet<Competency>? Competencies { get; set; }
    public DbSet<TeamMemberCompetency>? TeamMemberCompetencies { get; set; }

    public GrowthTrackerContext(DbContextOptions<GrowthTrackerContext> options) : base(options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
        {
            var conn = (SqlConnection)Database.GetDbConnection();
            conn.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/", "INSERT SUBSCRIPTION ID").Result;
        }
    }

    public IDbConnection? DbConnection { get; set; }
}

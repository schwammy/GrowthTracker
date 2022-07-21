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
    public DbSet<Role>? Roles { get; set; }

    public GrowthTrackerContext(DbContextOptions<GrowthTrackerContext> options) : base(options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
        {
            var conn = (SqlConnection)Database.GetDbConnection();
            conn.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/", "INSERT SUBSCRIPTION ID").Result;
        }
    }

    public IDbConnection? DbConnection { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TeamMemberCompetency>()
            .HasOne(c => c.EvaluatedBy)
            .WithOne()
            .HasForeignKey<TeamMemberCompetency>(c => c.EvaluatedById);

        modelBuilder.Entity<TeamMemberCompetency>()
            .HasOne(c => c.TeamMember)
            .WithOne()
            .HasForeignKey<TeamMemberCompetency>(c => c.TeamMemberId);


    //    modelBuilder.Entity<TeamMember>()
    //.HasMany(t => t.Competencies)
    //.WithOne(c => c.TeamMember)
    //.HasForeignKey(x => x.TeamMemberId);
    

    //    modelBuilder.Entity<TeamMember>()
    //.HasMany(t => t.Competencies)
    //.WithOne(c => c.EvaluatedBy)
    //.HasForeignKey(x => x.EvaluatedById);


    }
}

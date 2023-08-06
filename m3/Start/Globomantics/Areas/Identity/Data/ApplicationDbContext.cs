using Globomantics.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Globomantics.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ConferenceEntity> Conferences => Set<ConferenceEntity>();
    public DbSet<ProposalEntity> Proposals => Set<ProposalEntity>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Seed the conferences
        builder.Entity<ConferenceEntity>()
        .HasData(
            new ConferenceEntity { Id = 1, Name = "NDC", Location = "Oslo", Start = new DateTime(2021, 6, 14), AttendeeTotal = 2132 },
            new ConferenceEntity { Id = 2, Name = "IT/DevConnections", Location = "Las Vegas", Start = new DateTime(2021, 10, 18), AttendeeTotal = 3210 },
            new ConferenceEntity { Id = 3, Name = "Microsoft Ignite", Location = "New Orleans", Start = new DateTime(2021, 9, 2), AttendeeTotal = 2300 },
            new ConferenceEntity { Id = 4, Name = "Build", Location = "San Francisco", Start = new DateTime(2021, 5, 25), AttendeeTotal = 3200 }
        );

        // Seed the proposals
        builder.Entity<ProposalEntity>()
        .HasData(
            new ProposalEntity { Id = 1, ConferenceId = 1, Speaker = "Kevin Dockx", Title = "ASP.NET Core Tag Helpers", Approved = true },
            new ProposalEntity { Id = 2, ConferenceId = 1, Speaker = "Kevin Dockx", Title = "What's new in Entity Framework Core 5", Approved = true },
            new ProposalEntity { Id = 3, ConferenceId = 1, Speaker = "Kevin Dockx", Title = "Angular 12", Approved = false },
            new ProposalEntity { Id = 4, ConferenceId = 2, Speaker = "Kevin Dockx", Title = "ASP.NET Core Tag Helpers", Approved = true },
            new ProposalEntity { Id = 5, ConferenceId = 2, Speaker = "Kevin Dockx", Title = "What's new in Entity Framework Core 5", Approved = false },
            new ProposalEntity { Id = 6, ConferenceId = 2, Speaker = "Kevin Dockx", Title = "Angular 12", Approved = false },
            new ProposalEntity { Id = 7, ConferenceId = 3, Speaker = "Kevin Dockx", Title = "ASP.NET Core Tag Helpers", Approved = true }
        );

    }
}

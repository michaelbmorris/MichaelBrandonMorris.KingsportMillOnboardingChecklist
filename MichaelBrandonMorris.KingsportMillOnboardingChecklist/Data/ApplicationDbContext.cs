using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ActionItemAssignment> ActionItemAssignments
        {
            get;
            set;
        }

        public DbSet<ActionItem> ActionItems
        {
            get;
            set;
        }

        public DbSet<Checklist> Checklists
        {
            get;
            set;
        }

        public DbSet<Policy> Policies
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Checklist>()
                .HasOne(c => c.User)
                .WithOne(u => u.Checklist)
                .HasForeignKey<Checklist>(c => c.UserId);
        }
    }
}
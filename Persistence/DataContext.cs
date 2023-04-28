using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Activity> Activities { get; set; }  //This creates the table name
        
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x=> x.HasKey(aa=> new {aa.AppUserId, aa.ActivityId})); //this will form the primary key

            //configure the entity itself
            builder.Entity<ActivityAttendee>()
                .HasOne(u=> u.AppUser)
                .WithMany(a => a.Activities)
                .HasForeignKey(a=>a.AppUserId);

                builder.Entity<ActivityAttendee>()
                .HasOne(u=> u.Activity)
                .WithMany(a => a.Attendees)
                .HasForeignKey(a=>a.ActivityId);
        }
    }
}
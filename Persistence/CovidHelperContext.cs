using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CovidHelperContext : DbContext
    {
        public CovidHelperContext(DbContextOptions<CovidHelperContext> options): base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Questionaire> Questionaire { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Pacient> Pacients { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(p => p.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Message>()
                .HasOne(p => p.Receiver)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}

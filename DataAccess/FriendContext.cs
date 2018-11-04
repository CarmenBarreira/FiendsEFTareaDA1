using Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DataAccess
{
    public class FriendContext : DbContext
    {
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<User> Users { get; set; }

        public FriendContext() : base("name=FriendContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agenda>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Agenda>()
                .HasMany<User>(a => a.Contacts)
                .WithMany(u => u.Agendas)
                .Map(au =>
                {
                    au.MapLeftKey("AgendaRefId");
                    au.MapRightKey("UserRefId");
                    au.ToTable("AgendaUser");
                });
        }
    }
}

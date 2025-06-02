using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Tidsregistrering.DAL.Models;

namespace Tidsregistrering.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext") { }

        public DbSet<MedarbejderDAL> Medarbejdere { get; set; }
        public DbSet<AfdelingDAL> Afdelinger { get; set; }
        public DbSet<SagDAL> Sager { get; set; }
        public DbSet<TidsregistreringDAL> Tidsregistreringer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // 🚫 Undgå cascade delete mellem Sag og Tidsregistrering
            modelBuilder.Entity<TidsregistreringDAL>()
                .HasRequired(t => t.Sag)
                .WithMany(s => s.Tidsregistreringer)
                .HasForeignKey(t => t.SagId)
                .WillCascadeOnDelete(false);

            // 🚫 Undgå cascade delete mellem Medarbejder og Tidsregistrering
            modelBuilder.Entity<TidsregistreringDAL>()
                .HasRequired(t => t.Medarbejder)
                .WithMany(m => m.Tidsregistreringer)
                .HasForeignKey(t => t.MedarbejderId)
                .WillCascadeOnDelete(false);
        }

    }
}

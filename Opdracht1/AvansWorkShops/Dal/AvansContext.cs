using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Entities;

namespace AvansWorkShops.Dal
{
    public class AvansContext : DbContext
    {
        public AvansContext() : base("AvansConnection")
        {
                        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Geen meervoudsvorm voor tabelnamen en geen cascading deletes
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            // Many to Many relationship
            modelBuilder.Entity<Workshop>()
                .HasMany(c => c.Teachers).WithMany(i => i.Workshops)
                .Map(t => t.MapLeftKey("WorkshopId").MapRightKey("TeacherId").ToTable("TeacherWorkshops"));



        }


        // Toevoegen DbSet
        public DbSet<Student> Students { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}

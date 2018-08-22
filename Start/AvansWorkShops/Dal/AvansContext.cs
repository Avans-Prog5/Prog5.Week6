using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // todo: Toevoegen Configuration

        }


        // todo: Toevoegen DbSet
    }
}

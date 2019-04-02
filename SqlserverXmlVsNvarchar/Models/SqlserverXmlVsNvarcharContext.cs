using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlserverXmlVsNvarchar
{
    public class SqlserverXmlVsNvarcharContext : DbContext
    {
        public DbSet<NvarcharEntity> NvarcharEntries { get; set; }
        public DbSet<XmlEntity> XmlEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=10.0.10.81;Initial Catalog=SqlserverXmlVsNvarchar;User ID=sa;Password=***;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NvarcharEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Nvarchar", "dbo");
            });
            
            modelBuilder.Entity<XmlEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Xml", "dbo");
            });
        }
    }
}
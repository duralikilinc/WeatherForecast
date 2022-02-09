using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext():base("name=WeatherForecastContext")
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(true)
                .IsRequired();
            modelBuilder.Entity<Roles>()
                .Property(e => e.RoleName)
                .HasMaxLength(40);
            modelBuilder.Entity<Users>()
                .Property(e => e.ConfirmId)
                .IsUnicode(true)
                .IsRequired().HasMaxLength(40);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(true)
                .IsRequired();
            modelBuilder.Entity<Users>()
                .Property(e => e.FirsName)
                .IsRequired();
            modelBuilder.Entity<Users>()
                .Property(e => e.LastName)
                .IsRequired();
            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsRequired();
            modelBuilder.Entity<Users>()
                .Property(e => e.IsApproved)
                .IsRequired();
            modelBuilder.Entity<Logs>()
                .Property(e => e.Date)
                .IsRequired();
            modelBuilder.Entity<Logs>()
                .Property(e => e.Audit)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Logs>()
                .Property(e => e.Detail)
                .HasMaxLength(4000);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Lib;
using Hangfire.Annotations;

namespace Howest.Grade.Web
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AddressDetails> AddressDetails { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserTeam> ApplicationUserTeams { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectModule> ProjectModules { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        public DbSet<UserStory> UserStories { get; set; }


        public DatabaseContext([NotNullAttribute]DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //AddressDetails
            modelBuilder.Entity<AddressDetails>().Property(ad => ad.AdressLine).HasMaxLength(250);
            modelBuilder.Entity<AddressDetails>().Property(ad => ad.City).HasMaxLength(250);
            modelBuilder.Entity<AddressDetails>().Property(ad => ad.Postal).HasMaxLength(250);

            //ApplicationRole
            modelBuilder.Entity<ApplicationRole>().Property(ar => ar.Name).HasMaxLength(250);
            modelBuilder.Entity<ApplicationRole>().Property(ar => ar.Description).HasMaxLength(250);

            //ApplicationUser
            modelBuilder.Entity<ApplicationUser>().Property(au => au.LastName).HasMaxLength(250);
            modelBuilder.Entity<ApplicationUser>().Property(au => au.FirstName).HasMaxLength(250);
            modelBuilder.Entity<ApplicationUser>().Property(au => au.Email).HasMaxLength(250);
            //Company
            modelBuilder.Entity<Company>().Property(c => c.Name).HasMaxLength(250);
            modelBuilder.Entity<Company>().Property(c => c.VAT).HasMaxLength(250);

            modelBuilder.Entity<Company>().HasOne(e => e.AddressDetails)
                                         .WithOne(c => c.Company)
                                         .HasForeignKey<AddressDetails>(e => e.CompanyId)
                                         .OnDelete(DeleteBehavior.Restrict);


            //Module
            modelBuilder.Entity<Module>().Property(m => m.Name).HasMaxLength(250);
            //Project
            modelBuilder.Entity<Project>().Property(p => p.Name).HasMaxLength(250);
            modelBuilder.Entity<Project>().Property(p => p.Description).HasMaxLength(250);
            //ProjectStatus
            modelBuilder.Entity<ProjectStatus>().Property(ps => ps.Name).HasMaxLength(250);
            modelBuilder.Entity<ProjectStatus>().Property(ps => ps.Description).HasMaxLength(250);
            modelBuilder.Entity<ProjectStatus>().Property(ps => ps.Order).HasMaxLength(250);
            //Team
            modelBuilder.Entity<Team>().Property(t => t.Name).HasMaxLength(250);
            //UserStory
            modelBuilder.Entity<UserStory>().Property(us => us.Description).HasMaxLength(2500);

            //composite keys
            modelBuilder.Entity<ApplicationUserTeam>().ToTable("UserTeam")
                .HasKey(ut => new { ut.ApplicationUserId, ut.TeamId });

            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole")
                .HasKey(ur => new { ur.ApplicationUserId, ur.ApplicationUserRoleId });

            modelBuilder.Entity<ProjectModule>().ToTable("ProjectModule")
                .HasKey(pm => new { pm.ProjectId, pm.ModuleId });

            //DataSeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

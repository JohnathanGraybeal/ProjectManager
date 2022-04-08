using JGProject2.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JGProject2.Models.ViewModels;

namespace JGProject2.Services
{
    /// <summary>
    /// creates tables based on models and sets up the intermidiate many to many table
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProjectRole>()
                .HasOne(p => p.Project)
                .WithMany(pr => pr.ProjectRoles)
                .HasForeignKey(pi => pi.ProjectId);
            builder.Entity<ProjectRole>()
                .HasOne(p => p.Person)
                .WithMany(pr => pr.ProjectRoles)
                .HasForeignKey(pi => pi.PersonId);
            builder.Entity<ProjectRole>()
                .HasOne(ar => ar.AppRole)
                .WithMany(pr => pr.ProjectRoles)
                .HasForeignKey(ari => ari.RoleId);

            builder.Entity<ProjectRole>()
                .Property(hr => hr.HourlyRate)
                .HasColumnType("money");
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        
        
    }

}

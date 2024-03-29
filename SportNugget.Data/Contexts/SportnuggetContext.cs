﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportNugget.Data.Models;
#nullable disable

namespace SportNugget.Data.Contexts
{
    public partial class SportnuggetContext : DbContext//IdentityDbContext
    {
        public SportnuggetContext()
        {
        }

        public SportnuggetContext(DbContextOptions<SportnuggetContext> options) : base(options)
        {
        }

        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.PkId);

                entity.Property(e => e.TestName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

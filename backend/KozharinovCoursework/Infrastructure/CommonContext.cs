using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public sealed class CommonContext : DbContext
    {
        public CommonContext(DbContextOptions<CommonContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildHoldingModel()
                .BuildSubjectModel()
                .BuildEnergeticsObjectModel()
                .BuildEnergeticsTypeModel()
                .BuildInvestProjectModel()
                .BuildProjectCheckModel()
                .BuildProjectEquipmentModel()
                ;
            base.OnModelCreating(modelBuilder);
        }

    }

    public static class ModelBuilderExtensions
    {
        public static ModelBuilder BuildHoldingModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Holding>();
            builder.HasKey(holding => holding.Id);
            builder.Property(holding => holding.Name);
            builder.HasOne(holding => holding.Parent);
            builder.HasMany<EnergeticsSubject>().WithOne(subject => subject.Holding).HasForeignKey(subject => subject.HoldingId);
            return modelBuilder;
        }

        public static ModelBuilder BuildSubjectModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<EnergeticsSubject>();
            builder.HasKey(subject => subject.Id);
            builder.Property(subject => subject.Address);
            builder.Property(subject => subject.Email);
            builder.Property(subject => subject.Inn);
            builder.Property(subject => subject.Kpp);
            builder.Property(subject => subject.Okved);
            builder.Property(subject => subject.PostAddress);
            builder.HasOne<Holding>();
            builder.HasMany<EnergeticsObject>().WithOne(o => o.Subject).HasForeignKey(o => o.SubjectId).OnDelete(DeleteBehavior.ClientSetNull);
            return modelBuilder;
        }

        public static ModelBuilder BuildEnergeticsObjectModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<EnergeticsObject>();
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Address);
            builder.Property(o => o.Name);
            builder.HasOne(o => o.Subject);
            builder.HasOne(o => o.Parent);
            builder.HasOne(o => o.EnergeticsType);
            builder.HasMany(o => o.InvestProjects);
            return modelBuilder;
        }

        public static ModelBuilder BuildEnergeticsTypeModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<EnergeticsType>();
            builder.HasKey(type => type.Id);
            builder.Property(type => type.Title);
            return modelBuilder;
        }

        public static ModelBuilder BuildInvestProjectModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<InvestProject>();
            builder.Property(project => project.Name);
            builder.Property(project => project.Customer);
            builder.Property(project => project.Description);
            builder.Property(project => project.Executor);
            builder.Property(project => project.StartYear);
            builder.Property(project => project.EndYear);
            builder.HasMany(project => project.EnergeticsObjects);
            builder.HasOne(project => project.EnergeticsObject).WithMany(o => o.InvestProjects);
            builder.HasMany(project => project.ProjectChecks).WithOne(check => check.InvestProject).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(project => project.ProjectEquipments).WithOne(equipment => equipment.InvestProject)
                .OnDelete(DeleteBehavior.ClientSetNull);
            return modelBuilder;
        }

        public static ModelBuilder BuildProjectCheckModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ProjectCheck>();
            builder.Property(check => check.Start);
            builder.Property(check => check.End);
            builder.Property(check => check.CheckingType);
            builder.HasOne(check => check.EnergeticsObject);
            builder.HasOne(check => check.InvestProject).WithMany(project => project.ProjectChecks).OnDelete(DeleteBehavior.ClientSetNull);
            return modelBuilder;
        }

        public static ModelBuilder BuildProjectEquipmentModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ProjectEquipment>();
            builder.Property(equipment => equipment.State);
            builder.Property(equipment => equipment.Manufacturer);
            builder.Property(equipment => equipment.Type);
            builder.HasOne(equipment => equipment.InvestProject).WithMany(project => project.ProjectEquipments)
                .OnDelete(DeleteBehavior.ClientSetNull);
            return modelBuilder;
        }
    }
}

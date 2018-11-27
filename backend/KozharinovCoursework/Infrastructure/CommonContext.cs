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
            builder.HasMany<EnergeticsObject>().WithOne(o => o.Subject).HasForeignKey(o => o.SubjectId);
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
            return modelBuilder;
        }

        public static ModelBuilder BuildEnergeticsTypeModel(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<EnergeticsType>();
            builder.HasKey(type => type.Id);
            builder.Property(type => type.Title);
            return modelBuilder;
        }
    }
}

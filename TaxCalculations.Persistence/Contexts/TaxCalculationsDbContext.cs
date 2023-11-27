using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using TaxCalculations.Application.Contracts.Services;
using TaxCalculations.Domain.Commons;
using TaxCalculations.Domain.Entities;
using System.Reflection.Emit;

namespace TaxCalculations.Persistence.Contexts;

public class TaxCalculationsDbContext : DbContext
{
    private readonly IDateTimeService _dateTime;
    private readonly IAuthenticatedUserService _authenticatedUser;

    public TaxCalculationsDbContext(DbContextOptions<TaxCalculationsDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _dateTime = dateTime;
        _authenticatedUser = authenticatedUser;
    }
    public DbSet<TaxCalculation> TaxCalculations { get; set; }
    public DbSet<TaxCalculatorType> TaxCalculatorTypes { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.CurrentDateTime;
                    entry.Entity.CreatedBy = _authenticatedUser.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.CurrentDateTime
                        ;
                    entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TaxCalculation>()
        .ToTable("TaxCalculations");
        builder.Entity<TaxCalculation>()
            .Property(t => t.PostalCode)
            .IsRequired(true)
            .HasMaxLength(4);

        builder.Entity<TaxCalculation>()
           .Property(t => t.AnnualIncome)
           .IsRequired(true);

        builder.Entity<TaxCalculation>()
           .Property(t => t.TaxCalculationAmount)
           .IsRequired(true);

        builder.Entity<TaxCalculatorType>()
        .ToTable("TaxCalculatorTypes");

        builder.Entity<TaxCalculatorType>()
            .Property(t => t.PostalCode)
            .IsRequired(true)
            .HasMaxLength(4);

        builder.Entity<TaxCalculatorType>()
            .Property(t => t.TaxType)
            .IsRequired(true)
            .HasMaxLength(15);

        builder.Entity<TaxCalculatorType>()
           .Property(t => t.TaxTypeDescription)
           .IsRequired(true)
           .HasMaxLength(15);

        //builder.Entity<TaxCalculatorType>()
        //    .HasData(
        //        new TaxCalculatorType
        //        {
        //            Id = Guid.NewGuid(),
        //            TaxType = "Progressive",
        //            PostalCode = "7441",
        //            TaxTypeDescription = "Progressive",
        //            CreatedBy = "Aluwani Nethavhakone",
        //            Created = _dateTime.CurrentDateTime
        //        },
        //        new TaxCalculatorType
        //        {
        //            Id = Guid.NewGuid(),
        //            TaxType = "FlatValue",
        //            PostalCode = "A100",
        //            TaxTypeDescription = "Flat Value",
        //            CreatedBy = "Aluwani Nethavhakone",
        //            Created = _dateTime.CurrentDateTime
        //        },
        //        new TaxCalculatorType
        //        {
        //            Id = Guid.NewGuid(),
        //            TaxType = "FlatRate",
        //            PostalCode = "7000",
        //            TaxTypeDescription = "Flat Rate",
        //            CreatedBy = "Aluwani Nethavhakone",
        //            Created = _dateTime.CurrentDateTime
        //        },
        //        new TaxCalculatorType
        //        {
        //            Id = Guid.NewGuid(),
        //            TaxType = "Progressive",
        //            PostalCode = "1000",
        //            TaxTypeDescription = "Progressive",
        //            CreatedBy = "Aluwani Nethavhakone",
        //            Created = _dateTime.CurrentDateTime
        //        }
        //);

        //All Decimals will have 18,6 Range
        foreach (var property in builder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }
        base.OnModelCreating(builder);
    }
}
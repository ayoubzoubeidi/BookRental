using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuraitons;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {

        builder.HasKey(r => new {r.CustomerId, r.BookCopyId, r.RentDate});
        builder.HasIndex(r => r.CustomerId);
        builder.HasIndex(r => r.BookCopyId);
        builder.Property(r => r.RentDate).IsRequired().HasColumnType("datetime");
        builder.Property(r => r.ExpectedReturnDate).IsRequired().HasColumnType("datetime");
        builder.Property(r => r.ActualReturnDate).HasColumnType("datetime");

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerId);

        builder.HasOne(r => r.BookCopy)
            .WithMany(bc => bc.Rentals)
            .HasForeignKey(r => r.BookCopyId);

    }
}

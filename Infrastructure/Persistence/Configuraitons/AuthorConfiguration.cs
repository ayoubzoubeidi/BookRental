using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuraitons;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.DateOfBirth).HasDefaultValue(System.Data.SqlTypes.SqlDateTime.MinValue.Value).IsRequired().HasColumnType("datetime");
        builder.Property(a => a.Email).IsRequired().HasMaxLength(50);
        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.HasMany(a => a.Books).WithMany(b => b.authors).UsingEntity(j => j.ToTable("AuthorsBooks"));
    }
}

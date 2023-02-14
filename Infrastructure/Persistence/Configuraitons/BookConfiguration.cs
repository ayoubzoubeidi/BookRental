using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuraitons;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Description).IsRequired().HasMaxLength(100);
        builder.HasMany(b => b.authors).WithMany(a => a.Books).UsingEntity(j => j.ToTable("AuthorsBooks"));
    }
}

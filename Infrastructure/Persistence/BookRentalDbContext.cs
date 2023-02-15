using Application.Common;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;
public class BookRentalDbContext : DbContext, IBookRentalDbContext
{
    private readonly IDateTime _dateTime;

    public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options) 
        : base(options)
    {
    }

    public BookRentalDbContext(
        DbContextOptions<BookRentalDbContext> options, 
        IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    public DbSet<Customer> CustomerAggregateRoot { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookRentalDbContext).Assembly);
    }
}

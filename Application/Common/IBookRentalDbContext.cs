using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public interface IBookRentalDbContext
{
    DbSet<Customer> CustomerAggregateRoot { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<BookCopy> BookCopies { get; set; }
    DbSet<Rental> Rentals { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

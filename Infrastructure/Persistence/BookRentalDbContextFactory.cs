using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;
public class BookRentalDbContextFactory : DesignTimeDbContextFactoryBase<BookRentalDbContext>
{
    protected override BookRentalDbContext CreateNewInstance(DbContextOptions<BookRentalDbContext> options)
    {
        return new BookRentalDbContext(options);
    }
}

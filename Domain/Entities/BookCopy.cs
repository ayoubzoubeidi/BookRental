using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;
public class BookCopy : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
    public Guid BookId { get; set; }

}

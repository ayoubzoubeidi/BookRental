using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;
public class Rental : BaseAuditableEntity
{
    public Guid CustomerId { get; set; }
    public Guid BookCopyId { get; set; }
    public DateTime RentDate { get; set; }

    public BookCopy BookCopy { get; set; }
    public Customer Customer { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public bool IsReturned() => ActualReturnDate != null;

}

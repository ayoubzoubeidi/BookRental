using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class Book : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<Author> authors { get; set; } = new HashSet<Author>();

    public ICollection<BookCopy> bookCopies { get; set; } = new HashSet<BookCopy>();

}

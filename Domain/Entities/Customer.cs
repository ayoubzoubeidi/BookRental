using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities;
public class Customer : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth{ get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
    public void AddRental(Rental rental)
    {
        rental.Customer = this;
        Rentals.Add(rental);
    }

    public ICollection<Rental> UnreturnedRentals() => Rentals.Where(rental => !rental.IsReturned()).ToList();
    public bool HasUnReturnedRentals() => Rentals.Any(rental => !rental.IsReturned());
    public void ReturnBookCopy(BookCopy bookCopy)
    {
        var rental = Rentals.FirstOrDefault(r => r.BookCopy.Id == bookCopy.Id && !r.IsReturned());

        if (rental == null)
        {
            throw new NotFoundException("The rental does not exist");
        }

        ReturnRental(rental);
    }
    public void ReturnRental(Rental rental)
    {
        rental.ActualReturnDate = DateTime.Now;
    }
}

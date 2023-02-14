using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands.Rental;

public record RentBookCommand(Guid CustomerId, Guid BookCopyId, DateTime RentDate) : IRequest;

public class Handler : IRequestHandler<RentBookCommand>
{
    private readonly IBookRentalDbContext _context;
    public Handler(IBookRentalDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RentBookCommand request, CancellationToken cancellationToken)
    {


        var rental = new Domain.Entities.Rental
        {
            CustomerId = request.CustomerId,
            BookCopyId = request.BookCopyId,
            RentDate = request.RentDate
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}

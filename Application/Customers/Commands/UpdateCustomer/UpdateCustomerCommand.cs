using Application.Common;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands.UpdateCustomer;
public class UpdateCustomerCommand : IRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static implicit operator Customer(UpdateCustomerCommand updateCustomerCommand)
    {
        return new Customer
        {
            Id = updateCustomerCommand.Id,
            FirstName = updateCustomerCommand.FirstName,
            LastName = updateCustomerCommand.LastName,
            DateOfBirth = updateCustomerCommand.DateOfBirth,
            Email = updateCustomerCommand.Email,
            PhoneNumber = updateCustomerCommand.PhoneNumber
        };
    }
}

public class Handler : IRequestHandler<UpdateCustomerCommand>
{

    private readonly IBookRentalDbContext _context;
    private readonly ILogger _logger;

    public Handler(IBookRentalDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        if(!_context.CustomerAggregateRoot.Any(c => c.Id == request.Id))
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        _context.CustomerAggregateRoot.Update(request);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

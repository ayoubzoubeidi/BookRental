using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands.CreateCustomer;
public class CreateCustomerCommand : IRequest<CustomerCreated>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

}

public class Handler : IRequestHandler<CreateCustomerCommand, CustomerCreated>
{
    private readonly IBookRentalDbContext _context;

    public Handler(IBookRentalDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerCreated> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        _context.CustomerAggregateRoot.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return new CustomerCreated(customer.Id);
    }
}

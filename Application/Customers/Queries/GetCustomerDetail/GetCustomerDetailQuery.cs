using Application.Common;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomerDetail;
public record GetCustomerDetailQuery(Guid CustomerId) : IRequest<CustomerDetailVue> { }

public class Handler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVue>
{
    private readonly IBookRentalDbContext _context;

    public Handler(IBookRentalDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDetailVue> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {

        var customer = await _context.CustomerAggregateRoot.AsNoTracking()
            .FirstOrDefaultAsync(customer => customer.Id == request.CustomerId);

        if (customer == null)
        {
            throw new NotFoundException(nameof(customer), request.CustomerId);
        }

        return (CustomerDetailVue) customer;
    }
}
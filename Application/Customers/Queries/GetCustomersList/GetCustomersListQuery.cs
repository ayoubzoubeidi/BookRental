using Application.Common;
using Application.Customers.Queries.GetCustomerDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomersList;
public record GetCustomersListQuery(int PageNumber, int PageSize) : IRequest<CustomerPage>;

public record CustomerPage(ICollection<CustomerDetailVue> Customers);

public class Handler : IRequestHandler<GetCustomersListQuery, CustomerPage>
{

    private readonly IBookRentalDbContext _context;

    public Handler(IBookRentalDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerPage> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        return new CustomerPage(await _context.CustomerAggregateRoot.AsNoTracking().Skip(request.PageNumber * request.PageSize)
            .Select(c => (CustomerDetailVue)c).ToListAsync());
    }
}

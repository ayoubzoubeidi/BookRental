using Application.Common;
using Application.Customers.Queries.GetCustomerDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
    private readonly ILogger _logger;

    public Handler(IBookRentalDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CustomerPage> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Getting All The {Type}", "Customers");
        return new CustomerPage(await _context.CustomerAggregateRoot
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip(request.PageNumber * request.PageSize)
            .Select(c => (CustomerDetailVue)c).ToListAsync());
    }
}

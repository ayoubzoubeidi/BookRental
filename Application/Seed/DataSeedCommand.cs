using Application.Common;
using Application.Customers.Queries.GetCustomerDetail;
using Application.Customers.Queries.GetCustomersList;
using Bogus;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Seed;

public class DataSeedCommand : IRequest<CustomerPage>
{
}


public class Handler : IRequestHandler<DataSeedCommand, CustomerPage>
{
    private readonly IBookRentalDbContext _context;

    public Handler(IBookRentalDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerPage> Handle(DataSeedCommand request, CancellationToken cancellationToken)
    {
        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.PhoneNumber, f => f.Random.ReplaceNumbers("########").ToString());

        var booksFaker = new Faker<Book>()
            .RuleFor(b => b.Title, f => f.Random.Replace("######### ######## ####### ######"))
            .RuleFor(b => b.Description, f => f.Lorem.Paragraphs().Take(80).ToString());

        var authorFaker = new Faker<Author>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.PhoneNumber, f => f.Random.ReplaceNumbers("########").ToString())
            .RuleFor(c => c.Books, f => booksFaker.Generate(5).ToList());

        var customers = customerFaker.Generate(100_000);
        var authors = authorFaker.Generate(500);
        foreach (var author in authors)
        {
            foreach (var book in author.Books)
            {
                var bc1 = new BookCopy();
                var bc2 = new BookCopy();
                var bc3 = new BookCopy();

                bc1.Book = book;
                bc2.Book = book;
                bc3.Book = book;
                
                book.bookCopies.Add(bc1);
                book.bookCopies.Add(bc2);
                book.bookCopies.Add(bc3);
            }
        }
        await _context.CustomerAggregateRoot.AddRangeAsync(customers);
        await _context.Authors.AddRangeAsync(authors);
        await _context.SaveChangesAsync(cancellationToken);
        return new CustomerPage(customerFaker.Generate(100).Select(c => (CustomerDetailVue)c).ToList());
    }
}

using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Entities;

abstract class EntititesSetUp
{

    protected Guid BOOK_ID = Guid.NewGuid();
    protected const string BOOK_TITLE = "Title";
    protected const string BOOK_DESCRIPTION = "Description";


    protected const string CUSTOMER_FIRSTNAME = "First Name";
    protected const string CUSTOMER_LASTNAME = "Last Name";
    protected const string CUSTOMER_EMAIL = "email.email.com";
    protected const string CUSTOMER_PHONE_NUMBER = "12345678";
    protected DateTime CUSTOMER_DATE_OF_BIRTH = DateTime.Now;
    protected Guid CUSTOMER_ID = Guid.NewGuid();

    protected Guid BOOK_COPY_ID = Guid.NewGuid();

    protected Guid RANDOM_GUID = Guid.NewGuid();

    protected Customer customer;
    protected Book book;
    protected BookCopy bookCopy;
    protected Rental rental;
    protected HashSet<Customer> customers;

    protected BookCopy bookCopyNotInRentals;
    protected Guid BOOK_COPY_NOT_IN_RENTALS_ID = Guid.NewGuid();

    [SetUp]
    protected void SetUp()
    {

        customer = new Customer();
        book = new Book();
        bookCopy = new BookCopy();
        rental = new Rental();
        customers = new HashSet<Customer>();
        bookCopyNotInRentals = new BookCopy();


        book.Id = BOOK_ID;
        book.Title = BOOK_TITLE;
        book.Description = BOOK_DESCRIPTION;

        bookCopy.Id = BOOK_COPY_ID;
        bookCopy.Book = book;
        bookCopy.BookId = book.Id;
        book.bookCopies.Add(bookCopy);

        bookCopyNotInRentals.Id = BOOK_COPY_NOT_IN_RENTALS_ID;

        rental.BookCopy = bookCopy;
        rental.BookCopyId = bookCopy.Id;
        rental.Customer = customer;
        rental.CustomerId = customer.Id;
        


        customer.Id = CUSTOMER_ID;
        customer.FirstName = CUSTOMER_FIRSTNAME;
        customer.LastName = CUSTOMER_LASTNAME;
        customer.DateOfBirth = CUSTOMER_DATE_OF_BIRTH;
        customer.Email = CUSTOMER_EMAIL;
        customer.PhoneNumber = CUSTOMER_PHONE_NUMBER;
        customer.LastName = CUSTOMER_LASTNAME;
        customer.Rentals.Add(rental);
    }

}

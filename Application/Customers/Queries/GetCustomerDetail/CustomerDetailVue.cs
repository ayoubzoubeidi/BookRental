using Domain.Entities;

namespace Application.Customers.Queries.GetCustomerDetail;

public class CustomerDetailVue
{
    public Guid Id { get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static explicit operator CustomerDetailVue(Customer customer)
    {
        return new CustomerDetailVue
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.DateOfBirth,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber

        };
    }
}

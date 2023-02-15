using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands.UpdateCustomer;
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{

    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.DateOfBirth).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
    }

}

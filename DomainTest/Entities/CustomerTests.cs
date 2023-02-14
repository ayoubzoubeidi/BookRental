using Domain.Exceptions;
using FluentAssertions;

namespace DomainTest.Entities;

class CustomerTests : EntititesSetUp
{

    [Test]
    public void ShouldHaveOneUnreturnedRental()
    {
        customer.UnreturnedRentals().Count.Should().Be(1);
        customer.HasUnReturnedRentals().Should().BeTrue();
    }


    [Test]
    public void ShouldHaveNoUnreturnedRentals()
    {
        customer.ReturnBookCopy(bookCopy);
        customer.UnreturnedRentals().Count.Should().Be(0);
    }

    [Test]
    public void ShouldReturnRentalNotFoundException()
    {
        Action action = () => customer.ReturnBookCopy(bookCopyNotInRentals);
        action.Should().Throw<NotFoundException>().WithMessage("The rental does not exist");
    }

}

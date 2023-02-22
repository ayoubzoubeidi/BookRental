using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.Rental;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomerDetail;
using Application.Customers.Queries.GetCustomersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{

    private IMediator _mediator;
    Serilog.ILogger _logger;

    public CustomersController(IMediator mediator, Serilog.ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        var createdCustomerId = await _mediator.Send(createCustomerCommand);
        return new CreatedAtActionResult(nameof(GetById), "Customers", new { id = createdCustomerId.Id }, null);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 20)
    {

        var customers = await _mediator.Send(new GetCustomersListQuery(pageNumber, pageSize));
        return Ok(customers);

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _mediator.Send(new GetCustomerDetailQuery(id));
        return Ok(customer);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerCommand updateCustomerCommand)
    {
        updateCustomerCommand.Id = customerId;
        await _mediator.Send(updateCustomerCommand);
        return NoContent();
    }

    [HttpPut("AddRental")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddRental([FromBody] RentBookCommand rentBookCommand)
    {
        await _mediator.Send(rentBookCommand);
        return NoContent();
    }

}

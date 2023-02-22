using Application.Customers.Queries.GetCustomersList;
using Application.Seed;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedController : ControllerBase
    {

        private IMediator _mediator;
        Serilog.ILogger _logger;

        public DataSeedController(IMediator mediator, Serilog.ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 20)
        {

            var customers = await _mediator.Send(new DataSeedCommand());
            return Ok(customers);

        }

    }
}

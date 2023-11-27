using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxCalculations.API.Models.TaxCalculator;
using TaxCalculations.Application.Contracts.Services;
using TaxCalculations.Application.Features.TaxCalculate.Delete;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;


namespace TaxCalculations.API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TaxCalculationsController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        public TaxCalculationsController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("calculateTax")]
        public async Task<IActionResult> Post(TaxCalculateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("getTaxCalculations")]
        public async Task<IActionResult> Get()
        {
            GetAllTaxCalculationsQuery request = new GetAllTaxCalculationsQuery();
            return Ok(await Mediator.Send(request));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("getTaxCalculation/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetTaxCalculationByIdQuery { Id = id}));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("deleteTaxCalculation/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await Mediator.Send(new DeleteTaxCalculationByIdCommand { Id = id}));
        }
    }
}

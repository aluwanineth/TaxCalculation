using MediatR;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Delete;

public class DeleteTaxCalculationByIdCommand : IRequest <Response<Guid>>
{
    public Guid Id { get; set; }
}

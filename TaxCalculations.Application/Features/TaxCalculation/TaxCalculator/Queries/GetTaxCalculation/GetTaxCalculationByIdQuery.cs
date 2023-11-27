using MediatR;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;

public class GetTaxCalculationByIdQuery : IRequest<Response<GetTaxCalculationByIdQueryResponse>>
{
    public Guid Id { get; set; }
}

using MediatR;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;

public class GetAllTaxCalculationsQuery : IRequest<Response<IEnumerable<TaxCalculateQueryResponse>>>
{

}

using MediatR;
using TaxCalculations.Application.Business.TaxCalculator;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;

public class GeltAllTaxCalculationsQueryHandler : IRequestHandler<GetAllTaxCalculationsQuery, Response<IEnumerable<TaxCalculateQueryResponse>>>
{
    private readonly ITaxCalculatorBusinessAsync _taxCalculationBusinessAsync;

    public GeltAllTaxCalculationsQueryHandler(ITaxCalculatorBusinessAsync taxCalculationBusinessAsync)
    {
        _taxCalculationBusinessAsync = taxCalculationBusinessAsync;
    }
    public async Task<Response<IEnumerable<TaxCalculateQueryResponse>>> Handle(GetAllTaxCalculationsQuery request, CancellationToken cancellationToken)
    {
        return await _taxCalculationBusinessAsync.GetTaxCalculations(request);
    }
}

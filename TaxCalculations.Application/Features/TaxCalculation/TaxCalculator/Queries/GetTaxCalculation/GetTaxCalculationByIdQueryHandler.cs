using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculations.Application.Business.TaxCalculator;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation
{
    public class GetTaxCalculationByIdQueryHandler : IRequestHandler<GetTaxCalculationByIdQuery, Response<GetTaxCalculationByIdQueryResponse>>
    {
        private readonly ITaxCalculatorBusinessAsync _taxCalculationBusinessAsync;

        public GetTaxCalculationByIdQueryHandler(ITaxCalculatorBusinessAsync taxCalculationBusinessAsync)
        {
            _taxCalculationBusinessAsync = taxCalculationBusinessAsync;
        }

        public async Task<Response<GetTaxCalculationByIdQueryResponse>> Handle(GetTaxCalculationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taxCalculationBusinessAsync.GetTaxCalculateAync(request);
        }
    }
}

using TaxCalculations.Application.Features.TaxCalculate.Delete;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;

using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Business.TaxCalculator;

public interface ITaxCalculatorBusinessAsync
{
    Task<Response<TaxCalculateCommandResponse>> CalculateTax(TaxCalculateCommand request);
    Task<Response<IEnumerable<TaxCalculateQueryResponse>>> GetTaxCalculations(GetAllTaxCalculationsQuery request);
    Task<Response<Guid>> DeleteTaxCalculateAync(DeleteTaxCalculationByIdCommand request);
    Task<Response<GetTaxCalculationByIdQueryResponse>> GetTaxCalculateAync(GetTaxCalculationByIdQuery request);
}

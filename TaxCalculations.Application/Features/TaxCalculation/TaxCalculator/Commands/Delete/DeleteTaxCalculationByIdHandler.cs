using MediatR;
using TaxCalculations.Application.Business.TaxCalculator;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculate.Delete;

public class DeleteTaxCalculationByIdHandler : IRequestHandler<DeleteTaxCalculationByIdCommand, Response<Guid>>
{
    private readonly ITaxCalculatorBusinessAsync _taxCalculationBusinessAsync;

    public DeleteTaxCalculationByIdHandler(ITaxCalculatorBusinessAsync taxCalculationBusinessAsync)
    {
        _taxCalculationBusinessAsync = taxCalculationBusinessAsync;
    }
    public async Task<Response<Guid>> Handle(DeleteTaxCalculationByIdCommand request, CancellationToken cancellationToken)
    {
        return await _taxCalculationBusinessAsync.DeleteTaxCalculateAync(request);
    }
}

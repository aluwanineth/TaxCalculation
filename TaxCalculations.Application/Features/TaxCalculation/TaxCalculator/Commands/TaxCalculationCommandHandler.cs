using MediatR;
using TaxCalculations.Application.Business.TaxCalculator;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;

public class TaxCalculationCommandHandler : IRequestHandler<TaxCalculateCommand, Response<TaxCalculateCommandResponse>>
{
    private readonly ITaxCalculatorBusinessAsync _taxCalculationBusinessAsync;

    public TaxCalculationCommandHandler(ITaxCalculatorBusinessAsync taxCalculationBusinessAsync)
    {
        _taxCalculationBusinessAsync = taxCalculationBusinessAsync;
    }
    public async Task<Response<TaxCalculateCommandResponse>> Handle(TaxCalculateCommand request, CancellationToken cancellationToken)
    {
        return await _taxCalculationBusinessAsync.CalculateTax(request);
    }
}

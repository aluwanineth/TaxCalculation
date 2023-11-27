using TaxCalculations.Application.Enums;

namespace TaxCalculations.Application.Services.TaxCalculator;

public interface ITaxCalculatorService
{
    double CalculateTax(CalculationTaxType taxType, double taxableIncome);
}

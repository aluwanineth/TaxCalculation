using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;

public class TaxCalculateCommandResponse
{
    public string PostalCode { get; set; }
    public string TaxCalculationType { get; set; }
    public double? AnnualIncome { get; set; }
    public double? TaxCalculationAmount { get; set; }
}

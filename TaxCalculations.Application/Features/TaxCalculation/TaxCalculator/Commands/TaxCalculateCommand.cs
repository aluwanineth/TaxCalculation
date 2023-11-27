using MediatR;
using System.ComponentModel;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;

public partial class TaxCalculateCommand : IRequest<Response<TaxCalculateCommandResponse>>
{
    [DisplayName("Postal Code")]
    public string PostalCode { get; set; }
    [DisplayName("Annual Income")]
    public double AnnualIncome { get; set; }
}

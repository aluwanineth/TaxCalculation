using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;

public class GetTaxCalculationByIdQueryResponse
{
    public Guid Id { get; set; }
    [DisplayName("Postal Code")]
    public string PostalCode { get; set; }
    [DisplayName("Annual Income")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public double? AnnualIncome { get; set; }
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DisplayName("Tax Amount")]
    public double? TaxCalculationAmount { get; set; }
    [DisplayName("Created Date Time")]
    public DateTime Created { get; set; }
}

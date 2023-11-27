namespace TaxCalculations.API.Models.TaxCalculator
{
    public record TaxCalculatorRequest
    {
        public string PostalCode { get; set; }
        public double AnnualIncome { get; set; }
    }
}

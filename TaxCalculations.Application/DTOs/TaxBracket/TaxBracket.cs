namespace TaxCalculations.Application.DTOs.TaxBracket;

public class TaxBracket
{
    public double Rate { get; set; }
    public double From { get; set; }
    public double To { get; set; }

    public TaxBracket(double rate, double lowerLimit, double upperLimit)
    {
        Rate = rate;
        From = lowerLimit;
        To = upperLimit;
    }
}

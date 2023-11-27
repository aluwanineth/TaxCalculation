using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculations.Application.DTOs.TaxBracket;
using TaxCalculations.Application.Enums;
using TaxCalculations.Application.Exceptions;

namespace TaxCalculations.Application.Services.TaxCalculator;

public sealed class TaxCalculatorService : ITaxCalculatorService
{
    private static readonly TaxBracket[] taxBrackets = {
        new TaxBracket(0.10, 0, 8350),
        new TaxBracket(0.15, 8351, 33950),
        new TaxBracket(0.25, 33951, 82250),
        new TaxBracket(0.28, 82251, 171550),
        new TaxBracket(0.33, 171551, 372950),
        new TaxBracket(0.35, 372951, double.MaxValue)
    };
    public double CalculateTax(CalculationTaxType taxType, double taxableIncome)
    {
        double tax;

        switch (taxType)
        {
            case CalculationTaxType.Progressive:
                tax = CalculateProgressiveTax(taxableIncome);
                break;

            case CalculationTaxType.FlatValue:
                tax = CalculateFlatValueTax(taxableIncome);
                break;


            case CalculationTaxType.FlatRate:
                tax = CalculateFlatRateTax(taxableIncome);
                break;

            default:
                throw new ApiException("Invalid tax type");
        }

        return tax;
    }
    private double CalculateProgressiveTax(double annualIncome)
    {
        double totalTax = 0;

        foreach (var bracket in taxBrackets)
        {
            if (annualIncome<= bracket.To)
            {
                totalTax += (annualIncome - bracket.From) * bracket.Rate;
                break;
            }
            else
            {
                totalTax += (bracket.To - bracket.From) * bracket.Rate;
            }
        }

        return Math.Round(totalTax,2);
    }

    private double CalculateFlatValueTax(double income)
    {
        const double FlatTaxRate = 10000;
        const double PercentageTaxRate = 0.05;
        double tax = 0;

        if (income >= 200000)
        {
            tax = FlatTaxRate;
        }
        else
        {
            tax = income * PercentageTaxRate;
        }

        return Math.Round(tax,2);
    }
    private double CalculateFlatRateTax(double income)
    {
        const double FlatTaxRate = 0.175; // 17.5%

        double tax = income * FlatTaxRate;

        return Math.Round(tax, 2);
    }
}

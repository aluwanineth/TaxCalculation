using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculations.Application.Enums;
using TaxCalculations.Application.Services.TaxCalculator;

namespace TaxCalculations.UnitTest
{
    [TestFixture]
    public class CalculatorTests
    {
        private readonly Mock<ITaxCalculatorService> _mockTaxkCalculator;
        public CalculatorTests()
        {
            _mockTaxkCalculator = new Mock<ITaxCalculatorService>();
        }

        [TestCase(5000, CalculationTaxType.Progressive, 500)] // Example with income less than 8,350
        [TestCase(15000, CalculationTaxType.Progressive, 1832.3499)] // Example with income between 8,351 and 33,950
        [TestCase(50000, CalculationTaxType.Progressive, 8687.10)] // Example with income between 33,951 and 82,250
        [TestCase(100000, CalculationTaxType.Progressive, 21719.32)] // Example with income between 82,251 and 171,550
        [TestCase(200000, CalculationTaxType.Progressive, 51141.49)] // Example with income between 171,551 and 372,950
        [TestCase(400000, CalculationTaxType.Progressive, 117682.14)] // Example with income greater than 372,950
        public void CalculateProgressiveTax_ShouldCalculateCorrectProgressiveTax(double income, CalculationTaxType taxType, double expectedTax)
        {
            // Arrange

            _mockTaxkCalculator.Setup(x => x.CalculateTax(It.IsAny<CalculationTaxType>(), It.IsAny<double>())).Returns((double i, CalculationTaxType t) => i);


            var calculator = new TaxCalculatorService();

            // Act
            double actualTax = calculator.CalculateTax(taxType, income);

            // Assert
            Assert.AreEqual(expectedTax, actualTax, 0.01); 
        }

        [TestCase(100000, CalculationTaxType.FlatValue, 5000)] // Example with income less than 200,000
        [TestCase(250000, CalculationTaxType.FlatValue, 10000)] // Example with income 200,000 or more
        [TestCase(0, CalculationTaxType.FlatValue, 0)] // Example with zero income
        [TestCase(300000, CalculationTaxType.FlatValue, 10000)] // Example with higher income
        public void CalculateTax_ShouldCalculateCorrectFlatValueTax(double income, CalculationTaxType taxType, double expectedTax)
        {
            // Arrange

            _mockTaxkCalculator.Setup(x => x.CalculateTax(It.IsAny<CalculationTaxType>(), It.IsAny<double>())).Returns((double i, CalculationTaxType t) => i);


            var calculator = new TaxCalculatorService();

            // Act
            double actualTax = calculator.CalculateTax(taxType, income);

            // Assert
            Assert.AreEqual(expectedTax, actualTax, 0.01); 
        }

        [TestCase(100000, CalculationTaxType.FlatRate, 17500)] // Example with income 100,000
        [TestCase(50000, CalculationTaxType.FlatRate, 8750)] // Example with income 50,000
        [TestCase(0, CalculationTaxType.FlatRate, 0)] // Example with zero income
        [TestCase(200000, CalculationTaxType.FlatRate, 35000)] // Example with higher income
        public void CalculateTax_ShouldCalculateCorrectFlatRateTax(double annualIncome, CalculationTaxType taxType, double expectedTax)
        {
            // Arrange

            _mockTaxkCalculator.Setup(x => x.CalculateTax(It.IsAny<CalculationTaxType>(), It.IsAny<double>())).Returns((double i, CalculationTaxType t) => i);


            var calculator = new TaxCalculatorService();

            // Act
            double actualTax = calculator.CalculateTax(taxType, annualIncome);

            // Assert
            Assert.AreEqual(expectedTax, actualTax, 0.01); 
        }
    }
}

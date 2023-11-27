using TaxCalculations.Domain.Entities;

namespace TaxCalculations.Application.Contracts.Repositories;

public interface ITaxCalculatorTypeRepositoryAsync : IGenericRepositoryAsync<TaxCalculatorType>
{
    Task<string> GetTaxCalculatorTypeAsync(string postalCode);
}

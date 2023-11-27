using TaxCalculations.Application.Contracts.Repositories;
using TaxCalculations.Domain.Entities;
using TaxCalculations.Persistence.Contexts;

namespace TaxCalculations.Persistence.Repositories;

public class TaxCalculationRepositoryAsync : GenericRepositoryAsync<TaxCalculation>, ITaxCalculationRepositoryAsync
{
    public TaxCalculationRepositoryAsync(TaxCalculationsDbContext dbContext) : base(dbContext)
    { }
}

using Microsoft.EntityFrameworkCore;
using TaxCalculations.Application.Contracts.Repositories;
using TaxCalculations.Application.Exceptions;
using TaxCalculations.Domain.Entities;
using TaxCalculations.Persistence.Contexts;

namespace TaxCalculations.Persistence.Repositories;

public class TaxCalculatorTypeRepositoryAsync : GenericRepositoryAsync<TaxCalculatorType>, ITaxCalculatorTypeRepositoryAsync
{
    private readonly DbSet<TaxCalculatorType> _taxCalculatorType;
    public TaxCalculatorTypeRepositoryAsync(TaxCalculationsDbContext dbContext) : base(dbContext)
    {
        _taxCalculatorType = dbContext.Set<TaxCalculatorType>();
    }

    public async Task<string> GetTaxCalculatorTypeAsync(string postalCode)
    {
        var query = await _taxCalculatorType.FirstOrDefaultAsync(t => t.PostalCode == postalCode);

        if(query is not null) 
        {
            return query?.TaxType;
        }
        else
        {
            throw new ApiException("Tax type not found");
        }
        
    }
}

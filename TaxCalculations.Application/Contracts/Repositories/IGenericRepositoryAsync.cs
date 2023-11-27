namespace TaxCalculations.Application.Contracts.Repositories;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
}
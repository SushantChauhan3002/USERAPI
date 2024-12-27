using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Response;

namespace CleanArchitecture.Application.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<Result<AdminUser>> GetByEmailAsync(string email);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<AdminUser> GetByUsernameAsync(string username);
    }
}

using CleanArchitecture.Application.Application;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Response;

namespace CleanArchitecture.Application.Services
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUser>> GetAllUsersAsync();
        Task<AdminUser> GetUserByIdAsync(Guid UserId);
        Task<Result<string>> CreateUserAsync(AdminUser user);
        Task UpdateUserAsync(AdminUser user);
        Task DeleteUserAsync(Guid UserId);
        Task<bool> CheckIfUserExistsAsync(AdminUserRequestInput request);
        Task<AdminUser> GetUserByUsernameAsync(string username);
        
    }
}


using CleanArchitecture.Application.Application;
using CleanArchitecture.Application.GenericRepository;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Response;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;

namespace CleanArchitecture.Application.Service
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IGenericRepository<AdminUser> _adminUserRepository;
        private readonly IGenericRepository<AdminRole> _adminRoleRepository;
        private readonly ILogger<AdminUserService> _logger;
        private readonly UserDbContext _UserDbContext;


        public AdminUserService(IGenericRepository<AdminUser> adminUserRepository, IGenericRepository<AdminRole> adminRoleRepository, ILogger<AdminUserService> logger, UserDbContext userDbContext)
        {
            _adminUserRepository = adminUserRepository;
            _adminRoleRepository = adminRoleRepository;
            _logger = logger;
            _UserDbContext = userDbContext;
        }


        public async Task<IEnumerable<AdminUser>> GetAllUsersAsync()
        {
            Log.Information("Fetching all users from repository.");
            return await _adminUserRepository.GetAllAsync();
        }

        public async Task<AdminUser> GetUserByIdAsync(Guid UserId)
        {
            Log.Information("Fetching user with ID: {UserId}", UserId);
            return await _adminUserRepository.GetByIdAsync(UserId);
        }

        public async Task<Result<string>> CreateUserAsync(AdminUser user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Create the user in the database
                await _adminUserRepository.CreateAsync(user);
                
                // Return success result after creating the user
                return Result<string>.Success("User created successfully.");

            }

            catch (Exception ex)
            {
                // Log the exception if any error occurs
                _logger.LogError(ex, "Error occurred while creating user with email: {Email}", user.Email);
                return Result<string>.Fail("An error occurred while creating the user.", 500);
            }
        }




        public async Task DeleteUserAsync(Guid UserId)
        {
            Log.Information("Deleting user with ID: {UserId}", UserId);
            await _adminUserRepository.DeleteAsync(UserId);
        }

        public async Task UpdateUserAsync(AdminUser user)
        {
            Log.Information("Updating user with username: {Username}", user.Username);

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            await _adminUserRepository.UpdateAsync(user);
        }

        public async Task<AdminUser> GetUserByUsernameAsync(string username)
        {
            return await _adminUserRepository.GetByUsernameAsync(username);
        }

        // New method to check if a user exists and create one if not
        public async Task<bool> CheckIfUserExistsAsync(AdminUserRequestInput request)
        {

            var emailCheckResult = await _adminUserRepository.GetByEmailAsync(request.Email);
            if (!emailCheckResult.HasError)
            {
                return true; // User with this email exists
            }

            // Check if user exists by username
            var usernameCheckResult = await _adminUserRepository.GetByUsernameAsync(request.Username);
            if (usernameCheckResult != null)
            {
                return true; // User with this username exists
            }

            return false; // No user exists with this email or username

        }
    }

}


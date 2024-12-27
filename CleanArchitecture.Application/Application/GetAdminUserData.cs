using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Application
{
    public class GetAdminUserData : IRequestHandler<AdminUserRequestInput, Result<string>>
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<GetAdminUserData> _logger;
        public GetAdminUserData(IAdminUserService adminUserService , ILogger<GetAdminUserData> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(AdminUserRequestInput request, CancellationToken cancellationToken)
        {
            var existingUser = await _adminUserService.CheckIfUserExistsAsync(request);

            if (existingUser)
            {
               
                return Result<string>.Fail( "User with Email or Username already exists.", 400);
            }

            // If the user does not exist, create a new user
            var newUser = new AdminUser
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            // Create the new user
            await _adminUserService.CreateUserAsync(newUser);

            return Result<string>.Success("User created successfully");
        }
    }
}



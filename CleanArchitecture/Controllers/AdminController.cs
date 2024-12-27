using CleanArchitecture.Application.Application;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IMediator mediator;
        private readonly IAdminUserService _adminUserService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminController> _logger;


        public AdminController(IMediator mediator, IAdminUserService adminUserService, IConfiguration configuration,
            ILogger<AdminController> logger)
        {
            _logger = logger;
            this.mediator = mediator;
            _adminUserService = adminUserService;
            _configuration = configuration;

        }


        [HttpPost("createuserifnotexists")]
        public async Task<IActionResult> Createuserifnotexists([FromBody] AdminUserRequestInput request)
        {

            try
            {
                if (request == null)
                {
                    _logger.LogError("Request body is null.");

                    return BadRequest("Request body cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    _logger.LogWarning("Request is missing required fields.");

                    return BadRequest("Username and password are required.");
                }
                
                // Handle result returned from mediator
                var result = await mediator.Send(request);

                if (result.HasError)
                {
                    _logger.LogError("Error creating user: {Message}", result.Message);

                    return StatusCode(result.StatusCode, result);
                }

                // Log and return success response
                _logger.LogInformation("User with email {Email} created successfully.", request.Email);

                return Ok(result); // Return the success message
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request.");
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Request cannot be null.");
            }

            // Validate the user credentials
            var user = await _adminUserService.GetUserByUsernameAsync(loginRequest.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Message = "Login successful", User = user.Username });

        }
    }
}


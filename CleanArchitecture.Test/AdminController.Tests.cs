using CleanArchitecture.API;
using CleanArchitecture.API.Controllers;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.Test
{
    public class AdminControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IAdminUserService> _adminUserServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<ILogger<AdminController>> _loggerMock;
        private AdminController _adminController;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminUserServiceMock = new Mock<IAdminUserService>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<AdminController>>();

            // Create the controller with the mocked dependencies
            _adminController = new AdminController(
                _mediatorMock.Object,
                _adminUserServiceMock.Object,
                _configurationMock.Object,
                _loggerMock.Object


            );
        }

        [Test]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "testuser",
                Password = "password"
            };

            var user = new AdminUser
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            };

            // Mock the behavior of GetUserByUsernameAsync method
            _adminUserServiceMock.Setup(s => s.GetUserByUsernameAsync(loginRequest.Username)).ReturnsAsync(user);

            // Act
            var result = await _adminController.Login(loginRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult but got null.");

            // Ensure the 'Value' of the OkObjectResult has a 'Message' property.

            var responseValue = okResult.Value as dynamic;
            Assert.IsNotNull(responseValue, "Expected a response value but got null.");

            Assert.AreEqual("Login successful", responseValue.Message, "Login message mismatch.");
        }

        [Test]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "invaliduser",
                Password = "wrongpassword"
            };

            // Mock the behavior of GetUserByUsernameAsync method to return null (invalid user)

            _adminUserServiceMock.Setup(s => s.GetUserByUsernameAsync(loginRequest.Username)).ReturnsAsync((AdminUser)null);

            
            var result = await _adminController.Login(loginRequest);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult, "Expected UnauthorizedObjectResult but got null.");
            Assert.AreEqual("Invalid username or password.", unauthorizedResult.Value, "Error message mismatch.");
        }
    }
}

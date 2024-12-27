using CleanArchitecture.Application.GenericRepository;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Entities;
using Moq;

namespace CleanArchitecture.Test
{
    [TestFixture]
    public class AdminUserServiceTests
    {
        private Mock<IGenericRepository<AdminUser>> _adminUserRepoMock;
        private Mock<IGenericRepository<AdminRole>> _adminRoleRepoMock;
        private AdminUserService _adminUserService;


        [SetUp]
        public void Setup()
        {
            // Initialize mocks
            _adminUserRepoMock = new Mock<IGenericRepository<AdminUser>>();
            _adminRoleRepoMock = new Mock<IGenericRepository<AdminRole>>();

            // Initialize the service under test with mocked dependencies
            //_adminUserService = new AdminUserService(_adminUserRepoMock.Object, _adminRoleRepoMock.Object);
        }

        // Sample test for CreateUserAsync method
        [Test]
        public async Task CreateUserAsync_ShouldCreateNewUser_WhenValidUserIsPassed()
        {
            // Arrange: Prepare data
            var newUser = new AdminUser
            {
                UserId = Guid.NewGuid(),
                Username = "newuser",
                Password = "hashedpassword", // Assume password is already hashed
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                IsActive = true
            };

            // Mock repository method to simulate successful creation
            _adminUserRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<AdminUser>())).Returns(Task.CompletedTask);

            // Act: Call the method under test
            await _adminUserService.CreateUserAsync(newUser);

            // Assert: Verify repository method was called
            _adminUserRepoMock.Verify(repo => repo.CreateAsync(It.Is<AdminUser>(u => u.Username == "newuser")), Times.Once);
        }

        [Test]
        public void CreateUserAsync_ShouldThrowArgumentNullException_WhenUserIsNull()
        {
            // Act & Assert: Verify ArgumentNullException is thrown
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _adminUserService.CreateUserAsync(null));
        }

        // Test for CreateUserAsync with Existing User
        [Test]
        public async Task CreateUserAsync_ShouldThrowInvalidOperationException_WhenUserAlreadyExists()
        {
            // Arrange: Prepare data and mock repository
            var newUser = new AdminUser
            {
                UserId = Guid.NewGuid(),
                Username = "existinguser",
                Password = "hashedpassword",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                IsActive = true
            };

            _adminUserRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<AdminUser>())).Returns(Task.CompletedTask);


            // Act & Assert: Verify exception is thrown
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminUserService.CreateUserAsync(newUser));
            Assert.AreEqual("User already exists", ex.Message);
        }

    //    [Test]
    //    public async Task GetAllUsersAsync_ShouldReturnListOfUsers_WhenUsersExist()
    //    {
    //        // Arrange: Prepare mock data
    //        var usersList = new List<AdminUser>
    //{
    //    new AdminUser { UserId = Guid.NewGuid(), Username = "user1", FirstName = "Alice", LastName = "Smith", Email = "alice@example.com" },
    //    new AdminUser { UserId = Guid.NewGuid(), Username = "user2", FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com" }
    //};

    //        _adminUserRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usersList);

    //        // Act: Call the method under test
    //        var result = await _adminUserService.GetAllUsersAsync();

    //        // Assert: Verify the result contains the expected users
    //        Assert.AreEqual(2, result.Count);
    //        Assert.AreEqual("user1", result[0].Username);
    //        Assert.AreEqual("user2", result[1].Username);
    //    }

    //    // Test for GetUserByIdAsync
        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Prepare mock data
            var userId = Guid.NewGuid();
            var user = new AdminUser
            {
                UserId = userId,
                Username = "existinguser",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            };

            _adminUserRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act: Call the method under test
            var result = await _adminUserService.GetUserByIdAsync(userId);

            // Assert: Verify the correct user is returned
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual("existinguser", result.Username);
        }

        // Test for GetUserByIdAsync when user does not exist
        [Test]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange: Prepare mock behavior for non-existent user
            var userId = Guid.NewGuid();
            _adminUserRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((AdminUser)null);

            // Act: Call the method under test
            var result = await _adminUserService.GetUserByIdAsync(userId);

            // Assert: Verify the result is null
            Assert.IsNull(result);
        }

        // Test for UpdateUserAsync
        [Test]
        public async Task UpdateUserAsync_ShouldUpdateUser_WhenUserExists()
        {
            // Arrange: Prepare mock data
            var userId = Guid.NewGuid();
            var existingUser = new AdminUser
            {
                UserId = userId,
                Username = "oldusername",
                FirstName = "Old",
                LastName = "Name",
                Email = "oldname@example.com"
            };

            var updatedUser = new AdminUser
            {
                UserId = userId,
                Username = "newusername",
                FirstName = "New",
                LastName = "Name",
                Email = "newname@example.com"
            };

            _adminUserRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);

            _adminUserRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<AdminUser>())).Returns(Task.CompletedTask);

            // Act: Call the method under test

            await _adminUserService.UpdateUserAsync(updatedUser);

            // Assert: Verify repository method was called

            _adminUserRepoMock.Verify(repo => repo.UpdateAsync(It.Is<AdminUser>(u => u.Username == "newusername")), Times.Once);
        }

        // Test for DeleteUserAsync
        [Test]
        public async Task DeleteUserAsync_ShouldDeleteUser_WhenUserExists()
        {
            // Arrange: Prepare mock data
            var userId = Guid.NewGuid();
            var existingUser = new AdminUser
            {
                UserId = userId,
                Username = "deletableuser",
                FirstName = "ToBe",
                LastName = "Deleted",
                Email = "tobedeleted@example.com"
            };

            _adminUserRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);

            _adminUserRepoMock.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act: Call the method under test
            await _adminUserService.DeleteUserAsync(userId);

            // Assert: Verify the delete method was called
            _adminUserRepoMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}




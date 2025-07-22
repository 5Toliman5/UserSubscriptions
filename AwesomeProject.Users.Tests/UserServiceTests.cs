using AwesomeProject.Common.Result;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Application.Services;
using AwesomeProject.Users.Domain.Entities;
using Moq;

namespace AwesomeProject.Users.Tests
{
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _mockRepository;
		private readonly UserService _service;

		public UserServiceTests()
		{
			_mockRepository = new Mock<IUserRepository>();
			_service = new UserService(_mockRepository.Object);
		}

		[Fact]
		public async Task GetAllAsync_ReturnsUserList()
		{
			// Arrange
			var users = new List<User> { new User("John", "test@mail.com") };
			_mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

			// Act
			var result = await _service.GetAllAsync();

			// Assert
			Assert.Single(result);
			Assert.Equal("John", result.First().Name);
		}

		[Fact]
		public async Task GetByIdAsync_UserExists_ReturnsSuccess()
		{
			// Arrange
			_mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new User("John", "email"));

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task GetByIdAsync_UserNotFound_ReturnsFailure()
		{
			// Arrange
			_mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User)null);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.False(result.Successful);
			Assert.Equal(ResultErrorType.NotFound, result.ErrorType);
		}
	}
}

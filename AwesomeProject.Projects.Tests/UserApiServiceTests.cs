using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.ExternalServices;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Services;
using Moq;

namespace AwesomeProject.Projects.Tests
{
	public class UserApiServiceTests
	{
		private readonly Mock<IUserApiClient> _mockClient;
		private readonly UserApiService _service;

		public UserApiServiceTests()
		{
			_mockClient = new Mock<IUserApiClient>();
			_service = new UserApiService(_mockClient.Object);
		}

		[Fact]
		public async Task CheckIfUserExists_UserExists_ReturnsSuccess()
		{
			// Arrange
			_mockClient.Setup(c => c.GetUserByIdAsync(1)).ReturnsAsync(new UserModel());

			// Act
			var result = await _service.CheckIfUserExists(1);

			// Assert
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task CheckIfUserExists_UserDoesNotExist_ReturnsNotFound()
		{
			// Arrange
			_mockClient.Setup(c => c.GetUserByIdAsync(1)).ReturnsAsync((UserModel)null);

			// Act
			var result = await _service.CheckIfUserExists(1);

			// Assert
			Assert.False(result.Successful);
			Assert.Equal(ResultErrorType.NotFound, result.ErrorType);
		}
	}
}

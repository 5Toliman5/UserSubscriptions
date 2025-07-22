using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Application.Services;
using AwesomeProject.Projects.Domain.Entities;
using Moq;

namespace AwesomeProject.Projects.Tests
{
	public class UserSettingsServiceTests
	{
		private readonly Mock<IUserSettingsRepository> _mockRepo;
		private readonly UserSettingsService _service;

		public UserSettingsServiceTests()
		{
			_mockRepo = new Mock<IUserSettingsRepository>();
			_service = new UserSettingsService(_mockRepo.Object);
		}

		[Fact]
		public async Task CreateAsync_WhenAlreadyExists_ReturnsConflict()
		{
			// Arrange
			_mockRepo.Setup(r => r.GetByUserIdAsync(1)).ReturnsAsync(new UserSettings());

			// Act
			var result = await _service.CreateAsync(new UserSettingsModel { UserId = 1 });

			// Assert
			Assert.False(result.Successful);
			Assert.Equal(ResultErrorType.Conflict, result.ErrorType);
		}
	}
}

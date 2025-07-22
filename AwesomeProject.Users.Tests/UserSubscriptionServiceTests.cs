using AwesomeProject.Common.Result;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Application.Services;
using AwesomeProject.Users.Domain.Entities;
using Moq;
namespace AwesomeProject.Users.Tests
{
	public class UserSubscriptionServiceTests
	{
		private readonly Mock<IUserRepository> _mockUserRepo;
		private readonly Mock<ISubscriptionRepository> _mockSubRepo;
		private readonly UserSubscriptionService _service;

		public UserSubscriptionServiceTests()
		{
			_mockUserRepo = new Mock<IUserRepository>();
			_mockSubRepo = new Mock<ISubscriptionRepository>();
			_service = new UserSubscriptionService(_mockUserRepo.Object, _mockSubRepo.Object);
		}

		[Fact]
		public async Task SubscribeUserAsync_UserOrSubscriptionNotFound_ReturnsNotFound()
		{
			// Arrange
			_mockUserRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User)null);

			// Act
			var result = await _service.SubscribeUserAsync(1, 1);

			// Assert
			Assert.False(result.Successful);
			Assert.Equal(ResultErrorType.NotFound, result.ErrorType);
		}
	}
}

using AwesomeProject.Common.Result;
using AwesomeProject.Users.Application.Models;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Application.Services;
using Moq;

namespace AwesomeProject.Users.Tests
{
	public class SubscriptionServiceTests
	{
		private readonly Mock<ISubscriptionRepository> _mockRepository;
		private readonly SubscriptionService _service;

		public SubscriptionServiceTests()
		{
			_mockRepository = new Mock<ISubscriptionRepository>();
			_service = new SubscriptionService(_mockRepository.Object);
		}

		[Fact]
		public async Task AddAsync_ValidInput_ReturnsSuccess()
		{
			// Arrange
			var model = new SubscriptionAddModel
			{
				Type = "free",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddDays(30)

			};

			// Act
			var result = await _service.AddAsync(model);

			// Assert
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task AddAsync_InvalidEnum_ReturnsValidationError()
		{
			// Arrange
			var model = new SubscriptionAddModel
			{
				Type = "incorrect",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddDays(30)

			};
			// Act
			var result = await _service.AddAsync(model);

			// Assert
			Assert.False(result.Successful);
			Assert.Equal(ResultErrorType.Validation, result.ErrorType);
		}
	}
}

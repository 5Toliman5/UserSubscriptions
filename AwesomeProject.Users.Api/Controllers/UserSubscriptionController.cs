using AwesomeProject.Common.Api;
using AwesomeProject.Users.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Users.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserSubscriptionController : BaseApiController
	{
		private readonly IUserSubscriptionService _service;

		public UserSubscriptionController(IUserSubscriptionService service)
		{
			_service = service;
		}

		[HttpPost("subscribe/{userId}/to/{subscriptionId}")]
		public async Task<IActionResult> SubscribeUser(int userId, int subscriptionId)
		{
			var result = await _service.SubscribeUserAsync(userId, subscriptionId);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok();
		}

		[HttpGet("getUsersBySubscriptionType")]
		public async Task<IActionResult> GetUserIdsBySubscriptionType([FromQuery] string subscriptionType)
		{
			var result = await _service.GetUserIdsBySubscriptionType(subscriptionType);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok(result.Data);
		}
	}
}

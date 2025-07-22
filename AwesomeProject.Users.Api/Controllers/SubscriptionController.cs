using AwesomeProject.Common.Api;
using AwesomeProject.Users.Application.Models;
using AwesomeProject.Users.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Users.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionController : BaseApiController
	{
		private readonly ISubscriptionService _service;

		public SubscriptionController(ISubscriptionService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _service.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _service.GetByIdAsync(id);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Add(SubscriptionAddModel request)
		{
			var result = await _service.AddAsync(request);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok();
		}

		[HttpPatch]
		public async Task<IActionResult> Update(SubscriptionEditModel request)
		{
			var result = await _service.UpdateAsync(request);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _service.DeleteAsync(id);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok();
		}
	}
}

using AwesomeProject.Common.Api;
using AwesomeProject.Users.Application.Models;
using AwesomeProject.Users.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Users.Api.Controllers
{
	[Route("api/[controller]")]
	public class UserController : BaseApiController
	{
		private readonly IUserService _service;

		public UserController(IUserService userService)
		{
			_service = userService;
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

			return Ok(result.Data);
		}

		[HttpPost]
		public async Task<IActionResult> Add(UserAddModel request)
		{
			var result = await _service.AddAsync(request);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok(result.Data);
		}

		[HttpPatch]
		public async Task<IActionResult> Update(UserEditModel request)
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

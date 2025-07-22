using AwesomeProject.Common.Api;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Projects.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserSettingsController : BaseApiController
	{
		private readonly IUserSettingsService _userSettingsService;
		private readonly IUserApiService _userApiService;

		public UserSettingsController(IUserSettingsService userSettingsService, IUserApiService userApiService)
		{
			_userSettingsService = userSettingsService;
			_userApiService = userApiService;
		}

		[HttpGet("{userId:int}")]
		public async Task<IActionResult> GetByUserId(int userId)
		{
			var result = await _userSettingsService.GetByUserIdAsync(userId);
			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserSettingsModel request)
		{
			var checkUserResult = await _userApiService.CheckIfUserExists(request.UserId);
			if (!checkUserResult.Successful)
			{
				return GetFailureResult(checkUserResult);
			}

			var createResult = await _userSettingsService.CreateAsync(request);
			if (!createResult.Successful)
			{
				return GetFailureResult(createResult);
			}

			return Created();
		}

		[HttpPut]
		public async Task<IActionResult> Update(UserSettingsEditModel request)
		{
			var checkUserResult = await _userApiService.CheckIfUserExists(request.UserId);
			if (!checkUserResult.Successful)
			{
				return GetFailureResult(checkUserResult);
			}

			await _userSettingsService.UpdateAsync(request);
			return NoContent();
		}
	}
}

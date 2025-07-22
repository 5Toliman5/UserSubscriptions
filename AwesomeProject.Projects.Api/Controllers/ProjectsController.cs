using AwesomeProject.Common.Api;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Projects.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectsController : BaseApiController
	{
		private readonly IProjectService _projectService;
		private readonly IUserApiService _userApiService;
		public ProjectsController(IProjectService projectService, IUserApiService userApiService)
		{
			_projectService = projectService;
			_userApiService = userApiService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _projectService.GetByIdAsync(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet("user/{userId:int}")]
		public async Task<IActionResult> GetByUserId(int userId)
		{
			var result = await _projectService.GetByUserIdAsync(userId);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProjectModel request)
		{
			var checkUserResult = await _userApiService.CheckIfUserExists(request.UserId);
			if (!checkUserResult.Successful)
			{
				return GetFailureResult(checkUserResult);
			}

			await _projectService.CreateAsync(request);
			return Created();
		}

		[HttpPut]
		public async Task<IActionResult> Update(ProjectEditModel request)
		{
			await _projectService.UpdateAsync(request);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _projectService.DeleteAsync(id);
			return NoContent();
		}
	}
}

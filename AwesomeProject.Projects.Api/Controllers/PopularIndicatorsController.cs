using AwesomeProject.Common.Api;
using AwesomeProject.Projects.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Projects.Api.Controllers
{
	public class PopularIndicatorsController : BaseApiController
	{
		[HttpGet("{sybscriptionType}")]
		public async Task<IActionResult> GetSymbols(string sybscriptionType, [FromServices] IMetricsService service)
		{
			var result = await service.GetMostUsedIndicatorsBySubscription(sybscriptionType);
			if (!result.Successful)
			{
				return GetFailureResult(result);
			}

			return Ok(result.Data);
		}
	}
}

using AwesomeProject.Common.Api;
using AwesomeProject.Common.Utilities;
using AwesomeProject.Projects.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Projects.Api.Controllers
{
	public class ReferenceBookController : BaseApiController
	{
		[HttpGet("symbols")]
		public ActionResult GetSymbols()
		{
			return Ok(EnumAttributeUtility.GetEnumDescriptions<Symbol>());
		}

		[HttpGet("timeframes")]
		public ActionResult GetTimeframes()
		{
			return Ok(EnumAttributeUtility.GetEnumDescriptions<Timeframe>());
		}

		[HttpGet("indicatorNames")]
		public ActionResult GetIndicatorNames()
		{
			return Ok(EnumAttributeUtility.GetEnumDescriptions<IndicatorName>());
		}
	}
}

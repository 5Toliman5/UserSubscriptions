using AwesomeProject.Common.Api;
using AwesomeProject.Common.Utilities;
using AwesomeProject.Users.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Users.Api.Controllers
{
	public class ReferenceBookController : BaseApiController
	{
		[HttpGet("subscriptionTypes")]
		public ActionResult Symbols()
		{
			return Ok(EnumAttributeUtility.GetEnumDescriptions<SubscriptionType>());
		}
	}
}

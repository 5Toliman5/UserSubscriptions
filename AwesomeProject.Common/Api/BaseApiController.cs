using AwesomeProject.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProject.Common.Api
{
	public abstract class BaseApiController : ControllerBase
	{
		protected IActionResult GetFailureResult(IResult result)
		{
			return result.ErrorType switch
			{
				ResultErrorType.NotFound => NotFound(new ErrorResponse(result.ErrorDescription)),
				ResultErrorType.Validation or ResultErrorType.Conflict => UnprocessableEntity(new ErrorResponse(result.ErrorDescription)),
				_ => BadRequest(new ErrorResponse(result.ErrorDescription)),
			};
		}

		private record ErrorResponse(string ErrorMessage);
	}
}

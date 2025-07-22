namespace AwesomeProject.Common.Result
{
	public abstract class ResultBase<T> : IResult
		where T : ResultBase<T>, new()
	{
		public bool Successful { get; private init; }

		public string ErrorDescription { get; private init; }

		public ResultErrorType ErrorType { get; private init; }

		public static T Success()
		{
			return new T { Successful = true };
		}

		public static T Failure(ResultErrorType errorType)
		{
			return new T { ErrorType = errorType };
		}

		public static T Failure(ResultErrorType errorType, string errorDescription)
		{
			return new T { ErrorType = errorType, ErrorDescription = errorDescription };
		}

		public static T Failure(IResult result)
		{
			return Failure(result.ErrorType, result.ErrorDescription);
		}
	}
}

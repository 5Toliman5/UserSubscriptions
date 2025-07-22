namespace AwesomeProject.Common.Result
{
	public class Result : ResultBase<Result>
	{
	}

	public sealed class Result<T> : ResultBase<Result<T>>
	{
		public T Data { get; set; }

		public static Result<T> Success(T data)
		{
			var result = Success();
			result.Data = data;
			return result;
		}

		public static Result<T> Failure(T data, ResultErrorType errorType, string errorDescription)
		{
			var result = Failure(errorType, errorDescription);
			result.Data = data;
			return result;
		}
	}
}

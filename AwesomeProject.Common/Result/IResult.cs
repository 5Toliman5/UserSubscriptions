namespace AwesomeProject.Common.Result
{
	public interface IResult
	{
		bool Successful { get; }

		string ErrorDescription { get; }

		ResultErrorType ErrorType { get; }
	}

	public enum ResultErrorType
	{
		NotFound = 1,

		Validation = 2,

		Conflict = 3
	}
}

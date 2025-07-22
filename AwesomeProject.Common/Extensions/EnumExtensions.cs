using System.ComponentModel;

namespace AwesomeProject.Common.Extensions
{
	public static class EnumExtensions
	{
		public static bool TryGetDescription(this Enum value, out string description)
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);

			var attribute = type
				.GetField(name ?? string.Empty)?.GetCustomAttributes(typeof(DescriptionAttribute), false)
				.FirstOrDefault() as DescriptionAttribute;
			if (attribute == null)
			{
				description = default;
				return false;
			}

			description = attribute.Description;
			return true;
		}

		public static string GetDescription(this Enum value)
		{
			if (!TryGetDescription(value, out var description))
			{
				throw new ArgumentException($"Description of {value} was not found.", nameof(value));
			}

			return description;
		}
	}
}

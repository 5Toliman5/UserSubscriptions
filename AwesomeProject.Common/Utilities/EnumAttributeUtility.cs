using System.ComponentModel;
using System.Reflection;

namespace AwesomeProject.Common.Utilities
{
	public static class EnumAttributeUtility
	{
		public static bool TryGetValueFromDescription<T>(string description, out T value)
		{
			var type = typeof(T);

			if (!type.IsEnum)
			{
				throw new InvalidOperationException("type must be enum");
			}

			foreach (var field in type.GetFields())
			{
				if (Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
				{
					if (attribute.Description == description)
					{
						value = (T)field.GetValue(null);
						return true;
					}
				}
				else
				{
					if (field.Name == description)
					{
						value = (T)field.GetValue(null);
						return true;
					}
				}
			}

			value = default;
			return false;
		}

		public static T GetValueFromDescription<T>(string description)
		{
			if (!TryGetValueFromDescription<T>(description, out var value))
			{
				throw new ArgumentException($"Not found {description}.", nameof(description));
			}

			return value;
		}

		public static List<string> GetEnumDescriptions<T>() where T : Enum
		{
			return Enum.GetValues(typeof(T))
				.Cast<Enum>()
				.Select(e => e.GetDescription())
				.ToList();
		}

		private static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attr = field?.GetCustomAttribute<DescriptionAttribute>();
			return attr?.Description ?? value.ToString();
		}
	}
}

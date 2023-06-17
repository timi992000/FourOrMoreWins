using System;
using System.Text;

namespace FourOrMoreWins.Core.Extender
{
	public static class StringExtender
	{
		public static string ToStringValue(this object rawValue)
		{
			if (rawValue == null)
				return string.Empty;
			else if (rawValue is string)
				return (string)rawValue;
			else
				return rawValue.ToString();
		}

		public static bool IsNotNullOrEmpty(this string value)
				=> !string.IsNullOrEmpty(value);
		public static bool IsNullOrEmpty(this string value)
				=> string.IsNullOrEmpty(value);

		public static byte[] ToByteArray(this string value)
				=> Encoding.UTF8.GetBytes(value);

	}
}

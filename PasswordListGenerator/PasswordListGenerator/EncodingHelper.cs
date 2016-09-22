using System;
using System.Text;

namespace PasswordListGenerator
{
	public static class EncodingHelper
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static Encoding TryGetEncoding(string encoding)
		{
			try
			{
				return Encoding.GetEncoding(encoding);
			}
			catch (ArgumentException)
			{
				Logger.WarnAndPrint($"Can't using {encoding} encoding. Fallback to utf-8");
				return Encoding.UTF8;
			}
		}
	}
}

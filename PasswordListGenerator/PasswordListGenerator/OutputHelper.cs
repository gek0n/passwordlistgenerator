using System;
using System.Collections.Generic;

namespace PasswordListGenerator
{
	public static class OutputHelper
	{
		public static void WriteToStream(IEnumerable<string> collection, Action<string> write)
		{
			foreach (var s in collection)
			{
				write(s);
			}
		}
	}
}

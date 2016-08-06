using System;
using System.IO;

namespace PasswordListGeneratorTest
{
	public class ConsoleInput : IDisposable
	{
		private readonly StringReader _stringReader;
		private readonly TextReader _originalInput;

		public ConsoleInput(string s)
		{
			_stringReader = new StringReader(s);
			_originalInput = Console.In;
			Console.SetIn(_stringReader);
		}

		public void SetInput(string s)
		{
			Console.SetIn(new StringReader(s));
		}

		public void Dispose()
		{
			Console.SetIn(_originalInput);
			_stringReader.Dispose();
		}
	}
}
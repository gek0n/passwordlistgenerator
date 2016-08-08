// Copyright 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using System.IO;

namespace PasswordListGeneratorTest
{
	public class ConsoleOutput : IDisposable
	{
		private readonly StringWriter _stringWriter;
		private readonly TextWriter _originalOutput;

		public ConsoleOutput()
		{
			_stringWriter = new StringWriter();
			_originalOutput = Console.Out;
			Console.SetOut(_stringWriter);
		}

		public string GetOuput()
		{
			return _stringWriter.ToString();
		}

		public void Dispose()
		{
			Console.SetOut(_originalOutput);
			_stringWriter.Dispose();
		}
	}
}
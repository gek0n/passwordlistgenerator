using System;
using System.IO;
using CommandLine;
using NUnit.Framework;
using PasswordListGenerator;

namespace PasswordListGeneratorTest
{
	[TestFixture]
	public class SubstitutionTests
	{
		[SetUp]
		public void Init()
		{
			
		}

		[Test]
		public void SourceWordEmptyNoParams_ShouldReturnEmptyString()
		{
			var args = new []
			{
				"subs"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = string.Empty;
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		public void SourceWordEmptyWithParams_ShouldReturnEmptyString()
		{
			var args = new[]
			{
				"subs",
				"-d",
				"Test.json",
				"--ignore-case",
				"-m",
				"MadLeet",
				"-o",
				"Test.txt",
				"--in-encoding",
				"test",
				"--out-encoding",
				"test"
			};

			var subsOptions = ParseSubOptions(args);
			var expected = string.Empty;
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		public void SourceWordOneLetterNoParams_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = $"C\r\n" +
							$"[\r\n" +
							$"{{\r\n" +
							$"(\r\n" +
							$"<\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		private static SubstituteSubOptions ParseSubOptions(string[] args)
		{
			object invokedVerbInstance = null;

			var options = new Options();
			if (!Parser.Default.ParseArguments(args, options, (verb, subOptions) => { invokedVerbInstance = subOptions; }))
			{
				Assert.Fail();
			}

			return (SubstituteSubOptions) invokedVerbInstance;
		}
	}

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
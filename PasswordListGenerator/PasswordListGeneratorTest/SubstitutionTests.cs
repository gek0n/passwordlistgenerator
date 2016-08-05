using System;
using System.IO;
using System.Text;
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
		[Category("SourceWord")]
		public void SourceWordEmptyNoParams_ShouldReturnErrorAndHelp()
		{
			var args = new[]
			{
				"subs"
			};
			var subsOptions = ParseSubOptions(args);
			
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("SourceWord")]
		public void SourceWordOneSymbolNotInDict_ShouldReturnErrorAndHelp()
		{
			var args = new[]
			{
				"subs",
				"{"
			};
			var subsOptions = ParseSubOptions(args);

			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("SourceWord")]
		public void SourceWordEmptyWithParams_ShouldReturnErrorAndHelp()
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
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("SourceWord")]
		public void SourceWordOneLetterNoParams_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
			               "[\r\n" +
			               "{\r\n" +
			               "(\r\n" +
			               "<\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("SourceWord")]
		public void SourceWordThreeLetterNoParams_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"BZS"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "BZS\r\n" +
			               "|3ZS\r\n" +
			               "8ZS\r\n" +
			               "B2S\r\n" +
			               "B7_S\r\n" +
			               "|32S\r\n" +
			               "|37_S\r\n" +
			               "82S\r\n" +
			               "87_S\r\n" +
			               "BZ5\r\n" +
			               "BZ$\r\n" +
			               "|3Z5\r\n" +
			               "|3Z$\r\n" +
			               "8Z5\r\n" +
			               "8Z$\r\n" +
			               "B25\r\n" +
			               "B2$\r\n" +
			               "B7_5\r\n" +
			               "B7_$\r\n" +
			               "|325\r\n" +
			               "|32$\r\n" +
			               "|37_5\r\n" +
			               "|37_$\r\n" +
			               "825\r\n" +
			               "82$\r\n" +
			               "87_5\r\n" +
			               "87_$\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("IgnoreCase")]
		public void SourceWordThreeLetterIgnoreCase_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"bzs",
				"--ignore-case"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "bzs\r\n" +
			               "|3zs\r\n" +
			               "8zs\r\n" +
			               "b2s\r\n" +
			               "b7_s\r\n" +
			               "|32s\r\n" +
			               "|37_s\r\n" +
			               "82s\r\n" +
			               "87_s\r\n" +
			               "bz5\r\n" +
			               "bz$\r\n" +
			               "|3z5\r\n" +
			               "|3z$\r\n" +
			               "8z5\r\n" +
			               "8z$\r\n" +
			               "b25\r\n" +
			               "b2$\r\n" +
			               "b7_5\r\n" +
			               "b7_$\r\n" +
			               "|325\r\n" +
			               "|32$\r\n" +
			               "|37_5\r\n" +
			               "|37_$\r\n" +
			               "825\r\n" +
			               "82$\r\n" +
			               "87_5\r\n" +
			               "87_$\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("IgnoreCase")]
		public void SourceWordThreeLetterLowercase_ShouldReturnErrorAndHelp()
		{
			var args = new[]
			{
				"subs",
				"bzs"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("SourceWord")]
		public void SourceWordThreeSymbolsNotFromDict_ShouldReturnErrorAndHelp()
		{
			var args = new[]
			{
				"subs",
				"^&*"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordOneLetterPronunMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-m",
				"Pronunciation"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
			               "cee\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordThreeLetterPronunMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"BZS",
				"-m",
				"Pronunciation"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "BZS\r\n" +
			               "beeZS\r\n" +
			               "BzedS\r\n" +
			               "beezedS\r\n" +
			               "BZess\r\n" +
			               "beeZess\r\n" +
			               "Bzedess\r\n" +
			               "beezedess\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordOneLetterMadLeetMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-m",
				"MadLeet"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
			               "[\r\n" +
			               "¢\r\n" +
			               "{\r\n" +
			               "<\r\n" +
			               "(\r\n" +
			               "©\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordThreeLetterMadLeetMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"BZS",
				"-m",
				"MadLeet"
			};
			var subsOptions = ParseSubOptions(args);

			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(1041, consoleOutput.GetOuput().Split('\n').Length);
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordOneLetterCyrillicMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-m",
				"Cyrillic"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
			               "С\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Methods")]
		public void SourceWordThreeLetterCyrillicMethod_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"ABC",
				"-m",
				"Cyrillic"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "ABC\r\n" +
							"АBC\r\n" +
							"AВC\r\n" +
							"АВC\r\n" +
							"ABС\r\n" +
							"АBС\r\n" +
							"AВС\r\n" +
							"АВС\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void EmptyDict_ShouldReturnDefaultSubstitutions()
		{
			var file = File.Create("testDict1.txt");
			file.Close();
			var args = new[]
			{
				"subs",
				"C",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
						   "[\r\n" +
						   "{\r\n" +
						   "(\r\n" +
						   "<\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void NonexistentDict_ShouldReturnDefaultSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
						   "[\r\n" +
						   "{\r\n" +
						   "(\r\n" +
						   "<\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void DictWithGoodLeetOnly_ShouldReturnSubstitutions()
		{
			var content = "{\r\n\t\"GoodLeet\": {\r\n\t\t\"C\": [\r\n\t\t\t\"[\",\r\n\t\t\t\"(\"\r\n\t\t]\r\n\t}\r\n}";
			File.WriteAllText("testDict1.txt", content, Encoding.UTF8);
			
			var args = new[]
			{
				"subs",
				"C",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n[\r\n(\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void DictWithAllMethods_ShouldReturnSubstitutions()
		{
			var content = "{\"GoodLeet\": {\"C\": [\"[\", \"(\" ]}, \"MadLeet\": {\"C\": [\"c\"]}, \"Cyrillic\": {\"C\": [ \"С\" ]}}";
			File.WriteAllText("testDict1.txt", content, Encoding.UTF8);
			var args = new[]
			{
				"subs",
				"C",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n[\r\n(\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void DictWithGoodLeetOnlySymbols_ShouldReturnSubstitutions()
		{
			var content = "{\r\n\t\"GoodLeet\": {\r\n\t\t\"{\": [\r\n\t\t\t\"[\",\r\n\t\t\t\"(\"\r\n\t\t]\r\n\t}\r\n}";
			File.WriteAllText("testDict1.txt", content, Encoding.UTF8);
			var args = new[]
			{
				"subs",
				"{",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "{\r\n[\r\n(\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Dictionary")]
		public void DictWithWrongJsonSyntax_ShouldReturnErrorAndHelp()
		{
			var content = "{\r\n\t\"good-leet\": \r\n\t\t\"{\": [\r\n\t\t\t\"[\",\r\n\t\t\t\"(\"\r\n\t\t]\r\n\t}\r\n}";
			File.WriteAllText("testDict1.txt", content, Encoding.UTF8);
			var args = new[]
			{
				"subs",
				"{",
				"-d",
				"testDict1.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);

				Assert.That(consoleText.Contains("--ignore-case"), Is.True);
				Assert.That(consoleText.Contains("--out-file"), Is.True);
				Assert.That(consoleText.Contains("--dict"), Is.True);
			}
		}

		[Test]
		[Category("Methods")]
		public void MethodInWrongCase_ShouldReturnSubstitutions()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-m",
				"prOnunCiatiON"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
						   "cee\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Methods")]
		public void WrongMethod_ShouldReturnSubstitutionsFromFirstAvailableMethod()
		{
			var args = new[]
			{
				"subs",
				"C",
				"-m",
				"balabala"
			};
			var subsOptions = ParseSubOptions(args);
			var expected = "C\r\n" +
						   "С\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.AreEqual(expected, consoleOutput.GetOuput());
			}
		}

		[Test]
		[Category("Output")]
		public void OutputFileNotSpecified_ShouldPrintInConsole()
		{
			var args = new[]
			{
				"subs",
				"B"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("B\r\n|3\r\n8\r\n"), Is.True);
			}
		}

		[Test]
		[Category("Output")]
		public void OutputFileSpecified_ShouldPrintInFile()
		{
			var args = new[]
			{
				"subs",
				"B",
				"-o",
				"outputTest.txt"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			subsInstance.Process();
			var text = File.ReadAllText("outputTest.txt");
			File.Delete("outputTest.txt");
			Assert.That(text.Contains("B\r\n|3\r\n8\r\n"), Is.True);
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
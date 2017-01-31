// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using CommandLine;
using NUnit.Framework;
using PasswordListGenerator;
using PasswordListGenerator.Substitutions;

namespace PasswordListGeneratorTest
{
	[TestFixture]
	public class SubstitutionTests
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		[SetUp]
		public void Init()
		{
			Thread.Sleep(2000);
		}

		#region SourceWordTests

		[Test, TestCaseSource(typeof(SourceWordTestArguments), nameof(SourceWordTestArguments.ErrorTestCases))]
		[Category("SourceWord")]
		public void SourceWord_ShouldReturnErrorAndHelp(string[] args)
		{
			var subsOptions = ParseSubOptions(args);
			
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				try
				{
					subsInstance.Process();
				}
				catch (VerbOptionException e)
				{
					Logger.ErrorAndPrint(e.Message);
				}
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);
			}
		}

		[Test, TestCaseSource(typeof(SourceWordTestArguments), nameof(SourceWordTestArguments.OkTestCases))]
		[Category("SourceWord")]
		public void SourceWord_ShouldReturnSubstitutions(string[] args, string expected)
		{
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
			}
		}

		private class SourceWordTestArguments
		{
			public static IEnumerable ErrorTestCases
			{
				get
				{
					yield return new TestCaseData(arg: new[] {"subs"})
						.SetCategory("SourceWord")
						.SetName("Empty source word");

					yield return new TestCaseData(arg: new[] {"subs", "{"})
						.SetCategory("SourceWord")
						.SetName("One symbol source word not in dict");

					yield return new TestCaseData(arg: new[]
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
					})
						.SetCategory("SourceWord")
						.SetName("With parameters and without source word");

					yield return new TestCaseData(arg: new[] {"subs", "^&*"})
						.SetCategory("SourceWord")
						.SetName("Three symbols source word not in dict");
				}
			}

			public static IEnumerable OkTestCases
			{
				get
				{
					yield return new TestCaseData(
							arg1: new[] {"subs", "C"}, 
							arg2: "C\r\n[\r\n{\r\n(\r\n<\r\n")
						.SetCategory("SourceWord")
						.SetName("Source word in one symbol");
					yield return new TestCaseData(
							arg1: new[] {"subs", "BZS"}, 
							arg2: "BZS\r\n|3ZS\r\n8ZS\r\nB2S\r\nB7_S\r\n"
									+"|32S\r\n|37_S\r\n82S\r\n87_S\r\nBZ5\r\nBZ$\r\n|3Z5\r\n"
									+"|3Z$\r\n8Z5\r\n8Z$\r\nB25\r\nB2$\r\nB7_5\r\nB7_$\r\n"
									+"|325\r\n|32$\r\n|37_5\r\n|37_$\r\n825\r\n" +
									"82$\r\n87_5\r\n87_$\r\n")
						.SetCategory("SourceWord")
						.SetName("Source word in three symbols");
				}
			}
		};

		#endregion

		#region IgnoreCaseTests

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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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
			}
		}

		#endregion

		#region MethodsTests

		[Test, TestCaseSource(typeof(MethodTestArguments), nameof(MethodTestArguments.OkTestCases))]
		[Category("Methods")]
		public string Methods_ShouldReturnSubstitutions(string[] args)
		{
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				return consoleOutput.GetOuput();
			}
		}

		private class MethodTestArguments
		{
			public static IEnumerable OkTestCases
			{
				get
				{
					yield return new TestCaseData(arg: new[] { "subs", "C", "-m", "Pronunciation" })
						.Returns("C\r\ncee\r\n")
						.SetCategory("Methods")
						.SetName("One letter pronunciation");
					yield return new TestCaseData(arg: new[] { "subs", "BZS", "-m", "Pronunciation" })
						.Returns("BZS\r\nbeeZS\r\nBzedS\r\nbeezedS\r\nBZess\r\nbeeZess\r\nBzedess\r\nbeezedess\r\n")
						.SetCategory("Methods")
						.SetName("Three letter pronunciation");

					yield return new TestCaseData(arg: new[] { "subs", "C", "-m", "MadLeet" })
						.Returns("C\r\n[\r\n¢\r\n{\r\n<\r\n(\r\n©\r\n")
						.SetCategory("Methods")
						.SetName("One letter mad leet");

					yield return new TestCaseData(arg: new[] { "subs", "C", "-m", "Cyrillic" })
						.Returns("C\r\nС\r\n")
						.SetCategory("Methods")
						.SetName("One letter cyrillic");
					yield return new TestCaseData(arg: new[] { "subs", "ABC", "-m", "Cyrillic" })
						.Returns("ABC\r\nАBC\r\nAВC\r\nАВC\r\nABС\r\nАBС\r\nAВС\r\nАВС\r\n")
						.SetCategory("Methods")
						.SetName("Three letter cyrillic");

					yield return new TestCaseData(arg: new[] { "subs", "C", "-m", "prOnunCiatiON" })
						.Returns("C\r\ncee\r\n")
						.SetCategory("Methods")
						.SetName("Method in wrong case");
				}
			}
		}

		[Test]
		[Category("Methods")]
		public void MadLeetThreeSymbols_ShouldReturnSubstitutions()
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
							"[\r\n" +
							"{\r\n" +
							"(\r\n" +
							"<\r\n";
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
			}
		}

		#endregion

		#region DictionaryTests

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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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
				Assert.That(consoleOutput.GetOuput().Contains(expected), Is.True);
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

			using (var consoleOutput = new ConsoleOutput())
			{
				try
				{
					var subsOptions = ParseSubOptions(args);
					var subsInstance = new Substitution(subsOptions);
				}
				catch (VerbOptionException e)
				{
					Logger.ErrorAndPrint(e.Message);
				}

				File.Delete("testDict1.txt");
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);
			}
		}

		#endregion

		#region OutputTests

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

		#endregion

		#region StdInputTests

		[Test]
		[Category("Input")]
		public void StdInputWithSourceWord_ShouldIgnoreSourceWord()
		{
			var args = new[]
			{
				"subs",
				"B",
				"-i"
			};
			var subsOptions = ParseSubOptions(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				var subsInstance = new Substitution(subsOptions);
				subsInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals(""), Is.True);
			}
		}

		[Test]
		[Category("Input")]
		public void StdInputThreeSymbolsInput_ShouldPrintSubstitution()
		{
			var args = new[]
			{
				"subs",
				"-i"
			};
			var subsOptions = ParseSubOptions(args);
			using (new ConsoleInput("B\r\nC\r\n"))
			{
				using (var consoleOutput = new ConsoleOutput())
				{
					var subsInstance = new Substitution(subsOptions);
					subsInstance.Process();
					var consoleText = consoleOutput.GetOuput();
					Assert.That(consoleText.Equals("B\r\n|3\r\n8\r\nC\r\n[\r\n{\r\n(\r\n<\r\n"));
				}
			}
		}

		[Test]
		[Category("Input")]
		public void StdInputOneWrongSymbolInput_ShouldPrintErrorAndContinueWork()
		{
			var args = new[]
			{
				"subs",
				"-i"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleInput = new ConsoleInput("q\r\n"))
			{
				using (var consoleOutput = new ConsoleOutput())
				{
					subsInstance.Process();
					var consoleText = consoleOutput.GetOuput();
					Assert.That(consoleText.Contains("[ERROR]"));
					consoleInput.SetInput("B\r\n");
					subsInstance.Process();
					consoleText = consoleOutput.GetOuput();
					Assert.That(consoleText.Contains("B\r\n|3\r\n8\r\n"));
				}
			}
		}

		#endregion

		#region Verbose

		[Test]
		[Category("Vebose")]
		public void StdDictMethodAndEncoding_ShouldPrintVerbose()
		{
			var args = new[]
			{
				"subs",
				"QW",
				"-v"
			};
			var subsOptions = ParseSubOptions(args);
			using (new ConsoleInput("B\r\nC\r\n"))
			{
				using (var consoleOutput = new ConsoleOutput())
				{
					var subsInstance = new Substitution(subsOptions);
					subsInstance.Process();
					var consoleText = consoleOutput.GetOuput();
					Assert.That(consoleText.Contains($"[METHOD]: GoodLeet{Environment.NewLine}"));
					Assert.That(consoleText.Contains($"[DICTIONARY]: default{Environment.NewLine}"));
				}
			}
		}

		[Test]
		[Category("Verbose")]
		public void VerboseDictWithGoodLeetOnly_ShouldPrintVerbose()
		{
			var content = "{\r\n\t\"GLeet\": {\r\n\t\t\"C\": [\r\n\t\t\t\"[\",\r\n\t\t\t\"(\"\r\n\t\t]\r\n\t}\r\n}";
			File.WriteAllText("testDict1.txt", content, Encoding.UTF8);

			var args = new[]
			{
				"subs",
				"C",
				"-d",
				"testDict1.txt",
				"-v"
			};
			var subsOptions = ParseSubOptions(args);
			var subsInstance = new Substitution(subsOptions);
			using (var consoleOutput = new ConsoleOutput())
			{
				subsInstance.Process();
				File.Delete("testDict1.txt");
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains($"[DICTIONARY]: testDict1.txt{Environment.NewLine}"));
				Assert.That(consoleText.Contains($"[METHOD]: GLeet{Environment.NewLine}"));
			}
		}
		#endregion

		private static SubstituteSubOption ParseSubOptions(string[] args)
		{
			object invokedVerbInstance = null;

			var options = new Options();
			if (!Parser.Default.ParseArguments(args, options, (verb, subOptions) => { invokedVerbInstance = subOptions; }))
			{
				Assert.Fail();
			}

			return (SubstituteSubOption) invokedVerbInstance;
		}
	}
}
 
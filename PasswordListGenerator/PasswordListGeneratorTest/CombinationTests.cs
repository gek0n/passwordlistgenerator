using System;
using System.Collections;
using System.IO;
using CommandLine;
using NUnit.Framework;
using PasswordListGenerator;
using PasswordListGenerator.Combinations;

namespace PasswordListGeneratorTest
{
	[TestFixture]
	public class CombinationTests
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private const string EmptyFilename = "empty.txt";
		private const string CorruptFilename = "corrupt_filename.txt";
		private const string NonexistenFilename = "does_not_exits.txt";
		private const string NormalFilename1 = "normal1.txt";
		private const string NormalFilename2 = "normal2.txt";

		[OneTimeSetUp]
		public void Init()
		{
			var file = File.Create(EmptyFilename);
			file.Close();

			File.WriteAllText(CorruptFilename, $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
			File.WriteAllText(NormalFilename1, $"0{Environment.NewLine}1");
			File.WriteAllText(NormalFilename2, $"cat{Environment.NewLine}dog{Environment.NewLine}empty{Environment.NewLine}frog{Environment.NewLine}lazy");
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			File.Delete(EmptyFilename);
			File.Delete(CorruptFilename);
			File.Delete(NonexistenFilename);
			File.Delete(NormalFilename1);
			File.Delete(NormalFilename2);
		}

		#region In-file

		[Test, TestCaseSource(typeof(InFileTestArguments), nameof(InFileTestArguments.ErrorTestCases))]
		[Category("InFile")]
		public void InFile_ShouldReturnErrorAndHelp(string[] args)
		{
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				try
				{
					combInstance.Process();
				}
				catch (VerbOptionException e)
				{
					Logger.ErrorAndPrint(e.Message);
				}
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);
			}
		}

		[Test, TestCaseSource(typeof(InFileTestArguments), nameof(InFileTestArguments.NullTestCases))]
		[Category("InFile")]
		public void InFile_ShouldGetNullInstance(string[] args)
		{
			var combInstance = GetCombInstance(args);
			Assert.Null(combInstance);
		}

		[Test, TestCaseSource(typeof(InFileTestArguments), nameof(InFileTestArguments.OkTestCases))]
		[Category("InFile")]
		public void InFile_ShouldReturnCombinations(string[] args, string expected)
		{
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals(expected), Is.True);
			}
		}

		private class InFileTestArguments
		{
			public static IEnumerable ErrorTestCases
			{
				get
				{
					yield return new TestCaseData(arg: new[] { "comb", "-i", NonexistenFilename })
						.SetCategory("InFile")
						.SetName("File does not exist");

					yield return new TestCaseData(arg: new[] { "comb", "-i", EmptyFilename })
						.SetCategory("InFile")
						.SetName("File is empty");

					yield return new TestCaseData(arg: new[] { "comb", "-i", CorruptFilename })
						.SetCategory("InFile")
						.SetName("File with empty lines");
				}
			}

			public static IEnumerable NullTestCases
			{
				get
				{
					yield return new TestCaseData(arg: new[] { "comb" })
						.SetCategory("InFile")
						.SetName("Not specified input file");

					yield return new TestCaseData(arg: new[] { "comb", "-i" })
						.SetCategory("InFile")
						.SetName("Empty filename");
				}
			}

			public static IEnumerable OkTestCases
			{
				get
				{
					yield return new TestCaseData(
						arg1: new[] { "comb", "-i", NormalFilename1 },
						arg2: "0 1\r\n1 0\r\n")
						.SetCategory("InFile")
						.SetName("Normal file with short input");
					yield return new TestCaseData(
						arg1: new[] { "comb", "-i", NormalFilename2 },
						arg2: "cat dog\r\ncat empty\r\ncat frog\r\ncat lazy\r\ndog cat\r\ndog empty\r\n" +
							  "dog frog\r\ndog lazy\r\nempty cat\r\nempty dog\r\nempty frog\r\nempty lazy\r\n" +
							  "frog cat\r\nfrog dog\r\nfrog empty\r\nfrog lazy\r\nlazy cat\r\nlazy dog\r\n" +
							  "lazy empty\r\nlazy frog\r\n")
						.SetCategory("InFile")
						.SetName("Normal file with long input");
				}
			}
		}

		#endregion

		#region Out-File

		[Test]
		[Category("OutFile")]
		public void OutFile_ShouldGetNullInstance()
		{
			var args = new []
			{
				"comb",
				"-i",
				NormalFilename1,
				"-o"
			};
			var combInstance = GetCombInstance(args);
			Assert.Null(combInstance);
		}
		[Test]
		[Category("OutFile")]
		public void OutFile_FilenameIsBad_ShouldReturnErrorAndHelp()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"-o",
				"?:|\\"
			};
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				try
				{
					combInstance.Process();
				}
				catch (VerbOptionException e)
				{
					Logger.ErrorAndPrint(e.Message);
				}
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[ERROR]: "), Is.True);
			}
		}

		[Test]
		[Category("OutFile")]
		public void OutFile_ShouldReturnCombinations()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"-o",
				"outFile.txt"
			};
			const string expected = "0 1\r\n1 0\r\n";
			var combInstance = GetCombInstance(args);
			combInstance.Process();
			var text = File.ReadAllText("outFile.txt");
			File.Delete("outFile.txt");
			Assert.That(text.Equals(expected), Is.True);
		}

		#endregion

		#region Repetition

		[Test]
		[Category("Repetititon")]
		public void Repetititon_ShouldReturnCombinationsWithRepetititons()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"-r"
			};
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();

				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals("0 0\r\n0 1\r\n1 0\r\n1 1\r\n"), Is.True);
			}
		}

		#endregion

		#region Max-length

		[Test, TestCaseSource(typeof(MaxLengthTestArguments), nameof(MaxLengthTestArguments.NullTestCases))]
		[Category("MaxLength")]
		public void MaxLength_ShouldGetNullInstance(string[] args)
		{
			var combInstance = GetCombInstance(args);
			Assert.Null(combInstance);
		}

		[Test, TestCaseSource(typeof(MaxLengthTestArguments), nameof(MaxLengthTestArguments.OkTestCases))]
		[Category("MaxLength")]
		public void MaxLength_ShouldReturnCombinations(string[] args, string expected)
		{
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals(expected), Is.True);
			}
		}

		private class MaxLengthTestArguments
		{
			public static IEnumerable NullTestCases
			{
				get
				{
					yield return new TestCaseData(arg: new[] { "comb", "-i", NormalFilename1, "-max-length=1" })
						.SetCategory("MaxLength")
						.SetName("MaxLength is too small");

					yield return new TestCaseData(arg: new[] { "comb", "-i", NormalFilename1, "-max-length=100" })
						.SetCategory("MaxLength")
						.SetName("MaxLength is too big");

					yield return new TestCaseData(arg: new[] { "comb", "-i", NormalFilename1, "-max-length=5" })
						.SetCategory("MaxLength")
						.SetName("MaxLength more than count of entries without reprtititons");
				}
			}

			public static IEnumerable OkTestCases
			{
				get
				{
					yield return new TestCaseData(
						arg1: new[] { "comb", "-i", NormalFilename2, "--max-length=3" },
						arg2: "cat dog empty\r\ncat dog frog\r\ncat dog lazy\r\ncat empty dog\r\ncat empty frog\r\n" +
								"cat empty lazy\r\ncat frog dog\r\ncat frog empty\r\ncat frog lazy\r\ncat lazy dog\r\n" +
								"cat lazy empty\r\ncat lazy frog\r\ndog cat empty\r\ndog cat frog\r\ndog cat lazy\r\n" +
								"dog empty cat\r\ndog empty frog\r\ndog empty lazy\r\ndog frog cat\r\ndog frog empty\r\n" +
								"dog frog lazy\r\ndog lazy cat\r\ndog lazy empty\r\ndog lazy frog\r\nempty cat dog\r\n" +
								"empty cat frog\r\nempty cat lazy\r\nempty dog cat\r\nempty dog frog\r\nempty dog lazy\r\n" +
								"empty frog cat\r\nempty frog dog\r\nempty frog lazy\r\nempty lazy cat\r\nempty lazy dog\r\n" +
								"empty lazy frog\r\nfrog cat dog\r\nfrog cat empty\r\nfrog cat lazy\r\nfrog dog cat\r\n" +
								"frog dog empty\r\nfrog dog lazy\r\nfrog empty cat\r\nfrog empty dog\r\nfrog empty lazy\r\n" +
								"frog lazy cat\r\nfrog lazy dog\r\nfrog lazy empty\r\nlazy cat dog\r\nlazy cat empty\r\n" +
								"lazy cat frog\r\nlazy dog cat\r\nlazy dog empty\r\nlazy dog frog\r\nlazy empty cat\r\n" +
								"lazy empty dog\r\nlazy empty frog\r\nlazy frog cat\r\nlazy frog dog\r\nlazy frog empty\r\n")
						.SetCategory("OutFile")
						.SetName("Max length less than count of entries");

					yield return new TestCaseData(
						arg1: new[] { "comb", "-i", NormalFilename1, "--max-length=3", "-r" },
						arg2: "0 0 0\r\n0 0 1\r\n0 1 0\r\n0 1 1\r\n1 0 0\r\n1 0 1\r\n1 1 0\r\n1 1 1\r\n")
						.SetCategory("OutFile")
						.SetName("Max length more than count of entries with repetitions");
				}
			}
		}

		#endregion

		#region Delimiter

		[Test]
		[Category("Delimiter")]
		public void Delimiter_ShouldReturnCombinations()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"--delimiter=\"|\\|\""
			};
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals("0\"|\\|\"1\r\n1\"|\\|\"0\r\n"), Is.True);
			}

		}

		#endregion

		#region Suffix

		[Test]
		[Category("Suffix")]
		public void Suffix_ShouldReturnCombinations()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"--suffix=\"|\\|\""
			};
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals("0 1\"|\\|\"\r\n1 0\"|\\|\"\r\n"), Is.True);
			}

		}

		#endregion

		#region Prefix

		[Test]
		[Category("Prefix")]
		public void Prefix_ShouldReturnCombinations()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"--prefix=\"|\\|\""
			};
			var combInstance = GetCombInstance(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Equals("\"|\\|\"0 1\r\n\"|\\|\"1 0\r\n"), Is.True);
			}

		}

		#endregion

		#region Verbose

		[Test]
		[Category("Vebose")]
		public void Verbose_ShouldReturnCombinations()
		{
			var args = new[]
			{
				"comb",
				"-i",
				NormalFilename1,
				"-v"
			};
			var combOptions = ParseCombOptions(args);
			using (var consoleOutput = new ConsoleOutput())
			{
				var combInstance = new Combine(combOptions);
				combInstance.Process();
				var consoleText = consoleOutput.GetOuput();
				Assert.That(consoleText.Contains("[DELIMITER]: "));
				Assert.That(consoleText.Contains("[SUFFIX]: "));
				Assert.That(consoleText.Contains("[PREFIX]: "));
				Assert.That(consoleText.Contains($"[MAX LENGTH]: 2{Environment.NewLine}"));
			}

		}

		#endregion

		private static Combine GetCombInstance(string[] args)
		{
			CombineSubOption combOptions;
			try
			{
				combOptions = ParseCombOptions(args);
			}
			catch (AssertionException e)
			{
				Logger.ErrorAndPrint(e.Message);
				return null;
			}

			var combInstance = new Combine(combOptions);
			return combInstance;
		}

		private static CombineSubOption ParseCombOptions(string[] args)
		{
			object invokedVerbInstance = null;

			var options = new Options();
			if (!Parser.Default.ParseArguments(args, options, (verb, subOptions) => { invokedVerbInstance = subOptions; }))
			{
				Assert.Fail("Can't parse option Comb");
			}

			return (CombineSubOption)invokedVerbInstance;
		}
	}
}

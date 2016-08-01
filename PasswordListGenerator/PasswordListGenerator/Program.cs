using System;
using System.Text;
using CommandLine;

namespace PasswordListGenerator
{
	internal class Program
	{
		private static Encoding _inFileEncoding;
		private static Encoding _outFileEncoding;

		private static void Main(string[] args)
		{
			var options = new Options();
			string invokedVerb = null;
			object invokedVerbInstance = null;

			if (!Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
			{
				invokedVerb = verb;
				invokedVerbInstance = subOptions;
			}))
			{
				Environment.Exit(Parser.DefaultExitCodeFail);
			}

			if (IsVerbNotSpecified(invokedVerb, invokedVerbInstance))
			{
				Console.WriteLine("Должны обработать команды по умолчанию");
			}

			switch (invokedVerb)
			{
				case "comb":
					Console.WriteLine("Comb verb");
					break;

				case "subs":
					Console.WriteLine("Subs verb");
					var subsOptions = (SubstituteSubOptions)invokedVerbInstance;

					TrySetEncodings(subsOptions.InEncoding, subsOptions.OutEncoding);
					var subs = new Substitution(subsOptions);
					subs.Process();
					break;

				default:
					Console.WriteLine("Unknown verb");
					break;
			}

			/*
			using (var stream = new StreamReader(options.KeywordsFilename, _inFileEncoding))
			{
				while (!stream.EndOfStream)
				{
					var keyword = stream.ReadLine();
					Console.OutputEncoding = _inFileEncoding;
					Console.WriteLine(keyword);
				}
			}
			*/
		}

		private static bool IsVerbNotSpecified(string invokedVerb, object invokedVerbInstance)
			=> string.IsNullOrEmpty(invokedVerb) || invokedVerbInstance.Equals(null);

		private static void TrySetEncodings(string inEncoding, string outEncoding)
		{
			try
			{
				_inFileEncoding = Encoding.GetEncoding(inEncoding);
			}
			catch (ArgumentException)
			{
				Console.WriteLine("Error using {0} encoding. Fallback to utf-8", inEncoding);
				_inFileEncoding = Encoding.UTF8;
			}

			try
			{
				_outFileEncoding = Encoding.GetEncoding(outEncoding);
			}
			catch (ArgumentException)
			{
				Console.WriteLine("Error using {0} encoding. Fallback to utf-8", outEncoding);
				_outFileEncoding = Encoding.UTF8;
			}
		}
	}
}
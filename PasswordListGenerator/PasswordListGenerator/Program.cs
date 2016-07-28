using System;
using System.Text;

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

			if (!CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
			{
				invokedVerb = verb;
				invokedVerbInstance = subOptions;
			}))
			{
				Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
			}

			//TrySetEncodings(options.InFilesEncoding, options.OutFilesEncoding);

			if (IsVerbNotSpecified(invokedVerb, invokedVerbInstance))
			{
				Console.WriteLine("Должны обработать команды по умолчанию");
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

		private static bool IsVerbNotSpecified(string invokedVerb, object invokedVerbInstance) => string.IsNullOrEmpty(invokedVerb) || invokedVerbInstance.Equals(null);

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
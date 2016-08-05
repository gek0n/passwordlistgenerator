using System;
using System.Text;
using CommandLine;

namespace PasswordListGenerator
{
	internal class Program
	{
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static Encoding _inFileEncoding;
		private static Encoding _outFileEncoding;

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			var options = new Options();
			string invokedVerb = null;
			object invokedVerbInstance = null;

			if (!Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
			{
				invokedVerb = verb;
				invokedVerbInstance = subOptions;
			}))
			{
				Logger.Error("Application can't parse arguments");
				Environment.Exit(Parser.DefaultExitCodeFail);
			}

			if (IsVerbNotSpecified(invokedVerb, invokedVerbInstance))
			{
				Logger.Error("The verb is not specified");
				Environment.Exit(Parser.DefaultExitCodeFail);
			}

			switch (invokedVerb)
			{
				case "comb":
					Logger.Debug("Comb verb");
					break;

				case "subs":
					Logger.Debug("Subs verb");
					var subsOptions = (SubstituteSubOptions)invokedVerbInstance;

					var subs = new Substitution(subsOptions);
					subs.Process();
					break;

				default:
					Logger.Debug("Unknown verb");
					break;
			}
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
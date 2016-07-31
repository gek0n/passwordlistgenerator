using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using PasswordListGenerator.Properties;

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
					var subsOptions = (SubstituteSubOptions)invokedVerbInstance;

					TrySetEncodings(subsOptions.InEncoding, subsOptions.OutEncoding);
					var subs = new Substitution(subsOptions);

					var result = subs.GetResult();
					foreach (var s in result)
					{
						Console.WriteLine(s);
					}
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
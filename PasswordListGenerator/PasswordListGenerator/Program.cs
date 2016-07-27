using System;
using System.IO;
using System.Text;
using CommandLine.Text;

namespace PasswordListGenerator
{
	internal class Program
	{
		private static Encoding _inFileEncoding;
		private static Encoding _outFileEncoding;

		private static void Main(string[] args)
		{
			var options = new Options();
			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
				Console.WriteLine("Working with encodings:\nIN: {0}\nOUT: {1}\nFilename: {2}\n",
					options.InFilesEncoding,
					options.OutFilesEncoding,
					options.KeywordsFilename);
				
				TrySetEncodings(options.InFilesEncoding, options.OutFilesEncoding);
				using (var stream = new StreamReader(options.KeywordsFilename, _inFileEncoding))
				{
					while (!stream.EndOfStream)
					{
						var keyword = stream.ReadLine();
						Console.OutputEncoding = _inFileEncoding;
						Console.WriteLine(keyword);
					}
				}
			}
			else
			{
				Console.WriteLine(HelpText.AutoBuild(options));
			}
		}

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
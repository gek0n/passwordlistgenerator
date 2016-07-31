﻿using System;
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
					var wordToSubs = subsOptions.WordToSubs;

					Console.WriteLine("Subs verb");
					Console.WriteLine("wordToSub = " + wordToSubs);
					Console.WriteLine("method = " + subsOptions.Method);
					Console.WriteLine("dictPath = " + subsOptions.DictFilepath);
					Console.WriteLine("========================");

					var jsonString = "";
					if (!string.IsNullOrEmpty(subsOptions.DictFilepath))
					{
						using (var stream = new StreamReader(subsOptions.DictFilepath, _inFileEncoding))
						{
							jsonString = stream.ReadToEnd();
						}
					}
					if (string.IsNullOrEmpty(jsonString))
					{
						jsonString = Resources.EnglishLeetDict;
					}
					
					var isValid = ValidateJsonScheme(jsonString);
					if (!isValid)
					{
						Console.WriteLine("Json syntax is invalid. Please check your file");
						break;
					}
					var alphabet = GetAlphabet(jsonString);

					var charKeys = wordToSubs
						.ToCharArray()
						.Select(char.ToUpper)
						.ToArray();
					var inputSymbols = Regex
						.Split(wordToSubs, string.Empty)
						.Where(x => !string.IsNullOrEmpty(x))
						.ToArray();
					var wordsToSubs = new List<string[]> { inputSymbols };
					for (var index = 0; index < charKeys.Length; index++)
					{
						List<string> subsSymbols;
						switch (subsOptions.Method)
						{
							case SubsMethod.Cyrillic:
								subsSymbols = new List<string> {alphabet[charKeys[index]].Cyrillic};
								break;

							case SubsMethod.GoodLeet:
								subsSymbols = alphabet[charKeys[index]].GoodLeet;
								break;
								
							case SubsMethod.MadLeet:
								subsSymbols = alphabet[charKeys[index]].MadLeet;
								break;

							case SubsMethod.Pronunciation:
								subsSymbols = new List<string> {alphabet[charKeys[index]].Pronunciation};
								break;

							default:
								subsSymbols = new List<string>();
								break;
						}
						
						var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(wordsToSubs, subsSymbols, index);
						wordsToSubs.AddRange(newWordsToSubs);
					}

					var result = wordsToSubs.Select(word => word.Aggregate((i, s) => i + s)).ToArray();
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

		private static bool ValidateJsonScheme(string jsonString)
		{
			var schemaGenerator = new JSchemaGenerator();
			var schemaForLetter = schemaGenerator.Generate(typeof (Dictionary<char, SubsLetter>));
			try
			{
				var jsonObj = JObject.Parse(jsonString);
				return jsonObj.IsValid(schemaForLetter);
			}
			catch
			{
				return false;
			}
			
		}

		private static List<string[]> GetAllPossibleSubstitutesForEveryWord(List<string[]> wordsToSubs,
			List<string> subsSymbols, int index)
		{
			var result = new List<string[]>();

			foreach (var word in wordsToSubs)
			{
				var substitutesForSymbol = GetAllPossibleSubstitutesForSymbol(subsSymbols, word, index);
				result.AddRange(substitutesForSymbol);
			}
			return result;
		}

		private static List<string[]> GetAllPossibleSubstitutesForSymbol(List<string> subsSymbols, string[] word, int index)
		{
			var result = new List<string[]>();
			foreach (var symbol in subsSymbols)
			{
				var buf = (string[])word.Clone();
				buf[index] = symbol;
				result.Add(buf);
			}
			return result;
		}

		private static Dictionary<char, SubsLetter> GetAlphabet(string jsonString)
		{
			return JsonConvert.DeserializeObject<Dictionary<char, SubsLetter>>(jsonString);
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
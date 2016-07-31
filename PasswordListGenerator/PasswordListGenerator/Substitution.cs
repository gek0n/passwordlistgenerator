using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using PasswordListGenerator.Properties;

namespace PasswordListGenerator
{
	class Substitution
	{
		private readonly List<string[]> _wordsToSubs;
		public Substitution(SubstituteSubOptions subsOptions)
		{ 
			var wordToSubs = subsOptions.WordToSubs;

			Console.WriteLine("Subs verb");
			Console.WriteLine("wordToSub = " + wordToSubs);
			Console.WriteLine("method = " + subsOptions.Method);
			Console.WriteLine("dictPath = " + subsOptions.DictFilepath);
			Console.WriteLine("========================");
			Encoding inFileEncoding;
			try
			{
				inFileEncoding = Encoding.GetEncoding(subsOptions.InEncoding);
			}
			catch (ArgumentException)
			{
				Console.WriteLine($"Error using {subsOptions.InEncoding} encoding. Fallback to utf-8");
				inFileEncoding = Encoding.UTF8;
			}

			var jsonString = "";
			if (!string.IsNullOrEmpty(subsOptions.DictFilepath))
			{
				using (var stream = new StreamReader(subsOptions.DictFilepath, inFileEncoding))
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
				return;
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

			Dictionary<char, List<string>> subsSymbols;
			switch (subsOptions.Method)
			{
				case SubsMethod2.Cyrillic:
					subsSymbols = alphabet["cyrillic"];
					break;

				case SubsMethod2.GoodLeet:
					subsSymbols = alphabet["good-leet"];
					break;

				case SubsMethod2.MadLeet:
					subsSymbols = alphabet["mad-leet"];
					break;

				case SubsMethod2.Pronunciation:
					subsSymbols = alphabet["pronunciation"];
					break;

				default:
					subsSymbols = new Dictionary<char, List<string>>();
					break;
			}
			_wordsToSubs = new List<string[]> { inputSymbols };
			for (var index = 0; index < charKeys.Length; index++)
			{



				var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(_wordsToSubs, subsSymbols, index);
				_wordsToSubs.AddRange(newWordsToSubs);
			}
		}

		public IEnumerable<string> GetResult()
		{
			return _wordsToSubs.Select(word => word.Aggregate((i, s) => i + s));
		}


		private bool ValidateJsonScheme(string jsonString)
		{
			var schemaGenerator = new JSchemaGenerator();
			var schemaForLetter = schemaGenerator.Generate(typeof(Dictionary<string, SubsMethod>));
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

		private List<string[]> GetAllPossibleSubstitutesForEveryWord(List<string[]> wordsToSubs, Dictionary<char, List<string>> subsSymbols, int index)
		{
			var result = new List<string[]>();

			foreach (var word in wordsToSubs)
			{
				var substitutesForSymbol = GetAllPossibleSubstitutesForSymbol(subsSymbols, word, index);
				result.AddRange(substitutesForSymbol);
			}
			return result;
		}

		private List<string[]> GetAllPossibleSubstitutesForSymbol(Dictionary<char, List<string>> subsSymbols, string[] word, int index)
		{
			var result = new List<string[]>();
			var key = char.ToUpper(word[index][0]);
			foreach (var symbol in subsSymbols[key])
			{
				var buf = (string[])word.Clone();
				buf[index] = symbol;
				result.Add(buf);
			}
			return result;
		}

		private Dictionary<string, SubsMethod> GetAlphabet(string jsonString)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, SubsMethod>>(jsonString);
		}
	}
}

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
		private readonly List<string[]> _wordsToSubs;  // TODO: remove this member (not global)

		private readonly string _sourceWord;
		private readonly SubsMethod _method;
		private readonly string _dictFilename;
		private readonly string _outFilename;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		public Substitution(SubstituteSubOptions subsOptions)
		{ 
			_sourceWord = subsOptions.WordToSubs;
			_method = subsOptions.Method;
			_dictFilename = subsOptions.DictFilename;
			_outFilename = subsOptions.OutFilename;
			_inEncoding = TryGetEncoding(subsOptions.InEncoding);
			_outEncoding = TryGetEncoding(subsOptions.OutEncoding);

			Console.WriteLine("wordToSub = " + _sourceWord);
			Console.WriteLine("method = " + _method);
			Console.WriteLine("dictPath = " + _dictFilename);
			Console.WriteLine("========================");
			

			var jsonString = ReadJson();

			var isValid = ValidateJsonScheme(jsonString);
			if (!isValid)
			{
				Console.WriteLine("Json syntax is invalid. Please check your file");
				return;
			}
			var availableMethods = GetAvailableMethods(jsonString);

			var charKeys = _sourceWord
				.ToCharArray()
				.Select(char.ToUpper)
				.ToArray();
			var inputSymbols = Regex
				.Split(_sourceWord, string.Empty)
				.Where(x => !string.IsNullOrEmpty(x))
				.ToArray();

			var alphabet = GetAlphabetForMethod(_method, availableMethods);
			
			_wordsToSubs = new List<string[]> { inputSymbols };
			for (var index = 0; index < charKeys.Length; index++)
			{
				var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(_wordsToSubs, alphabet, index);
				_wordsToSubs.AddRange(newWordsToSubs);
			}
		}

		private Encoding TryGetEncoding(string encoding)
		{
			try
			{
				return Encoding.GetEncoding(encoding);
			}
			catch (ArgumentException)
			{
				Console.WriteLine($"Error using {encoding} encoding. Fallback to utf-8");
				return Encoding.UTF8;
			}
		}

		private Dictionary<char, List<string>> GetAlphabetForMethod(SubsMethod method, Dictionary<string, JsonSubsMethod> availableMethods)
		{
			switch (method)
			{
				case SubsMethod.Cyrillic:
					return availableMethods["cyrillic"];

				case SubsMethod.GoodLeet:
					return availableMethods["good-leet"];

				case SubsMethod.MadLeet:
					return availableMethods["mad-leet"];

				case SubsMethod.Pronunciation:
					return availableMethods["pronunciation"];

				default:
					return null;
			}
		}

		private string ReadJson()
		{
			string result;
			if (string.IsNullOrEmpty(_dictFilename))
			{
				return Resources.EnglishLeetDict;
			}
			using (var stream = new StreamReader(_dictFilename, _inEncoding))
			{
				result = stream.ReadToEnd();
			}
			return string.IsNullOrEmpty(result) ? Resources.EnglishLeetDict : result;
		}

		public IEnumerable<string> GetResult()
		{
			return _wordsToSubs.Select(word => word.Aggregate((i, s) => i + s));
		}


		private bool ValidateJsonScheme(string jsonString)
		{
			var schemaGenerator = new JSchemaGenerator();
			var schemaForLetter = schemaGenerator.Generate(typeof(Dictionary<string, JsonSubsMethod>));
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

		private Dictionary<string, JsonSubsMethod> GetAvailableMethods(string jsonString)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, JsonSubsMethod>>(jsonString);
		}
	}
}

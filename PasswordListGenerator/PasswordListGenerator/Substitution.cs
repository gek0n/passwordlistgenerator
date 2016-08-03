using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using PasswordListGenerator.Properties;

namespace PasswordListGenerator
{
	public class Substitution
	{
		private List<string[]> _wordsToSubs;

		private readonly string _sourceWord;
		private readonly SubsMethod _method;
		private readonly bool _isIgnoreCase;
		private readonly string _dictFilename;
		private readonly string _outFilename;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		private readonly string _helpMessage;
		private string _errorMessage = "Error: {0}" + Environment.NewLine + "See help usage" + Environment.NewLine + Environment.NewLine;

		public Substitution(SubstituteSubOptions subsOptions)
		{
			_helpMessage = HelpText.AutoBuild(subsOptions);
			_sourceWord = subsOptions.SourceWord;
			_method = subsOptions.Method;
			_isIgnoreCase = subsOptions.IsIgnoreCase;
			_dictFilename = subsOptions.DictFilename;
			_outFilename = subsOptions.OutFilename;
			_inEncoding = TryGetEncoding(subsOptions.InEncoding);
			_outEncoding = TryGetEncoding(subsOptions.OutEncoding);

			/*
			Console.WriteLine("========= Constructor ===========");
			Console.WriteLine("wordToSub = " + _sourceWord);
			Console.WriteLine("method = " + _method);
			Console.WriteLine("dictPath = " + _dictFilename);
			Console.WriteLine("=================================");
			*/
		}

		public void Process()
		{
			if (string.IsNullOrEmpty(_sourceWord))
			{
				Console.WriteLine($"{GetErrorMessage("Empty word to substitution")}{_helpMessage}");
				return;
			}

			var jsonString = ReadJson();

			if (!ValidateJsonScheme(jsonString))
			{
				Console.WriteLine("Json syntax is invalid. Please check your file");
				return;
			}
			var availableMethods = GetAvailableMethods(jsonString);

			var inputSymbols = SplitWordToStringArray(_sourceWord);

			var alphabet = GetAlphabetForMethod(_method, availableMethods);

			_wordsToSubs = new List<string[]> { inputSymbols };
			for (var index = 0; index < inputSymbols.Length; index++)
			{
				try
				{
					var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(_wordsToSubs, alphabet, index);
					_wordsToSubs.AddRange(newWordsToSubs);
				}
				catch (ArgumentException exception)
				{
					Console.WriteLine($"{GetErrorMessage(exception.Message)}{_helpMessage}");
				}
			}
			var result = _wordsToSubs.Select(word => word.Aggregate((i, s) => i + s));
			GetResult(result);
		}

		private void GetResult(IEnumerable<string> result)
		{
			if (string.IsNullOrEmpty(_outFilename))
			{
				foreach (var s in result)
				{
					Console.WriteLine(s);
				}
			}
			else
			{
				using (var stream = new StreamWriter(_outFilename, false, _outEncoding))
				{
					foreach (var s in result)
					{
						stream.WriteLine(s);
					}
				}
			}
		}

		private string[] SplitWordToStringArray(string source)
		{
			return Regex
				.Split(source, string.Empty)
				.Where(x => !string.IsNullOrEmpty(x))
				.ToArray();
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

		private string ReadJson()
		{
			string result;
			if (string.IsNullOrEmpty(_dictFilename))
			{
				return Resources.EnglishLeetDict;
			}
			if (!File.Exists(_dictFilename))
			{
				return Resources.EnglishLeetDict;
			}
			using (var stream = new StreamReader(_dictFilename, _inEncoding))
			{
				result = stream.ReadToEnd();
			}
			return string.IsNullOrEmpty(result) ? Resources.EnglishLeetDict : result;
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

		private Dictionary<string, JsonSubsMethod> GetAvailableMethods(string jsonString)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, JsonSubsMethod>>(jsonString);
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
			var key = _isIgnoreCase ? char.ToUpper(word[index][0]) : word[index][0];
			List<string> colletion;
			if (!subsSymbols.TryGetValue(key, out colletion))
			{
				throw new ArgumentException("The symbol is not in the dictionary. Please specify other dictionary or use ignore-case option");
			}
			foreach (var symbol in colletion)
			{
				var buf = (string[])word.Clone();
				buf[index] = symbol;
				result.Add(buf);
			}
			return result;
		}

		private string GetErrorMessage(string msg)
		{
			return string.Format(_errorMessage, msg);
		}
	}
}

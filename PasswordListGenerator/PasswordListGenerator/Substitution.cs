// Copyright 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

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
	public class Substitution
	{
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private readonly string _sourceWord;
		private readonly string _method;
		private readonly bool _isIgnoreCase;
		private readonly string _dictFilename;
		private readonly string _outFilename;
		private readonly bool _isUseStdInput;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		private readonly string _helpMessage;
		private readonly string _errorMessage = "[ERROR]: {0}" + Environment.NewLine + Environment.NewLine;

		public Substitution(SubstituteSubOptions subsOptions)
		{
			_helpMessage = subsOptions.GetUsage();
			_sourceWord = subsOptions.SourceWord;
			_method = subsOptions.Method?.ToLowerInvariant();
			_isIgnoreCase = subsOptions.IsIgnoreCase;
			_dictFilename = subsOptions.DictFilename;
			_outFilename = subsOptions.OutFilename;
			_isUseStdInput = subsOptions.IsUseStdInput;
			_inEncoding = TryGetEncoding(subsOptions.InEncoding);
			_outEncoding = TryGetEncoding(subsOptions.OutEncoding);

			Logger.Debug($"wordToSub = {_sourceWord}" +
						$"method = {_method}" +
						$"IsIgnoreCase = {_isIgnoreCase}" +
						$"dictFilename = {_dictFilename}" +
						$"outFilename = {_outFilename}" +
						$"inEncoding = {_inEncoding}" +
						$"outEncoding = {_outEncoding}");
		}

		public void Process()
		{
			if (string.IsNullOrEmpty(_sourceWord) && !_isUseStdInput)
			{
				Logger.Error("Empty word to substitution");
				Console.WriteLine($"{GetErrorMessage("Empty word to substitution")}{_helpMessage}");
				return;
			}

			var jsonString = ReadJson();

			if (!ValidateJsonScheme(jsonString))
			{
				Logger.Error("Json syntax is invalid. Please check your file");
				Console.WriteLine($"{GetErrorMessage("Json syntax is invalid. Please check your file")}{_helpMessage}");
				return;
			}
			var availableMethods = GetAvailableMethods(jsonString);

			var alphabet = GetAlphabetForMethod(_method, availableMethods);

			var processingWord = _sourceWord;
			while (true)
			{
				if (_isUseStdInput)
				{
					processingWord = Console.ReadLine();
					if (string.IsNullOrEmpty(processingWord))
					{
						break;
					}
				}
				var tokens = SplitWordToStringArray(processingWord);

				var wordsToSubs = new List<string[]> { tokens };

				IEnumerable<string> result;
				try
				{
					result = GetSubstitute(wordsToSubs, alphabet, tokens.Length);
				}
				catch (ArgumentException exception)
				{
					Logger.Error(exception.Message);
					if (_isUseStdInput)
					{
						Console.WriteLine($"{GetErrorMessage(exception.Message)}");
						continue;
					}
					Console.WriteLine($"{GetErrorMessage(exception.Message)}{_helpMessage}");
					return;
				}

				try
				{
					GetResult(result);
				}
				catch (Exception exception)
				{
					Logger.Error(exception.Message);
					Console.WriteLine("Output file error. See log file for more information");
					return;
				}
				if (!_isUseStdInput)
				{
					return;
				}
			}
		}

		private IEnumerable<string> GetSubstitute(List<string[]> wordsToSubs, Dictionary<char, List<string>> alphabet, int colTokens)
		{
			for (var index = 0; index < colTokens; index++)
			{
				var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(wordsToSubs, alphabet, index);
				wordsToSubs.AddRange(newWordsToSubs);
			}
			return wordsToSubs.Select(word => word.Aggregate((i, s) => i + s));
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
				using (var stream = new StreamWriter(_outFilename, _isUseStdInput, _outEncoding))
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
				Logger.Warn($"Can't using {encoding} encoding. Fallback to utf-8");
				Console.WriteLine($"Warning! Can't using {encoding} encoding. Fallback to utf-8");
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
			var originalDictionary = JsonConvert.DeserializeObject<Dictionary<string, JsonSubsMethod>>(jsonString);
			return new Dictionary<string, JsonSubsMethod>(originalDictionary, StringComparer.OrdinalIgnoreCase);
		}

		private Dictionary<char, List<string>> GetAlphabetForMethod(string method, Dictionary<string, JsonSubsMethod> availableMethods)
		{
			JsonSubsMethod subsMethod;
			if (method != null && availableMethods.TryGetValue(method, out subsMethod))
			{
				Logger.Info($"Selected method is {method}");
				Console.WriteLine($"Selected method is {method}");
				return availableMethods[method];
			}
			Logger.Info($"Selected method is {availableMethods.First().Key}");
			Console.WriteLine($"Selected method is {availableMethods.First().Key}");
			return availableMethods.First().Value;
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
				throw new ArgumentException($"The symbol \"{key}\" is not in the dictionary. Please specify other dictionary or use ignore-case option");
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

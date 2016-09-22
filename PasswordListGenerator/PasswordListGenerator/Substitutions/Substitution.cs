// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

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

namespace PasswordListGenerator.Substitutions
{
	public class Substitution : IVerbOption
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private readonly string _method;
		private readonly bool _isIgnoreCase;
		private readonly string _dictFilename;
		private readonly string _outFilename;
		private readonly bool _isUseStdInput;
		private readonly bool _isVerbose;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		private string _sourceWord;
		private string _verboseMsg;
		private Dictionary<char, List<string>> _alphabet;

		public Substitution(SubstituteSubOption subOption)
		{
			_verboseMsg = "";
			_sourceWord = subOption.SourceWord;
			_method = subOption.Method?.ToLowerInvariant();
			_isIgnoreCase = subOption.IsIgnoreCase;
			_dictFilename = subOption.DictFilename;
			_outFilename = subOption.OutFilename;
			_isUseStdInput = subOption.IsUseStdInput;
			_isVerbose = subOption.IsVerbose;
			_inEncoding = EncodingHelper.TryGetEncoding(subOption.InEncoding);
			_outEncoding = EncodingHelper.TryGetEncoding(subOption.OutEncoding);
			_verboseMsg += $"[ENCODING]:{Environment.NewLine}" +
							$"\tIN: {_inEncoding.BodyName}{Environment.NewLine}" +
							$"\tOUT: {_outEncoding.BodyName}{Environment.NewLine}";

			Logger.Debug($"wordToSub = {_sourceWord}" +
						$"method = {_method}" +
						$"IsIgnoreCase = {_isIgnoreCase}" +
						$"dictFilename = {_dictFilename}" +
						$"outFilename = {_outFilename}" +
						$"inEncoding = {_inEncoding}" +
						$"outEncoding = {_outEncoding}");

			Initialize();
		}

		private void Initialize()
		{
			var jsonString = ReadJson();
			if (IsJsonSchemeNotValid(jsonString))
			{
				throw new ValidateJsonSubstituteException("Json scheme is invalid. Please check your file");
			}

			var availableMethods = GetAvailableMethods(jsonString);
			_alphabet = GetAlphabetForMethod(_method, availableMethods);
		}

		public void Process()
		{
			if (IsNothingToSubstitute())
			{
				throw new SourceWordSubstituteException("Nothing for substitute. Specify word or use \"-i\" option");
			}

			if (_isVerbose)
			{
				Console.WriteLine(_verboseMsg);
			}

			while (true)
			{
				if (_isUseStdInput)
				{
					if (!TryGetSourceWordFromUserInput())
					{
						break;
					}
				}

				if (!TryGetSubstitutionsForSourceWord())
				{
					return;
				}

				if (!_isUseStdInput)
				{
					break;
				}
			}
		}

		private bool TryGetSourceWordFromUserInput()
		{
			try
			{
				_sourceWord = Console.ReadLine();
			}
			catch (IOException)
			{
				return false;
			}
			if (IsInputHasStopped())
			{
				return false;
			}
			return true;
		}

		private bool IsInputHasStopped() => string.IsNullOrEmpty(_sourceWord);

		private bool TryGetSubstitutionsForSourceWord()
		{
			var literals = SplitToLiterals(_sourceWord);

			var substitutableWords = new List<string[]> { literals };

			try
			{
				var substitutions = GetSubstitutions(substitutableWords, _alphabet);
				ReturnResult(substitutions);
			}
			catch (NotInTheDictionarySubstituteException exception)
			{
				Logger.ErrorAndPrint(exception.Message);

				if (!_isUseStdInput)
				{
					return false;
				}
			}
			return true;
		}

		private string[] SplitToLiterals(string source)
		{
			return Regex
				.Split(source, string.Empty)
				.Where(x => !string.IsNullOrEmpty(x))
				.ToArray();
		}

		private bool IsNothingToSubstitute() => string.IsNullOrEmpty(_sourceWord) && !_isUseStdInput;

		private IEnumerable<string> GetSubstitutions(List<string[]> substitutableWords, Dictionary<char, List<string>> alphabet)
		{
			var colTokens = substitutableWords[0].Length;
			for (var index = 0; index < colTokens; index++)
			{
				var newWordsToSubs = GetAllPossibleSubstitutesForEveryWord(substitutableWords, alphabet, index);
				substitutableWords.AddRange(newWordsToSubs);
			}
			return substitutableWords.Select(word => word.Aggregate((i, s) => i + s));
		}

		private void ReturnResult(IEnumerable<string> collection)
		{
			try
			{
				if (!string.IsNullOrEmpty(_outFilename))
				{
					var stream = new StreamWriter(_outFilename, _isUseStdInput, _outEncoding);
					OutputHelper.WriteToStream(collection, stream.WriteLine);
					stream.Close();
				}
				else
				{
					OutputHelper.WriteToStream(collection, Console.WriteLine);
				}
			}
			catch (IOException exception)
			{
				throw new IOSubstituteException(exception.Message);
			}
		}

		private string ReadJson()
		{
			string result;
			if (string.IsNullOrEmpty(_dictFilename))
			{
				Logger.Warn("Dictionary file is not specified. Default dictionary will be used");
				_verboseMsg += $"[DICTIONARY]: default{Environment.NewLine}";
				return Resources.EnglishLeetDict;
			}
			if (!File.Exists(_dictFilename))
			{
				Logger.WarnAndPrint($"Dictionary file \"{_dictFilename}\" not found. Default dictionary will be used");
				_verboseMsg += $"[DICTIONARY]: default{Environment.NewLine}";
				return Resources.EnglishLeetDict;
			}
			using (var stream = new StreamReader(_dictFilename, _inEncoding))
			{
				result = stream.ReadToEnd();
			}
			if (string.IsNullOrEmpty(result))
			{
				_verboseMsg += $"[DICTIONARY]: default{Environment.NewLine}";
				return Resources.EnglishLeetDict;
			}
			else
			{
				_verboseMsg += $"[DICTIONARY]: {_dictFilename}{Environment.NewLine}";
				return result;
			}
		}

		private bool IsJsonSchemeNotValid(string jsonString)
		{
			var schemaGenerator = new JSchemaGenerator();
			var schemaForLetter = schemaGenerator.Generate(typeof(Dictionary<string, JsonSubstituteVerb>));
			try
			{
				var jsonObj = JObject.Parse(jsonString);
				return !jsonObj.IsValid(schemaForLetter);
			}
			catch (JsonReaderException exception)
			{
				throw new ParseJsonSubstituteException(exception.Message);
			}
		}

		private Dictionary<string, JsonSubstituteVerb> GetAvailableMethods(string jsonString)
		{
			Dictionary<string, JsonSubstituteVerb> originalDictionary;

			try
			{
				originalDictionary = JsonConvert.DeserializeObject<Dictionary<string, JsonSubstituteVerb>>(jsonString);
			}
			catch (JsonSerializationException exception)
			{
				throw new DeserializeSubstituteException(exception.Message);
			}

			return new Dictionary<string, JsonSubstituteVerb>(originalDictionary, StringComparer.OrdinalIgnoreCase);
		}

		private Dictionary<char, List<string>> GetAlphabetForMethod(string method, Dictionary<string, JsonSubstituteVerb> availableMethods)
		{

			JsonSubstituteVerb subsMethod;
			Dictionary<char, List<string>> alphabet;
			if (method != null && availableMethods.TryGetValue(method, out subsMethod))
			{
				alphabet = availableMethods[method];
			}
			else
			{
				method = availableMethods.First().Key;
				alphabet = availableMethods.First().Value;
			}
			Logger.Info($"Selected method is {method}");
			_verboseMsg += $"[METHOD]: {method}{Environment.NewLine}";
			return alphabet;
		}

		private List<string[]> GetAllPossibleSubstitutesForEveryWord(List<string[]> wordsToSubs, Dictionary<char, List<string>> subsSymbols, int index)
		{
			var result = new List<string[]>();

			foreach (var word in wordsToSubs)
			{
				var substitutesForSymbol = GetAllPossibleSubstitutesForLiteral(subsSymbols, word, index);
				result.AddRange(substitutesForSymbol);
			}
			return result;
		}

		private List<string[]> GetAllPossibleSubstitutesForLiteral(Dictionary<char, List<string>> subsSymbols, string[] word, int index)
		{
			var result = new List<string[]>();
			var key = _isIgnoreCase ? char.ToUpper(word[index][0]) : word[index][0];
			List<string> colletion;
			if (!subsSymbols.TryGetValue(key, out colletion))
			{
				throw new NotInTheDictionarySubstituteException($"The symbol \"{key}\" is not in the dictionary. Please specify other dictionary or use ignore-case option");
			}
			foreach (var symbol in colletion)
			{
				var buf = (string[])word.Clone();
				buf[index] = symbol;
				result.Add(buf);
			}
			return result;
		}
	}
}

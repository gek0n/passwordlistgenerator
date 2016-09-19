﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PasswordListGenerator.Combinations
{
	public class Combine : IVerbOption
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private const ushort MIN_LENGTH_OF_ELEMENTS_IN_COMBINATION = 2;
		private const ushort MAX_LENGTH_OF_ELEMENTS_IN_COMBINATION = 10;

		private readonly ushort _maxLength;
		private readonly string _keywordFilename;
		private readonly string _combinationFilename;
		private readonly string _delimiter;
		private readonly string _suffix;
		private readonly string _prefix;
		private readonly bool _isRepetition;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		public Combine(CombineSubOption subOption)
		{
			_maxLength = subOption.MaxLength;
			_keywordFilename = subOption.KeywordFilename;
			_combinationFilename = subOption.CombinationFilename;
			_delimiter = subOption.Delimiter;
			_suffix = subOption.Suffix;
			_prefix = subOption.Prefix;
			_isRepetition = subOption.IsRepetition;
			_inEncoding = TryGetEncoding(subOption.InEncoding);
			_outEncoding = TryGetEncoding(subOption.OutEncoding);
		}

		public void Process()
		{
			CheckParameters();
			GetCombinations();
		}

		private void CheckParameters()
		{
			if (IsMaxLengthInvalid())
			{
				throw new MaxLengthNotInRangeCombineException("Max length is invalid. See help for more information");
			}

			if (IsKeywordFilenameInvalid())
			{
				throw new FilenameInvalidCombineException("Keyword file is invalid");
			}
			/*
			if (IsCombinationFilenameInvalid())
			{
				throw new FilenameInvalidCombineException("Combination file is invalid");
			}
			
			if (IsDelimiterInvalid())
			{
				throw new FilenameInvalidCombineException("Dilimiter is invalid");
			}
			*/
		}

		private bool IsMaxLengthInvalid() => _maxLength < MIN_LENGTH_OF_ELEMENTS_IN_COMBINATION || _maxLength > MAX_LENGTH_OF_ELEMENTS_IN_COMBINATION;

		private bool IsKeywordFilenameInvalid() => string.IsNullOrEmpty(_keywordFilename) || !File.Exists(_keywordFilename);

		private bool IsCombinationFilenameInvalid() => string.IsNullOrEmpty(_combinationFilename);

		private bool IsDelimiterInvalid() => string.IsNullOrEmpty(_delimiter);

		private void GetCombinations()
		{
			string content;
			using (var stream = new StreamReader(_keywordFilename, _inEncoding))
			{
				content = stream.ReadToEnd();
			}
			var words = content.Split(new []{ Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if ((_maxLength > words.Length) && !_isRepetition)
			{
				throw new MaxLengthNotInRangeCombineException("Max length is more than count of the words and repetitions not allowed. See help for more information");
			}
			
			var combinations = GetWordsCombinations(words);
			foreach (var combination in combinations)
			{
				Console.WriteLine(combination);
			}
		}

		private List<string> GetWordsCombinations(string[] words)
		{
			var listOfIndexes = CombineIndexes(words.Length, _maxLength);
			var result = new List<string>();
			foreach (var indexes in listOfIndexes)
			{
				var s = "";
				for (int index = 0; index < indexes.Count; index++)
				{
					var i = indexes[index];
					s += index < indexes.Count -1 ? $"{words[i]}{_delimiter}" : $"{words[i]}";
				}
				result.Add((_prefix ?? "") + s + (_suffix ?? ""));
			}
			return result;
		}

		private List<List<int>> CombineIndexes(int count, int length)
		{
			return Enumerable
					.Repeat(Enumerable.Range(0, count), length)
					.Combinations(_isRepetition)
					.Select(t => t.ToList())
					.ToList();
		}

		private Encoding TryGetEncoding(string encoding)
		{
			try
			{
				return Encoding.GetEncoding(encoding);
			}
			catch (ArgumentException)
			{
				Logger.WarnAndPrint($"Can't using {encoding} encoding. Fallback to utf-8");
				return Encoding.UTF8;
			}
		}
	}
}
// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PasswordListGenerator.Combinations
{
	public class Combine : IVerbOption
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private const ushort MinLengthOfElementsInCombination = 2;
		private const ushort MaxLengthOfElementsInCombination = 10;

		private readonly ushort _maxLength;
		private readonly string _inFilename;
		private readonly string _outFilename;
		private readonly string _delimiter;
		private readonly string _suffix;
		private readonly string _prefix;
		private readonly bool _isRepetition;
	    private readonly bool _isVerbose;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

        private readonly string _verboseMsg;

        public Combine(CombineSubOption combOption)
		{
            _verboseMsg = "";
            _maxLength = combOption.MaxLength;
			_inFilename = combOption.InFilename;
			_outFilename = combOption.OutFilename;
			_delimiter = combOption.Delimiter;
			_suffix = combOption.Suffix;
			_prefix = combOption.Prefix;
			_isRepetition = combOption.IsRepetition;
		    _isVerbose = combOption.IsVerbose;
			_inEncoding = EncodingHelper.TryGetEncoding(combOption.InEncoding);
			_outEncoding = EncodingHelper.TryGetEncoding(combOption.OutEncoding);
            _verboseMsg += $"[ENCODING]:{Environment.NewLine}" +
                            $"\tIN: {_inEncoding.BodyName}{Environment.NewLine}" +
                            $"\tOUT: {_outEncoding.BodyName}{Environment.NewLine}" +
                            $"[SUFFIX]: {_suffix}{Environment.NewLine}" +
                            $"[PREFIX]: {_prefix}{Environment.NewLine}" +
                            $"[DELIMITER]: {_delimiter}{Environment.NewLine}" +
                            $"[MAX LENGTH]: {_maxLength}{Environment.NewLine}";

            Logger.Debug($"maxLength = {_maxLength}" + 
						$"keywordFilename = {_inFilename}" +
						$"combinationFilename = {_outFilename}" +
						$"delimiter = {_delimiter}" +
						$"suffix = {_suffix}" +
						$"prefix = {_prefix}" +
						$"isRepetition = {_isRepetition}" +
						$"inEncoding = {_inEncoding}" +
						$"outEncoding = {_outEncoding}");
		}

		public void Process()
		{
            if (_isVerbose)
            {
                Console.WriteLine(_verboseMsg);
            }
            CheckParameters();
			var combinations = GetCombinations();
			OutputToFileOrConsole(combinations);
		}

		private void CheckParameters()
		{
			if (IsMaxLengthInvalid())
			{
				throw new MaxLengthNotInRangeCombineException("Max length is invalid. See help for more information");
			}

			if (IsInFilenameInvalid())
			{
				throw new FilenameInvalidCombineException("Keyword file is invalid");
			}
		}

		private bool IsMaxLengthInvalid() => _maxLength < MinLengthOfElementsInCombination || _maxLength > MaxLengthOfElementsInCombination;

		private bool IsInFilenameInvalid() => string.IsNullOrEmpty(_inFilename) || !File.Exists(_inFilename);

		private List<string> GetCombinations()
		{
			var content = GetContentFromFile();
			var words = SplitToWords(content);
			if ((_maxLength > words.Length) && !_isRepetition)
			{
				throw new MaxLengthNotInRangeCombineException("Max length is more than count of the words and repetitions not allowed. See help for more information");
			}
			
			var combinations = GetWordsCombinations(words);
			return combinations;
		}

		private static string[] SplitToWords(string content)
		{
			return content.Split(new []{ Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}

		private string GetContentFromFile()
		{
			using (var stream = new StreamReader(_inFilename, _inEncoding))
			{
				return stream.ReadToEnd();
			}
		}

		private List<string> GetWordsCombinations(IReadOnlyList<string> words)
		{
			var listOfIndexes = CombineIndexes(words.Count, _maxLength);
		    return listOfIndexes
                .Select(indexes => GenerateOneCombination(indexes, words))
                .Select(SurroundAffixes)
                .ToList();
		}

	    private List<List<int>> CombineIndexes(int count, int length)
		{
			return Enumerable
					.Repeat(Enumerable.Range(0, count), length)
					.Combinations(_isRepetition)
					.Select(t => t.ToList())
					.ToList();
		}

	    private string GenerateOneCombination(IReadOnlyList<int> indexes, IReadOnlyList<string> words)
	    {
            var result = "";
            for (var index = 0; index < indexes.Count; index++)
            {
                var i = indexes[index];
                result += index < indexes.Count - 1 ? $"{words[i]}{_delimiter}" : $"{words[i]}";
            }
	        return result;
	    }

	    private string SurroundAffixes(string oneCombination)
	    {
	        return (_prefix ?? "") + oneCombination + (_suffix ?? "");
	    }

	    private void OutputToFileOrConsole(IEnumerable<string> result)
		{
			try
			{
				if (!string.IsNullOrEmpty(_outFilename))
				{
					var stream = new StreamWriter(_outFilename, false, _outEncoding);
					OutputHelper.WriteToStream(result, stream.WriteLine);
					stream.Close();
				}
				else
				{
					OutputHelper.WriteToStream(result, Console.WriteLine);
				}
			}
			catch (IOException exception)
			{
				throw new IOCombineException(exception.Message);
			}
		}
	}
}

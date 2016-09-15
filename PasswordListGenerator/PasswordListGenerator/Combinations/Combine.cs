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

		private readonly ushort _maxLength;
		private readonly string _keywordFilename;
		private readonly string _combinationFilename;
		private readonly string _delimiter;
		private readonly bool _isRepetition;
		private readonly Encoding _inEncoding;
		private readonly Encoding _outEncoding;

		private List<List<int>> list;
		public Combine(CombineSubOption subOption)
		{
			_maxLength = subOption.MaxLength;
			_keywordFilename = subOption.KeywordFilename;
			_combinationFilename = subOption.CombinationFilename;
			_delimiter = subOption.Delimiter;
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
				throw new CombineExceptions("Max length is invalid. See help for more information");
			}

			if (IsKeywordFilenameInvalid())
			{
				throw new CombineExceptions("Keyword file is invalid");
			}
			/*
			if (IsCombinationFilenameInvalid())
			{
				throw new CombineExceptions("Combination file is invalid");
			}

			if (IsDelimiterInvalid())
			{
				throw new CombineExceptions("Dilimiter is invalid");
			}
			*/
		}

		private bool IsMaxLengthInvalid() => !(_maxLength >= 2 && _maxLength <= 20);

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
			list = new List<List<int>>();
			CombineIndexes(words.Length);
			
		}

		private void CombineIndexes(int count)
		{
			if (list.Count == 0)
			{
				for (var i = 0; i < count; i++)
				{
					list.Add(new List<int>());
				}
			}
			/*
			foreach (var element in list)
			{
				var buf = new List<List<int>>();
				for(var i = 0; i < count; i++)
				{
					var buf2 = new List<int>(element);
					if (!buf2.Contains(i) || _isRepetition)
					{
						buf2.Add(i);
					}
					buf.Add(buf2);
				}
				list.AddRange(buf);
			}
			*/
			Console.WriteLine(count);
			if(--count > 0) CombineIndexes(count);
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

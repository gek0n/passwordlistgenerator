using System;

namespace PasswordListGenerator.Combinations
{
	public class Combine : IVerbOption
	{
		private readonly ushort _maxLength;
		private readonly string _keywordFilename;
		private readonly string _combinationFilename;
		Combine(CombineSubOption subOption)
		{
			_maxLength = subOption.MaxLength;
			_keywordFilename = subOption.KeywordFilename;
			_combinationFilename = subOption.CombinationFilename;
		}

		public void Process()
		{
			if (IsMaxLengthInValid())
			{
				//throw new error
			}
		}

		private bool IsMaxLengthInValid() => !(_maxLength >= 2 && _maxLength <= 20);

		private void GetCombinations()
		{
			throw new NotImplementedException("Not implemented yet");
		}
	}
}

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
			CheckParameters();
		}

		private void CheckParameters()
		{
			if (IsMaxLengthInValid())
			{
				//throw new error
			}

			if (IsKeywordFilenameInvalid())
			{
				//throw new error
			}

			if (IsCombinationFilenameInvalid())
			{
				//throw new error
			}
		}

		private bool IsMaxLengthInValid() => !(_maxLength >= 2 && _maxLength <= 20);

		private bool IsKeywordFilenameInvalid()
		{
			throw new NotImplementedException("Not implemented yet");
		}

		private bool IsCombinationFilenameInvalid()
		{
			throw new NotImplementedException("Not implemented yet");
		}

		private void GetCombinations()
		{
			throw new NotImplementedException("Not implemented yet");
		}
	}
}

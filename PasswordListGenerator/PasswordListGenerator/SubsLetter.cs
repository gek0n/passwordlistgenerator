using System.Collections.Generic;
using Newtonsoft.Json;

namespace PasswordListGenerator
{
	[JsonObject]
	public class SubsLetter
	{
		[JsonProperty("cyrillic")]
		public string Cyrillic { get; set; }

		[JsonProperty("pronunciation")]
		public string Pronunciation { get; set; }

		[JsonProperty("mad-leet")]
		public List<string> MadLeet { get; set; }

		[JsonProperty("good-leet")]
		public List<string> GoodLeet { get; set; }
	}
}

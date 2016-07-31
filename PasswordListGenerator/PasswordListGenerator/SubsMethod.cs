using System.Collections.Generic;
using Newtonsoft.Json;

namespace PasswordListGenerator
{
	[JsonObject]
	public class SubsMethod
	{
		[JsonDictionary("alphabet")]
		public Dictionary<char, List<string>> Alphabet { get; set; } 
	}
}

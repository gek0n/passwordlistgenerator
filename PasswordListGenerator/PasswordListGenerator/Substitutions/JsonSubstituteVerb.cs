// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace PasswordListGenerator.Substitutions
{
	[JsonDictionary]
	public class JsonSubstituteVerb : Dictionary<char, List<string>>
	{

	}
}

// Copyright 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace PasswordListGenerator
{
	[JsonDictionary]
	public class JsonSubsMethod : Dictionary<char, List<string>>
	{

	}
}

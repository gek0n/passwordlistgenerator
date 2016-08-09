// Copyright 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using System.Text;
using CommandLine;

namespace PasswordListGenerator
{
	internal class Program
	{
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			try
			{
				var verbOption = CreateVerb(args);
				verbOption.Process();
			}
			catch (ArgumentNullException e)
			{
				Logger.Error(e.Message);
			}
			catch (ArgumentException e)
			{
				Logger.Error(e.Message);
			}
		}

		private static IVerbOption CreateVerb(string[] args)
		{
			IVerbOption option = null;
			if (!Parser
				.Default
				.ParseArguments(
					args, 
					new Options(), 
					(verbName, verbInstance) => option = VerbOptionFactory.Construct(verbName, verbInstance)
				)
			)
			{
				throw new ArgumentException("Application can't parse arguments");
			}
			return option;
		}
	}
}
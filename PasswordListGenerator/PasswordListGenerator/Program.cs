// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System;
using System.Text;
using CommandLine;

namespace PasswordListGenerator
{
	internal class Program
	{
		private static readonly Logger Logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
		    try
		    {
		        var verbOption = CreateVerbOption(args);
		        if (verbOption == null)
		        {
		            Logger.ErrorAndPrint("Can't create command handler");
		            return;
		        }
		        verbOption.Process();
		    }
		    catch (ArgumentException e)
		    {
		        Logger.Error(e.Message);
		    }
		    catch (VerbOptionException exception)
		    {
		        Logger.ErrorAndPrint(exception.Message);
		    }
		}

		private static IVerbOption CreateVerbOption(string[] args)
		{
			IVerbOption verbOption = null;
			if (!Parser
				.Default
				.ParseArguments(
					args, 
					new Options(), 
					(verbName, verbInstance) => verbOption = VerbOptionFactory.Construct(verbName, verbInstance)
				)
			)
			{
				throw new ArgumentException("Application can't parse arguments");
			}
			return verbOption;
		}
	}
}
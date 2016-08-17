// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.
using System;

namespace PasswordListGenerator
{
	public class Logger
	{
		private readonly log4net.ILog _log;

		public Logger(Type type)
		{
			_log = log4net.LogManager.GetLogger(type);
		}

		public void Debug(string msg)
		{
			Log(_log.Debug, msg);
		}

		public void Error(string msg)
		{
			Log(_log.Error, msg);
		}

		public void Fatal(string msg)
		{
			Log(_log.Fatal, msg);
		}

		public void Info(string msg)
		{
			Log(_log.Info, msg);
		}

		public void Warn(string msg)
		{
			Log(_log.Warn, msg);
		}

		public void DebugAndPrint(string msg)
		{
			LogAndPrint(_log.Debug, msg);
		}

		public void ErrorAndPrint(string msg)
		{
			LogAndPrint(_log.Error, msg);
		}

		public void FatalAndPrint(string msg)
		{
			LogAndPrint(_log.Fatal, msg);
		}

		public void InfoAndPrint(string msg)
		{
			LogAndPrint(_log.Info, msg);
		}

		public void WarnAndPrint(string msg)
		{
			LogAndPrint(_log.Warn, msg);
		}

		private static void LogAndPrint(Action<string> logMethod, string msg)
		{
			Log(logMethod, msg);
			Console.WriteLine(msg);
		}

		private static void Log(Action<string> logMethod, string msg)
		{
			logMethod(msg);
		}
	}
}

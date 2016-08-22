// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.
using System;

namespace PasswordListGenerator
{
	public class Logger
	{
		private readonly log4net.ILog _log;
		private static readonly string ErrorMessage = Environment.NewLine + "[ERROR]: {0}";
		private static readonly string WarningMessage = Environment.NewLine + "[WARNING]: {0}";
		private static readonly string FatalMessage = Environment.NewLine + "[FATAL]: {0}";
		private static readonly string InfoMessage = Environment.NewLine + "[INFO]: {0}";
		private static readonly string DebugMessage = Environment.NewLine + "[DEBUG]: {0}";

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
			Debug(msg);
			Console.WriteLine(DebugMessage, msg);
		}

		public void ErrorAndPrint(string msg)
		{
			Error(msg);
			Console.WriteLine(ErrorMessage, msg);
		}

		public void FatalAndPrint(string msg)
		{
			Fatal(msg);
			Console.WriteLine(FatalMessage, msg);
		}

		public void InfoAndPrint(string msg)
		{
			Info(msg);
			Console.WriteLine(InfoMessage, msg);
		}

		public void WarnAndPrint(string msg)
		{
			Warn(msg);
			Console.WriteLine(WarningMessage, msg);
		}

		private static void Log(Action<string> logMethod, string msg)
		{
			logMethod(msg);
		}
	}
}

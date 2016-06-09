﻿
using System;
using MoneyTrackApp.Accounting;
using MoneyTrackApp.Accounting.Output;
using MoneyTrackApp.Accounting.Parsing;

namespace MoneyTrackApp
{
	class Program
	{
		public static void Main(string[] args)
		{
			var register=new AccountRegister();
			var parser=new CommandParser(register);
			foreach (var line in StandardInputReader.EndWithBlankLine())
			{
				parser.ExecuteCommand(line);
			}
			
			var output=new AccountsStandardOutput(
				register.GetAccounts(),
				register.GetCheckAccounts());
			foreach (var line in output.Lines()) {
				Console.Out.WriteLine(line);
			}
		}
	}
}
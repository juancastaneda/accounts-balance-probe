
using System;
using System.Text.RegularExpressions;

namespace MoneyTrackApp.Accounting.Parsing
{
	/// <summary>
	/// Description of AddAccountParser.
	/// </summary>
	public sealed class AddAccountParser:ICommandHandler
	{
		private static readonly Regex addCommandRegex=new Regex(@"add (check|income|expense) account[ \t](.*)");
		private readonly Action<ParsedArguments> commandAction;
		
		public AddAccountParser(Action<ParsedArguments> commandAction)
		{
			if (commandAction==null){
				throw new ArgumentNullException("commandAction");
			}
			
			this.commandAction=commandAction;
		}

		public bool CanHandleCommand(string command)
		{
			return addCommandRegex.IsMatch(command);
		}

		public void HandleCommand(string command)
		{
			var match=addCommandRegex.Match(command);
			var parsedArguments = new ParsedArguments(
				match.Groups[1].Value.Trim(),
				match.Groups[2].Value.Trim());
			commandAction(parsedArguments);
		}
		
		public sealed class ParsedArguments
		{
			private readonly string accountName;
			private readonly string accountType;
			public ParsedArguments(
				string accountType,
				string accountName)
			{
				this.accountType=accountType;
				this.accountName=accountName;
			}

			public string AccountType
			{
				get
				{
					return accountType;
				}
			}
			
			public string AccountName
			{
				get
				{
					return accountName;
				}
			}
		}
	}
}

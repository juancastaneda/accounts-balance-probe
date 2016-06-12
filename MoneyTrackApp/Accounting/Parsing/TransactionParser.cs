using System;
using System.Text.RegularExpressions;

namespace MoneyTrackApp.Accounting.Parsing
{
    /// <summary>
    /// Description of TransactionParser.
    /// </summary>
    public class TransactionParser : ICommandHandler
    {
        private static readonly Regex transactionCommandRegex = new Regex(@"transaction from:(.*)[ \t]+to:(.*)[ \t]+(\d*)");
        private readonly Action<ParsedArguments> commandAction;

        public TransactionParser(Action<ParsedArguments> commandAction)
        {
            if (commandAction == null)
            {
                throw new ArgumentNullException("commandAction");
            }

            this.commandAction = commandAction;
        }

        public bool CanHandleCommand(string command)
        {
            return transactionCommandRegex.IsMatch(command);
        }

        public void HandleCommand(string command)
        {
            var match = transactionCommandRegex.Match(command);
            var args = new ParsedArguments(
                match.Groups[1].Value.Trim(),
                match.Groups[2].Value.Trim(),
                int.Parse(match.Groups[3].Value)
            );
            commandAction(args);
        }

        public sealed class ParsedArguments
        {
            private readonly string accountFrom;
            private readonly string accountTo;
            private readonly int amount;
            public ParsedArguments(string accountFrom, string accountTo, int amount)
            {
                this.accountFrom = accountFrom;
                this.accountTo = accountTo;
                this.amount = amount;
            }

            public int Amount
            {
                get
                {
                    return amount;
                }
            }
            public string AccountFrom
            {
                get
                {
                    return accountFrom;
                }
            }

            public string AccountTo
            {
                get
                {
                    return accountTo;
                }
            }
        }
    }
}

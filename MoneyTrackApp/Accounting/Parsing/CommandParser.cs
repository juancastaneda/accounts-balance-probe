namespace MoneyTrackApp.Accounting.Parsing
{
    /// <summary>
    /// Description of CommandParser.
    /// </summary>
    public class CommandParser
    {
        private readonly CompositeCommandHandler handler;

        public CommandParser(AccountRegister register)
        {
            var registerAdapter = new AccountRegisterAdapter(register);

            handler = new CompositeCommandHandler(
                new AddAccountParser(registerAdapter.CreateaAccount),
                new TransactionParser(registerAdapter.ExecuteTransaction)
            );
        }

        public void ExecuteCommand(string command)
        {
            handler.HandleCommand(command);
        }
    }
}

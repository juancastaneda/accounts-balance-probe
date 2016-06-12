namespace MoneyTrackApp.Accounting.Parsing
{
    /// <summary>
    /// Description of ICommandHandler.
    /// </summary>
    public interface ICommandHandler
	{
		bool CanHandleCommand(string command);
		void HandleCommand(string command);
	}
}

using System.Linq;

namespace MoneyTrackApp.Accounting.Parsing
{
    /// <summary>
    /// Description of CompositeCommandHandler.
    /// </summary>
    public class CompositeCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler[] handlers;

        public CompositeCommandHandler(params ICommandHandler[] handlers)
        {
            this.handlers = handlers;
        }


        public bool CanHandleCommand(string command)
        {
            return handlers.Any(h => h.CanHandleCommand(command));
        }

        public void HandleCommand(string command)
        {
            if (!CanHandleCommand(command))
            {
                return;
            }

            var handler = handlers.First(h => h.CanHandleCommand(command));
            handler.HandleCommand(command);
        }

    }
}

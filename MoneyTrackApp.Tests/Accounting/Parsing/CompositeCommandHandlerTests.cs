using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace MoneyTrackApp.Accounting.Parsing
{
    [TestFixture]
    public class CompositeCommandHandlerTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void Can_handle_command_if_one_handle_can(bool canHandleCommand)
        {
            var fixture = new Fixture();
            fixture.commandHandlers.AddRange(
                new[]{
                    new TestCommandHandler(false),
                    new TestCommandHandler(false),
                    new TestCommandHandler(canHandleCommand)
                });
            var sut = fixture.CreateSUT();

            var actual = sut.CanHandleCommand("something");

            Assert.AreEqual(canHandleCommand, actual);
        }

        [Test]
        public void Can_handle_command_with_first_handler_that_can_handle()
        {
            var fixture = new Fixture();
            const string commandTest = "a";
            var firstHandler = new TestCommandHandler(true, commandTest.Equals);
            var secondHandler = new TestCommandHandler(true, commandTest.Equals);
            fixture.commandHandlers.AddRange(
                new[]{
                    new TestCommandHandler(false),
                    new TestCommandHandler(false),
                    firstHandler,
                    secondHandler
                });
            var sut = fixture.CreateSUT();

            sut.HandleCommand(commandTest);

            Assert.IsTrue(firstHandler.HasHandleCommand, "first handler was not used");
            Assert.IsFalse(secondHandler.HasHandleCommand, "second handler was used");
        }

        [Test]
        public void Can_handle_command_with_no_handler_that_can_handle()
        {
            var fixture = new Fixture();
            const string commandTest = "a";
            var handler = new TestCommandHandler(false, commandTest.Equals);
            fixture.commandHandlers.AddRange(
                new[]{
                    new TestCommandHandler(false),
                    new TestCommandHandler(false),
                    handler
                });
            var sut = fixture.CreateSUT();

            sut.HandleCommand(commandTest);

            Assert.IsFalse(handler.HasHandleCommand, "handler was used");
        }

        private class Fixture
        {
            public readonly List<ICommandHandler> commandHandlers
                = new List<ICommandHandler>();
            public CompositeCommandHandler CreateSUT()
            {
                return new CompositeCommandHandler(commandHandlers.ToArray());
            }
        }

        private class TestCommandHandler : ICommandHandler
        {
            private bool hasHandleCommand;
            private readonly bool canHandleCommand;
            private readonly Predicate<string> testForCommand;
            public TestCommandHandler(
                bool canHandleCommand,
                Predicate<string> testForCommand = null)
            {
                this.canHandleCommand = canHandleCommand;
                this.testForCommand = testForCommand;
            }

            public bool HasHandleCommand
            {
                get
                {
                    return hasHandleCommand;
                }
            }

            public bool CanHandleCommand(string command)
            {
                return canHandleCommand;
            }

            public void HandleCommand(string command)
            {
                if (testForCommand != null)
                {
                    hasHandleCommand = testForCommand(command);
                }
                else {
                    hasHandleCommand = true;
                }
            }
        }
    }
}

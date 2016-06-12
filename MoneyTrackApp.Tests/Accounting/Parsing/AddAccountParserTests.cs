using NUnit.Framework;
using System;

namespace MoneyTrackApp.Accounting.Parsing
{
    [TestFixture]
    public class AddAccountParserTests
    {
        [TestCase("add check account account name")]
        [TestCase("add income account account name")]
        [TestCase("add expense account account name")]
        public void Can_verify_can_execute_add_command(string commandLine)
        {
            var sut = new Fixture().CreateSUT();

            var actual = sut.CanHandleCommand(commandLine);

            Assert.IsTrue(actual, "can handle command:" + commandLine);
        }

        [TestCase("add checks account account name")]
        [TestCase("add incomef account account name")]
        [TestCase("add expensex account account name")]
        public void Cannot_verify_can_execute_if_missing_account_type(string commandLine)
        {
            var sut = new Fixture().CreateSUT();

            var actual = sut.CanHandleCommand(commandLine);

            Assert.IsFalse(actual, "can execute incomplete command:" + commandLine);
        }

        [TestCase("add check account my check account", "check", "my check account")]
        [TestCase("add income account my income account", "income", "my income account")]
        [TestCase("add expense account my expense account", "expense", "my expense account")]
        [TestCase("add check account check", "check", "check")]
        [TestCase("add income account income", "income", "income")]
        [TestCase("add expense account expense", "expense", "expense")]
        [TestCase("add check account   check  ", "check", "check")]
        [TestCase("add income account   income  ", "income", "income")]
        [TestCase("add expense account   expense  ", "expense", "expense")]
        [TestCase("add check account\tcheck  ", "check", "check")]
        [TestCase("add income account\tincome  ", "income", "income")]
        [TestCase("add expense account\texpense  ", "expense", "expense")]
        public void Can_call_action_with_right_parameters(
            string commandLine,
            string accountType,
            string accountName)
        {
            var fixture = new Fixture();
            AddAccountParser.ParsedArguments actual = null;
            fixture.commandAction = a => actual = a;
            var sut = fixture.CreateSUT();

            sut.HandleCommand(commandLine);

            Assert.IsNotNull(actual, "parameters to actios");
            Assert.AreEqual(accountType, actual.AccountType, "account type. line:" + commandLine);
            Assert.AreEqual(accountName, actual.AccountName, "account name. line:" + commandLine);
        }

        private sealed class Fixture
        {
            public Action<AddAccountParser.ParsedArguments> commandAction =
                a => { };
            public AddAccountParser CreateSUT()
            {
                return new AddAccountParser(commandAction);
            }
        }
    }
}

using NUnit.Framework;

namespace MoneyTrackApp.Accounting.Parsing
{
    [TestFixture]
    public class AccountRegisterAdapterTests
    {
        [Test]
        public void Can_create_check_account()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();

            sut.CreateaAccount(new AddAccountParser.ParsedArguments("check", "a"));

            var actual = fixture.Register.GetAccount("a");

            Assert.IsNotNull(actual, "check account");
        }

        [Test]
        public void Can_create_expense_account()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();

            sut.CreateaAccount(new AddAccountParser.ParsedArguments("expense", "a"));

            var actual = fixture.Register.GetAccount("a");

            Assert.IsNotNull(actual, "expense account");
        }

        [Test]
        public void Can_create_income_account()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();

            sut.CreateaAccount(new AddAccountParser.ParsedArguments("income", "a"));

            var actual = fixture.Register.GetAccount("a");

            Assert.IsNotNull(actual, "income account");
        }

        [Test]
        public void Can_do_transfers()
        {
            var fixture = new Fixture();
            fixture.Register.CreateCheckAccount("a");
            fixture.Register.CreateExpenseAccount("b");
            var sut = fixture.CreateSUT();

            sut.ExecuteTransaction(new TransactionParser.ParsedArguments("a", "b", 10));

            var fromAccount = fixture.Register.GetAccount("a");
            var toAccount = fixture.Register.GetAccount("b");

            Assert.AreNotEqual(0, fromAccount.Balance, "from account was moved");
            Assert.AreNotEqual(0, toAccount.Balance, "to account was moved");
        }

        private class Fixture
        {
            public readonly AccountRegister Register
                = new AccountRegister();

            public AccountRegisterAdapter CreateSUT()
            {
                return new AccountRegisterAdapter(Register);
            }
        }
    }
}

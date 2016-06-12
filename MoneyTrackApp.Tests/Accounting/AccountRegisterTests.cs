using System;
using System.Linq;
using NUnit.Framework;

namespace MoneyTrackApp.Accounting
{
    [TestFixture]
    public class AccountRegisterTests
    {
        [Test]
        public void Can_register_many_check_accounts()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateCheckAccount("a");
            sut.CreateCheckAccount("b");
            sut.CreateCheckAccount("c");

            var actual = sut.GetAccounts();
            var expected = new[] {
                new AccountByNameEquals("a"),
                new AccountByNameEquals("b"),
                new AccountByNameEquals("c")
            };
            CollectionAssert.AreEquivalent(
                expected,
                actual,
                "check account notfound");
        }

        [Test]
        public void Can_register_an_income_account()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateIncomeAccount("a");
            sut.CreateIncomeAccount("b");
            sut.CreateIncomeAccount("c");

            var actual = sut.GetAccounts();
            var expected = new[] {
                new AccountByNameEquals("a"),
                new AccountByNameEquals("b"),
                new AccountByNameEquals("c")
            };
            CollectionAssert.AreEquivalent(
                expected,
                actual,
                "income account notfound");
        }

        [Test]
        public void Can_register_an_expense_account()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateExpenseAccount("a");
            sut.CreateExpenseAccount("b");
            sut.CreateExpenseAccount("c");

            var actual = sut.GetAccounts();
            var expected = new[] {
                new AccountByNameEquals("a"),
                new AccountByNameEquals("b"),
                new AccountByNameEquals("c")
            };
            CollectionAssert.AreEquivalent(
                expected,
                actual,
                "expense account notfound");
        }

        [Test]
        public void Can_get_check_accounts_only()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateExpenseAccount("a");
            sut.CreateCheckAccount("check");
            sut.CreateIncomeAccount("c");

            var actual = sut.GetAccounts().GetCheckingAccounts();
            var expected = new[] {
                new AccountByNameEquals("check")
            };
            CollectionAssert.AreEquivalent(
                expected,
                actual,
                "check account notfound");
        }

        [Test]
        public void Cannot_have_a_check_account_with_more_than_20_characters()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            const int maxAccountNameLength = 20;
            var name = new string('f', maxAccountNameLength + 10);
            var expectedName = name.Substring(0, maxAccountNameLength);

            sut.CreateCheckAccount(name);
            var actual = sut.GetAccounts();

            CollectionAssert.AreEquivalent(
                new[] { new AccountByNameEquals(expectedName) },
                actual,
                "should truncate check account name"
            );
        }

        [Test]
        public void Cannot_have_an_expense_account_with_more_than_20_characters()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            const int maxAccountNameLength = 20;
            var name = new string('f', maxAccountNameLength + 10);
            var expectedName = name.Substring(0, maxAccountNameLength);

            sut.CreateExpenseAccount(name);
            var actual = sut.GetAccounts();

            CollectionAssert.AreEquivalent(
                new[] { new AccountByNameEquals(expectedName) },
                actual,
                "should truncate expense account name"
            );
        }

        [Test]
        public void Cannot_have_an_income_account_with_more_than_20_characters()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            const int maxAccountNameLength = 20;
            var name = new string('f', maxAccountNameLength + 10);
            var expectedName = name.Substring(0, maxAccountNameLength);

            sut.CreateIncomeAccount(name);
            var actual = sut.GetAccounts();

            CollectionAssert.AreEquivalent(
                new[] { new AccountByNameEquals(expectedName) },
                actual,
                "should truncate income account name"
            );
        }

        [Test]
        public void Can_createt_check_account_with_zero_balance()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateCheckAccount("a");

            var actual = sut.GetAccounts().ElementAt(0).Balance;

            Assert.AreEqual(0, actual, "account balance");
        }

        [Test]
        public void Can_createt_expense_account_with_zero_balance()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateExpenseAccount("a");

            var actual = sut.GetAccounts().ElementAt(0).Balance;

            Assert.AreEqual(0, actual, "account balance");
        }

        [Test]
        public void Can_create_income_account_with_zero_balance()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateIncomeAccount("a");

            var actual = sut.GetAccounts().ElementAt(0).Balance;

            Assert.AreEqual(0, actual, "account balance");
        }

        [Test]
        public void Can_get_account_by_name()
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateIncomeAccount("a");

            var actual = sut.GetAccount("a");

            var expected = new AccountByNameEquals("a");
            Assert.AreEqual(expected, actual, "account instance");
        }

        [TestCase("incom1", "check1", "expense1", 10, "incom1", "check1")]
        [TestCase("incom2", "check2", "expense2", 120, "incom2", "check2")]
        [TestCase("incom3", "check3", "expense3", 50, "incom3", "check3")]
        [TestCase("incom1", "check1", "expense1", 10, "incom1", "expense1")]
        [TestCase("incom2", "check2", "expense2", 120, "incom2", "expense2")]
        [TestCase("incom3", "check3", "expense3", 50, "incom3", "expense3")]
        [TestCase("incom1", "check1", "expense1", 10, "expense1", "check1")]
        [TestCase("incom2", "check2", "expense2", 120, "expense2", "check2")]
        [TestCase("incom3", "check3", "expense3", 50, "expense3", "check3")]
        [TestCase("incom1", "check1", "expense1", 10, "expense1", "incom1")]
        [TestCase("incom2", "check2", "expense2", 120, "expense2", "incom2")]
        [TestCase("incom3", "check3", "expense3", 50, "expense3", "incom3")]
        [TestCase("incom1", "check1", "expense1", 10, "check1", "incom1")]
        [TestCase("incom2", "check2", "expense2", 120, "check2", "incom2")]
        [TestCase("incom3", "check3", "expense3", 50, "check3", "incom3")]
        [TestCase("incom1", "check1", "expense1", 10, "check1", "expense1")]
        [TestCase("incom2", "check2", "expense2", 120, "check2", "expense2")]
        [TestCase("incom3", "check3", "expense3", 50, "check3", "expense3")]
        public void Can_move_money_between_accounts(
            string incomeAccountName,
            string checkAccountName,
            string expenseAccountName,
            int amount,
            string transactionFrom,
            string transactionTo)
        {
            var fixture = new Fixture();
            var sut = fixture.CreateSUT();
            sut.CreateIncomeAccount(incomeAccountName);
            sut.CreateCheckAccount(checkAccountName);
            sut.CreateExpenseAccount(expenseAccountName);

            sut.Move(amount).From(transactionFrom).To(transactionTo);
            sut.Move(amount).From(transactionFrom).To(transactionTo);
            sut.Move(amount).From(transactionFrom).To(transactionTo);

            var from = sut.GetAccount(transactionFrom);
            var to = sut.GetAccount(transactionTo);

            Assert.AreEqual(-3 * amount, from.Balance, "removed from fromAccount");
            Assert.AreEqual(3 * amount, to.Balance, "added to toAccount");
        }

        private class Fixture
        {
            public AccountRegister CreateSUT()
            {
                return new AccountRegister();
            }
        }

        private class AccountByNameEquals : IAccount
        {
            public AccountByNameEquals(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                Name = name;
            }

            public int Balance
            {
                get; private set;
            }

            public string Name
            {
                get; private set;
            }

            public override bool Equals(object obj)
            {
                var other = obj as IAccount;
                if (other == null)
                {
                    return false;
                }

                return Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return string.Format("[AccountByNameEquals Name={0}]", Name);
            }

        }
    }
}


using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MoneyTrackApp.Accounting.Output
{
	[TestFixture]
	public class AccountsStandardOutputTests
	{
		[Test]
		public void Can_return_empty_on_no_accounts()
		{
			var fixture=new Fixture();
			var sut=fixture.CreateSUT();
			var expected=new []{"(no accounts)"};
			
			var actual=sut.Lines();
			
			CollectionAssert.AreEquivalent(expected,actual);
		}
		
		[Test]
		public void Can_return_ordered_by_account_name()
		{
			var fixture=new Fixture();
			fixture.allAccounts.AddRange(
				new []{
					ConfigurableAccount.WithName("f 3").WithBalanceZero(),
					ConfigurableAccount.WithName("a").WithBalanceZero(),
					ConfigurableAccount.WithName("z a").WithBalanceZero()
				}
			);
			var sut=fixture.CreateSUT();
			var expected=new []{"a:0","f 3:0","z a:0"};
			
			var actual=sut.Lines();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		[Test]
		public void Can_show_balance_on_non_check_accounts_as_negative()
		{
			var fixture=new Fixture();
			fixture.allAccounts.AddRange(
				new []{
					ConfigurableAccount.WithName("a").WithBalance(4),
					ConfigurableAccount.WithName("b").WithBalance(2),
					ConfigurableAccount.WithName("c").WithBalance(-3),
					ConfigurableAccount.WithName("d").WithBalance(-5)
				}
			);
			var sut=fixture.CreateSUT();
			var expected=new []{"a:-4","b:-2","c:3","d:5"};
			
			var actual=sut.Lines();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		[Test]
		public void Can_credit_balance_on_check_accounts_as_positive()
		{
			var fixture=new Fixture();
			var checkAccount1 = ConfigurableAccount.WithName("a").WithBalance(4);
			var checkAccount2 = ConfigurableAccount.WithName("b").WithBalance(-2);
			fixture.allAccounts.AddRange(
				new []
				{
					checkAccount1,
					checkAccount2,
				}
			);
			fixture.checkAccounts.AddRange(
				new[]{
					checkAccount2,
					checkAccount1
				}
			);
			var sut=fixture.CreateSUT();
			var expected=new []{"a:4","b:-2"};
			
			var actual=sut.Lines();
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private sealed class Fixture{
			public List<IAccount> allAccounts
				=new List<IAccount>();
			public List<IAccount> checkAccounts
				=new List<IAccount>();
			
			public AccountsStandardOutput CreateSUT(){
				return new AccountsStandardOutput(allAccounts.ToArray(),checkAccounts.ToArray());
			}
		}
	}
}

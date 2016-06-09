namespace MoneyTrackApp.Accounting
{
	using System.Collections.Generic;
	using System.Linq;
	
	/// <summary>
	/// Manages the accounts for the system
	/// </summary>
	public sealed class AccountRegister
	{
		private const int MaxAccountNameLength=20;
		private readonly Dictionary<string,Account> accountsByName=
			new Dictionary<string,Account> ();
		private readonly List<string> existingCheckAccountNames=
			new List<string>();
		public IAccount[] GetAccounts()
		{
			return accountsByName
				.Select(kvp=>kvp.Value)
				.ToArray();
		}

		public IAccount GetAccount(string accountName)
		{
			return FindAccount(accountName);
		}
		
		private Account FindAccount(string accountName)
		{
			if (!accountsByName.ContainsKey(accountName)){
				return null;
			}
			return accountsByName[accountName];
		}
		
		public void CreateCheckAccount(string accountName)
		{
			existingCheckAccountNames.Add(accountName);
			accountsByName.Add(accountName,new Account(accountName));
		}

		public void CreateExpenseAccount(string accountName)
		{
			accountsByName.Add(accountName,new Account(accountName));
		}
		
		public void CreateIncomeAccount(string accountName)
		{
			accountsByName.Add(accountName,new Account(accountName));
		}

		public TransactionBuilder Move(int amount)
		{
			return new TransactionBuilder(this,amount);
		}
		
		public IAccount[] GetCheckAccounts()
		{
			return existingCheckAccountNames
				.Select(GetAccount)
				.ToArray();
		}
		
		public sealed class TransactionBuilder{
			private readonly			AccountRegister parent;
			private readonly			int amount;
			private string fromAccountName;
			
			public TransactionBuilder(AccountRegister parent, int amount)
			{
				this.parent=parent;
				this.amount=amount;
			}

			public TransactionBuilder From(string accountName)
			{
				fromAccountName=accountName;
				return this;
			}

			public void To(string destinationAccountName)
			{
				var from=parent.FindAccount(fromAccountName);
				var to=parent.FindAccount(destinationAccountName);
				
				from.Withdraw(amount);
				to.Credit(amount);
			}
		}
		
		private sealed class Account : IAccount
		{
			private readonly string name;
			private int balance;
			
			public Account(string name)
			{
				var nameLength = name.Length > MaxAccountNameLength ? MaxAccountNameLength : name.Length;
				this.name= name.Substring(0, nameLength);
			}
			
			public string Name
			{
				get
				{
					return name;
				}
			}
			
			public int Balance
			{
				get { return balance; }
			}
			
			public void Withdraw(int amount)
			{
				balance-=amount;
			}

			public void Credit(int amount)
			{
				balance+=amount;
			}
			
			public override string ToString()
			{
				return string.Format("[Account Name={0}]", name);
			}
		}
	}
}
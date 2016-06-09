
using System;
using System.Linq;

namespace MoneyTrackApp.Accounting
{
	/// <summary>
	/// Description of ConfigurableAccount.
	/// </summary>
	public class ConfigurableAccount:IAccount
	{
		private readonly string name;
		private readonly int balance;
		public ConfigurableAccount(string name, int balance)
		{
			this.name=name;
			this.balance=balance;
		}

		public int Balance
		{
			get
			{
				return balance;			}
		}

		public string Name
		{
			get
			{
				return name;			}
		}

		public static Builder WithName(string value){
			return new Builder(value);
		}
		
		public sealed class Builder{
			private readonly string name;
			public Builder(string name)
			{
				this.name=name;
			}
			
			public ConfigurableAccount WithBalanceZero(){
				return new ConfigurableAccount(name,0);
			}
			
			public ConfigurableAccount WithBalance(int value){
				return new ConfigurableAccount(name, value);
			}
		}
	}
}

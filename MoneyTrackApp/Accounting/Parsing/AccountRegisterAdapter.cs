
using System;
using System.Collections.Generic;

namespace MoneyTrackApp.Accounting.Parsing
{
	/// <summary>
	/// Description of AccountRegisterAdapter.
	/// </summary>
	public class AccountRegisterAdapter
	{
		private readonly Dictionary<string,Action<AddAccountParser.ParsedArguments>> registerActionByAccountType;
		private readonly AccountRegister register;
		public AccountRegisterAdapter(AccountRegister register)
		{
			registerActionByAccountType=new Dictionary<string, Action<AddAccountParser.ParsedArguments>>{
				{"check",a=>register.CreateCheckAccount(a.AccountName)},
				{"expense",a=>register.CreateExpenseAccount(a.AccountName)},
				{"income",a=>register.CreateIncomeAccount(a.AccountName)}
			};
			this.register=register;
		}
		
		public void CreateaAccount(AddAccountParser.ParsedArguments args){
			if (args==null){
				throw new ArgumentNullException("args");
			}
			
			if (!registerActionByAccountType.ContainsKey(args.AccountType)){
				throw new ArgumentOutOfRangeException("args","Unknown account type");
			}
			
			registerActionByAccountType[args.AccountType](args);
		}
		
		public void ExecuteTransaction(TransactionParser.ParsedArguments args){
			if (args==null){
				throw new ArgumentNullException("args");
			}
			
			register
				.Move(args.Amount)
				.From(args.AccountFrom)
				.To(args.AccountTo);
		}
	}
}

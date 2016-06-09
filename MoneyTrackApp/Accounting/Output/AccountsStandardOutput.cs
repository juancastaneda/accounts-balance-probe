
using System;
using System.Linq;
using System.Collections.Generic;

namespace MoneyTrackApp.Accounting.Output
{
	/// <summary>
	/// Description of AccountsStandardOutput.
	/// </summary>
	public class AccountsStandardOutput
	{
		private const string NoAccountsText="(no accounts)";
		
		private readonly		IAccount[] allAccounts;
		private readonly		IAccount[] checkAccounts;

		public AccountsStandardOutput(
			IAccount[] allAccounts,
			IAccount[] checkAccounts)
		{
			this.allAccounts=allAccounts;
			this.checkAccounts=checkAccounts;
		}

		public IEnumerable<string> Lines(){
			if (allAccounts.Length==0){
				yield return NoAccountsText;
			}
			
			foreach (var account in allAccounts.OrderBy(a=>a.Name)) {
				if (checkAccounts.Contains(account)){
					yield return string.Format("{0}:{1}",account.Name,account.Balance);
				}else{
					yield return string.Format("{0}:{1}",account.Name,-account.Balance);
				}
			}
		}
	}
}

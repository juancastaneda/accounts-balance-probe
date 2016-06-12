using System.Collections.Generic;

namespace MoneyTrackApp.Accounting
{
	/// <summary>
	/// Represents the accounts for a holder of accounts
	/// </summary>
	public interface IRegisterAccounts : IReadOnlyCollection<IAccount>
	{
		IReadOnlyCollection<IAccount> GetCheckingAccounts();
	}
}

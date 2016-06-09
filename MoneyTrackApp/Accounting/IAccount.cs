
using System;

namespace MoneyTrackApp.Accounting
{
	/// <summary>
	/// Represents where the money is stored
	/// </summary>
	public interface IAccount
	{
		int Balance
		{
			get;
		}

		string Name{get;}
	}
}

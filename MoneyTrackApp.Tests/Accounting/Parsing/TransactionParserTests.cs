
using System;
using NUnit.Framework;

namespace MoneyTrackApp.Accounting.Parsing
{
	[TestFixture]
	public class TransactionParserTests
	{
		[TestCase("transaction from:my account to:other account 10")]
		[TestCase("transaction from:from to:to 20")]
		public void Can_verify_can_execute_add_command(string commandLine)
		{
			var sut=new Fixture().CreateSUT();

			var actual=sut.CanHandleCommand(commandLine);

			Assert.IsTrue(actual,"can handle command:" + commandLine);
		}
		
		[TestCase("transaction from:my account to:other account 10", "my account","other account",10)]
		[TestCase("transaction from:from to:to 20", "from","to",20)]
		[TestCase("transaction from:my account\t\tto:other account\t\t10", "my account","other account",10)]
		[TestCase("transaction from:from\t\tto:to\t\t\t20", "from","to",20)]
		public void Can_call_action_with_right_parameters(
			string commandLine,
			string expectedAccountFrom,
			string expectedAccountTo,
			int expectedAmount)
		{
			var fixture=new Fixture();
			TransactionParser.ParsedArguments actual=null;
			fixture.commandAction=a=>actual=a;
			var sut=fixture.CreateSUT();

			sut.HandleCommand(commandLine);

			Assert.IsNotNull(actual,"parameters to actios");
			Assert.AreEqual(expectedAccountFrom,actual.AccountFrom,"account from");
			Assert.AreEqual(expectedAccountTo,actual.AccountTo,"account to");
			Assert.AreEqual(expectedAmount,actual.Amount,"account to");
		}
		
		private class Fixture{
			
			public Action<TransactionParser.ParsedArguments> commandAction=
				a=> { };
			public TransactionParser CreateSUT(){
				return new TransactionParser(commandAction);
			}
		}
	}
}

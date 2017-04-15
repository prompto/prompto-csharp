using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestExit : BaseEParserTest
	{

		[Test]
		public void testAssignedReturn()
		{
			compareResourceEOE("exit/assignedReturn.pec");
		}

		[Test]
		public void testAssignedReturnInDoWhile()
		{
			compareResourceEOE("exit/assignedReturnInDoWhile.pec");
		}

		[Test]
		public void testAssignedReturnInForEach()
		{
			compareResourceEOE("exit/assignedReturnInForEach.pec");
		}

		[Test]
		public void testAssignedReturnInIf()
		{
			compareResourceEOE("exit/assignedReturnInIf.pec");
		}

		[Test]
		public void testAssignedReturnInWhile()
		{
			compareResourceEOE("exit/assignedReturnInWhile.pec");
		}

		[Test]
		public void testUnassignedReturn()
		{
			compareResourceEOE("exit/unassignedReturn.pec");
		}

		[Test]
		public void testUnassignedReturnInDoWhile()
		{
			compareResourceEOE("exit/unassignedReturnInDoWhile.pec");
		}

		[Test]
		public void testUnassignedReturnInForEach()
		{
			compareResourceEOE("exit/unassignedReturnInForEach.pec");
		}

		[Test]
		public void testUnassignedReturnInIf()
		{
			compareResourceEOE("exit/unassignedReturnInIf.pec");
		}

		[Test]
		public void testUnassignedReturnInWhile()
		{
			compareResourceEOE("exit/unassignedReturnInWhile.pec");
		}

	}
}


using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestExit : BaseEParserTest
	{

		[Test]
		public void testAssignedReturn()
		{
			compareResourceEME("exit/assignedReturn.pec");
		}

		[Test]
		public void testAssignedReturnInDoWhile()
		{
			compareResourceEME("exit/assignedReturnInDoWhile.pec");
		}

		[Test]
		public void testAssignedReturnInForEach()
		{
			compareResourceEME("exit/assignedReturnInForEach.pec");
		}

		[Test]
		public void testAssignedReturnInIf()
		{
			compareResourceEME("exit/assignedReturnInIf.pec");
		}

		[Test]
		public void testAssignedReturnInWhile()
		{
			compareResourceEME("exit/assignedReturnInWhile.pec");
		}

		[Test]
		public void testUnassignedReturn()
		{
			compareResourceEME("exit/unassignedReturn.pec");
		}

		[Test]
		public void testUnassignedReturnInDoWhile()
		{
			compareResourceEME("exit/unassignedReturnInDoWhile.pec");
		}

		[Test]
		public void testUnassignedReturnInForEach()
		{
			compareResourceEME("exit/unassignedReturnInForEach.pec");
		}

		[Test]
		public void testUnassignedReturnInIf()
		{
			compareResourceEME("exit/unassignedReturnInIf.pec");
		}

		[Test]
		public void testUnassignedReturnInWhile()
		{
			compareResourceEME("exit/unassignedReturnInWhile.pec");
		}

	}
}


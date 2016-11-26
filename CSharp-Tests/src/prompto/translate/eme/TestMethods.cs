using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceEME("methods/anonymous.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEME("methods/attribute.pec");
		}

		[Test]
		public void testDefault()
		{
			compareResourceEME("methods/default.pec");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceEME("methods/e_as_e_bug.pec");
		}

		[Test]
		public void testExplicit()
		{
			compareResourceEME("methods/explicit.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceEME("methods/expressionWith.pec");
		}

		[Test]
		public void testExtended()
		{
			compareResourceEME("methods/extended.pec");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceEME("methods/implicit.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEME("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			compareResourceEME("methods/memberCall.pec");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceEME("methods/polymorphic_abstract.pec");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceEME("methods/polymorphic_implicit.pec");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceEME("methods/polymorphic_named.pec");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceEME("methods/polymorphic_runtime.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEME("methods/return.pec");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceEME("methods/specified.pec");
		}

	}
}

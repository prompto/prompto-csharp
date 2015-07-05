// generated: 2015-07-05T23:01:01.326
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceESE("methods/anonymous.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceESE("methods/attribute.pec");
		}

		[Test]
		public void testDefault()
		{
			compareResourceESE("methods/default.pec");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceESE("methods/e_as_e_bug.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceESE("methods/expressionWith.pec");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceESE("methods/implicit.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceESE("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			compareResourceESE("methods/memberCall.pec");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceESE("methods/polymorphic_abstract.pec");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceESE("methods/polymorphic_implicit.pec");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceESE("methods/polymorphic_named.pec");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceESE("methods/polymorphic_runtime.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceESE("methods/return.pec");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceESE("methods/specified.pec");
		}

	}
}


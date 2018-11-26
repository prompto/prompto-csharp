using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceEOE("methods/anonymous.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEOE("methods/attribute.pec");
		}

		[Test]
		public void testDefault()
		{
			compareResourceEOE("methods/default.pec");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceEOE("methods/e_as_e_bug.pec");
		}

		[Test]
		public void testExplicit()
		{
			compareResourceEOE("methods/explicit.pec");
		}

		[Test]
		public void testExplicitMember()
		{
			compareResourceEOE("methods/explicitMember.pec");
		}

		[Test]
		public void testExpressionMember()
		{
			compareResourceEOE("methods/expressionMember.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceEOE("methods/expressionWith.pec");
		}

		[Test]
		public void testExtended()
		{
			compareResourceEOE("methods/extended.pec");
		}

		[Test]
		public void testGlobal()
		{
			compareResourceEOE("methods/global.pec");
		}

		[Test]
		public void testHomonym()
		{
			compareResourceEOE("methods/homonym.pec");
		}

		[Test]
		public void testImplicitAnd()
		{
			compareResourceEOE("methods/implicitAnd.pec");
		}

		[Test]
		public void testImplicitMember()
		{
			compareResourceEOE("methods/implicitMember.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEOE("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			compareResourceEOE("methods/memberCall.pec");
		}

		[Test]
		public void testOverride()
		{
			compareResourceEOE("methods/override.pec");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceEOE("methods/polymorphic_abstract.pec");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceEOE("methods/polymorphic_implicit.pec");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceEOE("methods/polymorphic_named.pec");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceEOE("methods/polymorphic_runtime.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEOE("methods/return.pec");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceEOE("methods/specified.pec");
		}

		[Test]
		public void testTextAsync()
		{
			compareResourceEOE("methods/textAsync.pec");
		}

		[Test]
		public void testVoidAsync()
		{
			compareResourceEOE("methods/voidAsync.pec");
		}

	}
}


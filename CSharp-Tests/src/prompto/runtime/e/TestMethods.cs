using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testAnonymous()
		{
			CheckOutput("methods/anonymous.pec");
		}

		[Test]
		public void testAttribute()
		{
			CheckOutput("methods/attribute.pec");
		}

		[Test]
		public void testDefault()
		{
			CheckOutput("methods/default.pec");
		}

		[Test]
		public void testE_as_e_bug()
		{
			CheckOutput("methods/e_as_e_bug.pec");
		}

		[Test]
		public void testExplicit()
		{
			CheckOutput("methods/explicit.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.pec");
		}

		[Test]
		public void testExtended()
		{
			CheckOutput("methods/extended.pec");
		}

		[Test]
		public void testHomonym()
		{
			CheckOutput("methods/homonym.pec");
		}

		[Test]
		public void testImplicitMember()
		{
			CheckOutput("methods/implicitMember.pec");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			CheckOutput("methods/memberCall.pec");
		}

		[Test]
		public void testOverride()
		{
			CheckOutput("methods/override.pec");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			CheckOutput("methods/polymorphic_abstract.pec");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			CheckOutput("methods/polymorphic_implicit.pec");
		}

		[Test]
		public void testPolymorphic_named()
		{
			CheckOutput("methods/polymorphic_named.pec");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			CheckOutput("methods/polymorphic_runtime.pec");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.pec");
		}

	}
}


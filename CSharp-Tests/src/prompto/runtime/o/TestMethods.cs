using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
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
		public void testAbstractMember()
		{
			CheckOutput("methods/abstractMember.poc");
		}

		[Test]
		public void testAnonymous()
		{
			CheckOutput("methods/anonymous.poc");
		}

		[Test]
		public void testAttribute()
		{
			CheckOutput("methods/attribute.poc");
		}

		[Test]
		public void testDefault()
		{
			CheckOutput("methods/default.poc");
		}

		[Test]
		public void testE_as_e_bug()
		{
			CheckOutput("methods/e_as_e_bug.poc");
		}

		[Test]
		public void testExplicit()
		{
			CheckOutput("methods/explicit.poc");
		}

		[Test]
		public void testExplicitMember()
		{
			CheckOutput("methods/explicitMember.poc");
		}

		[Test]
		public void testExpressionMember()
		{
			CheckOutput("methods/expressionMember.poc");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.poc");
		}

		[Test]
		public void testExtended()
		{
			CheckOutput("methods/extended.poc");
		}

		[Test]
		public void testImplicitMember()
		{
			CheckOutput("methods/implicitMember.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.poc");
		}

		[Test]
		public void testOverride()
		{
			CheckOutput("methods/override.poc");
		}

		[Test]
		public void testParameter()
		{
			CheckOutput("methods/parameter.poc");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			CheckOutput("methods/polymorphic_abstract.poc");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			CheckOutput("methods/polymorphic_implicit.poc");
		}

		[Test]
		public void testPolymorphic_named()
		{
			CheckOutput("methods/polymorphic_named.poc");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			CheckOutput("methods/polymorphic_runtime.poc");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.poc");
		}

		[Test]
		public void testTextAsync()
		{
			CheckOutput("methods/textAsync.poc");
		}

		[Test]
		public void testVoidAsync()
		{
			CheckOutput("methods/voidAsync.poc");
		}

	}
}


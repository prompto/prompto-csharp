using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.poc");
		}

		[Test]
		public void testImplicit()
		{
			CheckOutput("methods/implicit.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.poc");
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

	}
}


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
			CheckOutput("methods/anonymous.o");
		}

		[Test]
		public void testAttribute()
		{
			CheckOutput("methods/attribute.o");
		}

		[Test]
		public void testDefault()
		{
			CheckOutput("methods/default.o");
		}

		[Test]
		public void testE_as_e_bug()
		{
			CheckOutput("methods/e_as_e_bug.o");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.o");
		}

		[Test]
		public void testImplicit()
		{
			CheckOutput("methods/implicit.o");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.o");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			CheckOutput("methods/polymorphic_abstract.o");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			CheckOutput("methods/polymorphic_implicit.o");
		}

		[Test]
		public void testPolymorphic_named()
		{
			CheckOutput("methods/polymorphic_named.o");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			CheckOutput("methods/polymorphic_runtime.o");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.o");
		}

	}
}


using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("methods/anonymous.e");
		}

		[Test]
		public void testAttribute()
		{
			CheckOutput("methods/attribute.e");
		}

		[Test]
		public void testDefault()
		{
			CheckOutput("methods/default.e");
		}

		[Test]
		public void testE_as_e_bug()
		{
			CheckOutput("methods/e_as_e_bug.e");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.e");
		}

		[Test]
		public void testImplicit()
		{
			CheckOutput("methods/implicit.e");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.e");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			CheckOutput("methods/polymorphic_abstract.e");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			CheckOutput("methods/polymorphic_implicit.e");
		}

		[Test]
		public void testPolymorphic_named()
		{
			CheckOutput("methods/polymorphic_named.e");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			CheckOutput("methods/polymorphic_runtime.e");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.e");
		}

	}
}


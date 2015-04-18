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
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.pec");
		}

		[Test]
		public void testImplicit()
		{
			CheckOutput("methods/implicit.pec");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.pec");
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

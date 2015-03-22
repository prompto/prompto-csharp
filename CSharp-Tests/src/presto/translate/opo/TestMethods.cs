using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceOPO("methods/anonymous.o");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceOPO("methods/attribute.o");
		}

		[Test]
		public void testDefault()
		{
			compareResourceOPO("methods/default.o");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceOPO("methods/e_as_e_bug.o");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceOPO("methods/expressionWith.o");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceOPO("methods/implicit.o");
		}

		[Test]
		public void testMember()
		{
			compareResourceOPO("methods/member.o");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceOPO("methods/polymorphic_abstract.o");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceOPO("methods/polymorphic_implicit.o");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceOPO("methods/polymorphic_named.o");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceOPO("methods/polymorphic_runtime.o");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOPO("methods/return.o");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceOPO("methods/specified.o");
		}

	}
}


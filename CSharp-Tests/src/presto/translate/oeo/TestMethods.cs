using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceOEO("methods/anonymous.o");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceOEO("methods/attribute.o");
		}

		[Test]
		public void testDefault()
		{
			compareResourceOEO("methods/default.o");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceOEO("methods/e_as_e_bug.o");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceOEO("methods/expressionWith.o");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceOEO("methods/implicit.o");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("methods/member.o");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceOEO("methods/polymorphic_abstract.o");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceOEO("methods/polymorphic_implicit.o");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceOEO("methods/polymorphic_named.o");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceOEO("methods/polymorphic_runtime.o");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOEO("methods/return.o");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceOEO("methods/specified.o");
		}

	}
}


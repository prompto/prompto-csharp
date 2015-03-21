using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceEOE("methods/anonymous.e");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEOE("methods/attribute.e");
		}

		[Test]
		public void testDefault()
		{
			compareResourceEOE("methods/default.e");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceEOE("methods/e_as_e_bug.e");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceEOE("methods/expressionWith.e");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceEOE("methods/implicit.e");
		}

		[Test]
		public void testMember()
		{
			compareResourceEOE("methods/member.e");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceEOE("methods/polymorphic_abstract.e");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceEOE("methods/polymorphic_implicit.e");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceEOE("methods/polymorphic_named.e");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceEOE("methods/polymorphic_runtime.e");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEOE("methods/return.e");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceEOE("methods/specified.e");
		}

	}
}


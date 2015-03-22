using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceEPE("methods/anonymous.e");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEPE("methods/attribute.e");
		}

		[Test]
		public void testDefault()
		{
			compareResourceEPE("methods/default.e");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceEPE("methods/e_as_e_bug.e");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceEPE("methods/expressionWith.e");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceEPE("methods/implicit.e");
		}

		[Test]
		public void testMember()
		{
			compareResourceEPE("methods/member.e");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceEPE("methods/polymorphic_abstract.e");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceEPE("methods/polymorphic_implicit.e");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceEPE("methods/polymorphic_named.e");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceEPE("methods/polymorphic_runtime.e");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEPE("methods/return.e");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceEPE("methods/specified.e");
		}

	}
}


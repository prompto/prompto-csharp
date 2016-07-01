using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceOEO("methods/anonymous.poc");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceOEO("methods/attribute.poc");
		}

		[Test]
		public void testDefault()
		{
			compareResourceOEO("methods/default.poc");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceOEO("methods/e_as_e_bug.poc");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceOEO("methods/expressionWith.poc");
		}

		[Test]
		public void testExtended()
		{
			compareResourceOEO("methods/extended.poc");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceOEO("methods/implicit.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("methods/member.poc");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceOEO("methods/polymorphic_abstract.poc");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceOEO("methods/polymorphic_implicit.poc");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceOEO("methods/polymorphic_named.poc");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceOEO("methods/polymorphic_runtime.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOEO("methods/return.poc");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceOEO("methods/specified.poc");
		}

	}
}


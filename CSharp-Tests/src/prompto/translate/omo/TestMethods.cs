using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAbstractMember()
		{
			compareResourceOMO("methods/abstractMember.poc");
		}

		[Test]
		public void testAnonymous()
		{
			compareResourceOMO("methods/anonymous.poc");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceOMO("methods/attribute.poc");
		}

		[Test]
		public void testDefault()
		{
			compareResourceOMO("methods/default.poc");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceOMO("methods/e_as_e_bug.poc");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceOMO("methods/empty.poc");
		}

		[Test]
		public void testExplicit()
		{
			compareResourceOMO("methods/explicit.poc");
		}

		[Test]
		public void testExplicitMember()
		{
			compareResourceOMO("methods/explicitMember.poc");
		}

		[Test]
		public void testExpressionMember()
		{
			compareResourceOMO("methods/expressionMember.poc");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceOMO("methods/expressionWith.poc");
		}

		[Test]
		public void testExtended()
		{
			compareResourceOMO("methods/extended.poc");
		}

		[Test]
		public void testGlobal()
		{
			compareResourceOMO("methods/global.poc");
		}

		[Test]
		public void testImplicitMember()
		{
			compareResourceOMO("methods/implicitMember.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOMO("methods/member.poc");
		}

		[Test]
		public void testOverride()
		{
			compareResourceOMO("methods/override.poc");
		}

		[Test]
		public void testParameter()
		{
			compareResourceOMO("methods/parameter.poc");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceOMO("methods/polymorphic_abstract.poc");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceOMO("methods/polymorphic_implicit.poc");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceOMO("methods/polymorphic_named.poc");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceOMO("methods/polymorphic_runtime.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOMO("methods/return.poc");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceOMO("methods/specified.poc");
		}

		[Test]
		public void testTextAsync()
		{
			compareResourceOMO("methods/textAsync.poc");
		}

		[Test]
		public void testVoidAsync()
		{
			compareResourceOMO("methods/voidAsync.poc");
		}

	}
}


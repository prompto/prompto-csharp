using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAbstractMember()
		{
			compareResourceOEO("methods/abstractMember.poc");
		}

		[Test]
		public void testAbstractMemberItem()
		{
			compareResourceOEO("methods/abstractMemberItem.poc");
		}

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
		public void testEmpty()
		{
			compareResourceOEO("methods/empty.poc");
		}

		[Test]
		public void testExplicit()
		{
			compareResourceOEO("methods/explicit.poc");
		}

		[Test]
		public void testExplicitMember()
		{
			compareResourceOEO("methods/explicitMember.poc");
		}

		[Test]
		public void testExpressionMember()
		{
			compareResourceOEO("methods/expressionMember.poc");
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
		public void testGlobal()
		{
			compareResourceOEO("methods/global.poc");
		}

		[Test]
		public void testHomonym2()
		{
			compareResourceOEO("methods/homonym2.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("methods/member.poc");
		}

		[Test]
		public void testMemberRef()
		{
			compareResourceOEO("methods/memberRef.poc");
		}

		[Test]
		public void testOverride()
		{
			compareResourceOEO("methods/override.poc");
		}

		[Test]
		public void testParameter()
		{
			compareResourceOEO("methods/parameter.poc");
		}

		[Test]
		public void testPolymorphicAbstract()
		{
			compareResourceOEO("methods/polymorphicAbstract.poc");
		}

		[Test]
		public void testPolymorphicNamed()
		{
			compareResourceOEO("methods/polymorphicNamed.poc");
		}

		[Test]
		public void testPolymorphicRuntime()
		{
			compareResourceOEO("methods/polymorphicRuntime.poc");
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

		[Test]
		public void testTextAsync()
		{
			compareResourceOEO("methods/textAsync.poc");
		}

		[Test]
		public void testVoidAsync()
		{
			compareResourceOEO("methods/voidAsync.poc");
		}

	}
}


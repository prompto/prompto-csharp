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
		public void testAbstractMemberItem()
		{
			compareResourceOMO("methods/abstractMemberItem.poc");
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
		public void testHomonym2()
		{
			compareResourceOMO("methods/homonym2.poc");
		}

		[Test]
		public void testInheritedMember()
		{
			compareResourceOMO("methods/inheritedMember.poc");
		}

		[Test]
		public void testLocalMember()
		{
			compareResourceOMO("methods/localMember.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOMO("methods/member.poc");
		}

		[Test]
		public void testMemberRef()
		{
			compareResourceOMO("methods/memberRef.poc");
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
		public void testPolymorphicAbstract()
		{
			compareResourceOMO("methods/polymorphicAbstract.poc");
		}

		[Test]
		public void testPolymorphicMember()
		{
			compareResourceOMO("methods/polymorphicMember.poc");
		}

		[Test]
		public void testPolymorphicNamed()
		{
			compareResourceOMO("methods/polymorphicNamed.poc");
		}

		[Test]
		public void testPolymorphicRuntime()
		{
			compareResourceOMO("methods/polymorphicRuntime.poc");
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


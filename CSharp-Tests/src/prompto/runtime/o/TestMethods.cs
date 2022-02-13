using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
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
		public void testAbstractMember()
		{
			CheckOutput("methods/abstractMember.poc");
		}

		[Test]
		public void testAbstractMemberItem()
		{
			CheckOutput("methods/abstractMemberItem.poc");
		}

		[Test]
		public void testAnonymous()
		{
			CheckOutput("methods/anonymous.poc");
		}

		[Test]
		public void testAttribute()
		{
			CheckOutput("methods/attribute.poc");
		}

		[Test]
		public void testDefault()
		{
			CheckOutput("methods/default.poc");
		}

		[Test]
		public void testE_as_e_bug()
		{
			CheckOutput("methods/e_as_e_bug.poc");
		}

		[Test]
		public void testExplicit()
		{
			CheckOutput("methods/explicit.poc");
		}

		[Test]
		public void testExplicitMember()
		{
			CheckOutput("methods/explicitMember.poc");
		}

		[Test]
		public void testExpressionMember()
		{
			CheckOutput("methods/expressionMember.poc");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.poc");
		}

		[Test]
		public void testExtended()
		{
			CheckOutput("methods/extended.poc");
		}

		[Test]
		public void testHomonym2()
		{
			CheckOutput("methods/homonym2.poc");
		}

		[Test]
		public void testLocalMember()
		{
			CheckOutput("methods/localMember.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.poc");
		}

		[Test]
		public void testMemberRef()
		{
			CheckOutput("methods/memberRef.poc");
		}

		[Test]
		public void testOverride()
		{
			CheckOutput("methods/override.poc");
		}

		[Test]
		public void testParameter()
		{
			CheckOutput("methods/parameter.poc");
		}

		[Test]
		public void testPolymorphicAbstract()
		{
			CheckOutput("methods/polymorphicAbstract.poc");
		}

		[Test]
		public void testPolymorphicMember()
		{
			CheckOutput("methods/polymorphicMember.poc");
		}

		[Test]
		public void testPolymorphicNamed()
		{
			CheckOutput("methods/polymorphicNamed.poc");
		}

		[Test]
		public void testPolymorphicRuntime()
		{
			CheckOutput("methods/polymorphicRuntime.poc");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.poc");
		}

		[Test]
		public void testTextAsync()
		{
			CheckOutput("methods/textAsync.poc");
		}

		[Test]
		public void testVoidAsync()
		{
			CheckOutput("methods/voidAsync.poc");
		}

	}
}


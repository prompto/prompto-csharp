using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
		public void testAbstractMember()
		{
			CheckOutput("methods/abstractMember.pec");
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
		public void testExplicit()
		{
			CheckOutput("methods/explicit.pec");
		}

		[Test]
		public void testExplicitMember()
		{
			CheckOutput("methods/explicitMember.pec");
		}

		[Test]
		public void testExpressionMember()
		{
			CheckOutput("methods/expressionMember.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			CheckOutput("methods/expressionWith.pec");
		}

		[Test]
		public void testExtended()
		{
			CheckOutput("methods/extended.pec");
		}

		[Test]
		public void testHomonym()
		{
			CheckOutput("methods/homonym.pec");
		}

		[Test]
		public void testHomonym2()
		{
			CheckOutput("methods/homonym2.pec");
		}

		[Test]
		public void testImplicitAnd()
		{
			CheckOutput("methods/implicitAnd.pec");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			CheckOutput("methods/memberCall.pec");
		}

		[Test]
		public void testMemberRef()
		{
			CheckOutput("methods/memberRef.pec");
		}

		[Test]
		public void testOverride()
		{
			CheckOutput("methods/override.pec");
		}

		[Test]
		public void testParameter()
		{
			CheckOutput("methods/parameter.pec");
		}

		[Test]
		public void testPolymorphicAbstract()
		{
			CheckOutput("methods/polymorphicAbstract.pec");
		}

		[Test]
		public void testPolymorphicMember()
		{
			CheckOutput("methods/polymorphicMember.pec");
		}

		[Test]
		public void testPolymorphicNamed()
		{
			CheckOutput("methods/polymorphicNamed.pec");
		}

		[Test]
		public void testPolymorphicRuntime()
		{
			CheckOutput("methods/polymorphicRuntime.pec");
		}

		[Test]
		public void testSpecified()
		{
			CheckOutput("methods/specified.pec");
		}

		[Test]
		public void testTextAsync()
		{
			CheckOutput("methods/textAsync.pec");
		}

		[Test]
		public void testVoidAsync()
		{
			CheckOutput("methods/voidAsync.pec");
		}

	}
}


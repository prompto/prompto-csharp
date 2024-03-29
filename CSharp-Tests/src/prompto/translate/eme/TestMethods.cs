using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestMethods : BaseEParserTest
	{

		[Test]
		public void testAbstractMember()
		{
			compareResourceEME("methods/abstractMember.pec");
		}

		[Test]
		public void testAnonymous()
		{
			compareResourceEME("methods/anonymous.pec");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceEME("methods/attribute.pec");
		}

		[Test]
		public void testDefault()
		{
			compareResourceEME("methods/default.pec");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceEME("methods/e_as_e_bug.pec");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceEME("methods/empty.pec");
		}

		[Test]
		public void testExplicit()
		{
			compareResourceEME("methods/explicit.pec");
		}

		[Test]
		public void testExplicitMember()
		{
			compareResourceEME("methods/explicitMember.pec");
		}

		[Test]
		public void testExpressionMember()
		{
			compareResourceEME("methods/expressionMember.pec");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceEME("methods/expressionWith.pec");
		}

		[Test]
		public void testExtended()
		{
			compareResourceEME("methods/extended.pec");
		}

		[Test]
		public void testGlobal()
		{
			compareResourceEME("methods/global.pec");
		}

		[Test]
		public void testHomonym()
		{
			compareResourceEME("methods/homonym.pec");
		}

		[Test]
		public void testHomonym2()
		{
			compareResourceEME("methods/homonym2.pec");
		}

		[Test]
		public void testImplicitAnd()
		{
			compareResourceEME("methods/implicitAnd.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEME("methods/member.pec");
		}

		[Test]
		public void testMemberCall()
		{
			compareResourceEME("methods/memberCall.pec");
		}

		[Test]
		public void testMemberRef()
		{
			compareResourceEME("methods/memberRef.pec");
		}

		[Test]
		public void testOverride()
		{
			compareResourceEME("methods/override.pec");
		}

		[Test]
		public void testParameter()
		{
			compareResourceEME("methods/parameter.pec");
		}

		[Test]
		public void testPolymorphicAbstract()
		{
			compareResourceEME("methods/polymorphicAbstract.pec");
		}

		[Test]
		public void testPolymorphicMember()
		{
			compareResourceEME("methods/polymorphicMember.pec");
		}

		[Test]
		public void testPolymorphicNamed()
		{
			compareResourceEME("methods/polymorphicNamed.pec");
		}

		[Test]
		public void testPolymorphicRuntime()
		{
			compareResourceEME("methods/polymorphicRuntime.pec");
		}

		[Test]
		public void testReturn()
		{
			compareResourceEME("methods/return.pec");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceEME("methods/specified.pec");
		}

		[Test]
		public void testTextAsync()
		{
			compareResourceEME("methods/textAsync.pec");
		}

		[Test]
		public void testVoidAsync()
		{
			compareResourceEME("methods/voidAsync.pec");
		}

	}
}


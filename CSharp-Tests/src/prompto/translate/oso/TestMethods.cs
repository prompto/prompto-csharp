// generated: 2015-07-05T23:01:01.329
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestMethods : BaseOParserTest
	{

		[Test]
		public void testAnonymous()
		{
			compareResourceOSO("methods/anonymous.poc");
		}

		[Test]
		public void testAttribute()
		{
			compareResourceOSO("methods/attribute.poc");
		}

		[Test]
		public void testDefault()
		{
			compareResourceOSO("methods/default.poc");
		}

		[Test]
		public void testE_as_e_bug()
		{
			compareResourceOSO("methods/e_as_e_bug.poc");
		}

		[Test]
		public void testExpressionWith()
		{
			compareResourceOSO("methods/expressionWith.poc");
		}

		[Test]
		public void testImplicit()
		{
			compareResourceOSO("methods/implicit.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOSO("methods/member.poc");
		}

		[Test]
		public void testPolymorphic_abstract()
		{
			compareResourceOSO("methods/polymorphic_abstract.poc");
		}

		[Test]
		public void testPolymorphic_implicit()
		{
			compareResourceOSO("methods/polymorphic_implicit.poc");
		}

		[Test]
		public void testPolymorphic_named()
		{
			compareResourceOSO("methods/polymorphic_named.poc");
		}

		[Test]
		public void testPolymorphic_runtime()
		{
			compareResourceOSO("methods/polymorphic_runtime.poc");
		}

		[Test]
		public void testReturn()
		{
			compareResourceOSO("methods/return.poc");
		}

		[Test]
		public void testSpecified()
		{
			compareResourceOSO("methods/specified.poc");
		}

	}
}


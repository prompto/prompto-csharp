using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestCast : BaseOParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceOMO("cast/autoDowncast.poc");
		}

		[Test]
		public void testAutoDowncastMethod()
		{
			compareResourceOMO("cast/autoDowncastMethod.poc");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOMO("cast/castChild.poc");
		}

		[Test]
		public void testCastEnum()
		{
			compareResourceOMO("cast/castEnum.poc");
		}

		[Test]
		public void testCastMethod()
		{
			compareResourceOMO("cast/castMethod.poc");
		}

		[Test]
		public void testCastMissing()
		{
			compareResourceOMO("cast/castMissing.poc");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceOMO("cast/castNull.poc");
		}

		[Test]
		public void testCastParent()
		{
			compareResourceOMO("cast/castParent.poc");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceOMO("cast/isAChild.poc");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceOMO("cast/isAText.poc");
		}

		[Test]
		public void testMutableEntity()
		{
			compareResourceOMO("cast/mutableEntity.poc");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceOMO("cast/mutableList.poc");
		}

		[Test]
		public void testNullIsNotAText()
		{
			compareResourceOMO("cast/nullIsNotAText.poc");
		}

	}
}


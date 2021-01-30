using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestProblems : BaseOParserTest
	{

		[Test]
		public void testAbstract()
		{
			compareResourceOEO("problems/abstract.poc");
		}

	}
}


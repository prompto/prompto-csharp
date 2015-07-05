// generated: 2015-07-05T23:01:01.392
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceEOE("setters/getter.pec");
		}

		[Test]
		public void testGetterCall()
		{
			compareResourceEOE("setters/getterCall.pec");
		}

		[Test]
		public void testSetter()
		{
			compareResourceEOE("setters/setter.pec");
		}

	}
}


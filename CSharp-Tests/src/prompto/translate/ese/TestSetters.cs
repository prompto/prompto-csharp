// generated: 2015-07-05T23:01:01.393
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceESE("setters/getter.pec");
		}

		[Test]
		public void testGetterCall()
		{
			compareResourceESE("setters/getterCall.pec");
		}

		[Test]
		public void testSetter()
		{
			compareResourceESE("setters/setter.pec");
		}

	}
}


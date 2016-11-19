using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceEME("setters/getter.pec");
		}

		[Test]
		public void testGetterCall()
		{
			compareResourceEME("setters/getterCall.pec");
		}

		[Test]
		public void testSetter()
		{
			compareResourceEME("setters/setter.pec");
		}

	}
}


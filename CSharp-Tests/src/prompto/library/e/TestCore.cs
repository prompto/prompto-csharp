using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

	[TestFixture]
	public class TestCore : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Loader.Load ();
			Out.init();
			coreContext = null;
			LoadDependency("core");
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testAny()
		{
			CheckTests("core/any.pec");
		}

		[Test]
		public void testAttribute()
		{
			CheckTests("core/attribute.pec");
		}

		[Test]
		public void testAttributes()
		{
			CheckTests("core/attributes.pec");
		}

		[Test]
		public void testCloud()
		{
			CheckTests("core/cloud.pec");
		}

		[Test]
		public void testError()
		{
			CheckTests("core/error.pec");
		}

		[Test]
		public void testMath()
		{
			CheckTests("core/math.pec");
		}

		[Test]
		public void testParse()
		{
			CheckTests("core/parse.pec");
		}

		[Test]
		public void testTime()
		{
			CheckTests("core/time.pec");
		}

		[Test]
		public void testUtils()
		{
			CheckTests("core/utils.pec");
		}

	}
}


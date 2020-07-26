using NUnit.Framework;
using prompto.parser;
using prompto.utils;
using prompto.runtime;

namespace prompto.library.e
{

    [TestFixture]
    public class TestScheduler : BaseEParserTest
    {
        Context savedContext;

        [SetUp]
        public void before()
        {
            Loader.Load();
            Out.init();
            LoadDependency("core");
            savedContext = ApplicationContext.Set(coreContext);
        }

        [TearDown]
        public void after()
        {
            Out.restore();
            savedContext = ApplicationContext.Set(savedContext);
            savedContext = null;
        }

        [Test]
        public void testScheduler()
        {
            CheckTests("prompto/scheduler.pec");
        }



    }
}

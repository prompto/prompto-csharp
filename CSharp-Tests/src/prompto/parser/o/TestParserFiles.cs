using NUnit.Framework;
using prompto.grammar;
namespace prompto.parser {

    [TestFixture]
    public class TestOParserFiles : BaseOParserTest
    {

        [Test]
        public void testEmpty()
        {
            DeclarationList stmts = parseString("");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(0, stmts.Count);
        }

    }

}

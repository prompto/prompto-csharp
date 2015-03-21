using NUnit.Framework;
using presto.grammar;
namespace presto.parser
{

    [TestFixture]
    public class TestEParserFiles : BaseEParserTest
    {

        [Test]
        public void testEmpty()
        {
            DeclarationList stmts = parseString("");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(0, stmts.Count);
        }

        [Test]
        public void testNative()
        {
            DeclarationList stmts = parseResource("native/method.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(2, stmts.Count);
        }

        [Test]
        public void testSpecified()
        {
            DeclarationList stmts = parseResource("methods/specified.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(6, stmts.Count);
        }

        [Test]
        public void testAttribute()
        {
            DeclarationList stmts = parseResource("methods/attribute.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(6, stmts.Count);
        }

        [Test]
        public void testImplicit()
        {
            DeclarationList stmts = parseResource("methods/implicit.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(6, stmts.Count);
        }

        [Test]
        public void testPolymorphicImplicit()
        {
            DeclarationList stmts = parseResource("methods/polymorphic_implicit.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(12, stmts.Count);
        }

        // [Test]
        public void testEnumeratedCategory()
        {
            DeclarationList stmts = parseResource("enums/categoryEnum.e");
            Assert.IsNotNull(stmts);
            Assert.AreEqual(3, stmts.Count);
        }

    }

}
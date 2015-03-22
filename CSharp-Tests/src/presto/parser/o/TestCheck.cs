using presto.parser;
using NUnit.Framework;
using presto.grammar;
using presto.error;
namespace presto.o.runtime
{

    [TestFixture]
    public class TestCheck : BaseOParserTest
    {

        [Test]
        public void testNativeAttribute()
        {
            DeclarationList stmts = parseString("attribute id: Integer;");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testUndeclaredCategoryAttribute()
        {
            DeclarationList stmts = parseString("attribute person : Person;");
            stmts.register(context);
            try
            {
                stmts.check(context);
                Assert.Fail("Should fail since Person is not declared !");
            }
            catch (SyntaxError)
            {

            }
        }

        [Test]
        public void testMethodAttribute()
        {
            DeclarationList stmts = parseString("attribute name: Text;" +
                    "method PrintName(name) { " +
                    "print ( value = \"name\" + name ); }" +
                    "category Person extends PrintName;");
            stmts.register(context);
            try
            {
                stmts.check(context);
                Assert.Fail("Should fail since printName is not a category !");
            }
            catch (SyntaxError)
            {

            }
        }

        [Test]
        public void testCategoryAttribute()
        {
            DeclarationList stmts = parseString("attribute id: Integer;" +
                    "category Person(id);" +
                    "attribute person: Person;");
            stmts.register(context);
            stmts.check(context);
        }


        [Test]
        public void testCategoryWithUndeclaredDerived()
        {
            DeclarationList stmts = parseString("category Employee extends Person;");
            try
            {
                stmts.register(context);
                stmts.check(context);
                Assert.Fail("Should fail since Person not declared !");
            }
            catch (SyntaxError)
            {

            }
        }

        [Test]
        public void testCategoryWithUndeclaredAttribute()
        {
            DeclarationList stmts = parseString("category Person(id);");
            try
            {
                stmts.register(context);
                stmts.check(context);
                Assert.Fail("Should fail since id not declared !");
            }
            catch (SyntaxError)
            {

            }
        }

        [Test]
        public void testCategory()
        {
            DeclarationList stmts = parseString("attribute id: Integer;" +
                    "category Person(id);" +
                    "category Employee extends Person;");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testMethodWithUndeclaredAttribute()
        {
            DeclarationList stmts = parseString("method printName(name) {" +
                    "print (value = \"name\" + name ); }");
            try
            {
                stmts.register(context);
                stmts.check(context);
                Assert.Fail("Should fail since name not declared !");
            }
            catch (SyntaxError)
            {

            }
        }

        [Test]
        public void testMethod()
        {
            DeclarationList stmts = parseString("native method print( Text value) {" +
                        "C#: System.Console.WriteLine(value); }" +
                        "attribute name: Text;" +
                        "method printName(name ) {" +
                        "print( value = \"name\" + name ); }");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testList()
        {
            DeclarationList stmts = parseString("method test (Text value) {" +
                        "list = [ \"john\" , \"jim\" ];" +
                        "elem = list[1]; }");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testDict()
        {
            DeclarationList stmts = parseString("method test (Text value) {" +
                        "dict = { \"john\":123, \"jim\":345 };" +
                        "elem = dict[\"john\"]; }");
            stmts.register(context);
            stmts.check(context);
        }
    }

}
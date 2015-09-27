using prompto.parser;
using NUnit.Framework;
using prompto.grammar;
using prompto.error;
namespace prompto.e.runtime
{

    [TestFixture]
    public class TestCheck : BaseEParserTest
    {

        [Test]
        public void testNativeAttribute()
        {
            DeclarationList stmts = parseString("define id as Integer attribute");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testUndeclaredCategoryAttribute()
        {
            DeclarationList stmts = parseString("define person as Person attribute");
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
            DeclarationList stmts = parseString("define name as Text attribute\r\n" +
                    "define printName as method receiving name doing:\r\n" +
                    "\tprint with \"name\" + name as value\r\n" +
                    "define Person as category with attribute printName");
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
            DeclarationList stmts = parseString("define id as Integer attribute\r\n" +
                    "define Person as category with attribute id\r\n" +
                    "define person as Person attribute");
            stmts.register(context);
            stmts.check(context);
        }


        [Test]
        public void testCategoryWithUndeclaredDerived()
        {
            DeclarationList stmts = parseString("define Employee as Person");
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
            DeclarationList stmts = parseString("define Person as category with attribute id");
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
            DeclarationList stmts = parseString("define id as Integer attribute\r\n" +
                    "define Person as category with attribute id\r\n" +
                    "define Employee as Person");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testMethodWithUndeclaredAttribute()
        {
            DeclarationList stmts = parseString("define printName as method receiving name doing:\r\n" +
                    "\tprint with \"name\" + name as value");
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
            DeclarationList stmts = parseString("define print as native method receiving Text value doing:\r\n" +
                        "\tC#: System.Console.WriteLine(value);\r\n" +
                        "define name as Text attribute\r\n" +
                        "define printName as method receiving name doing:\r\n" +
                        "\tprint with \"name\" + name as value");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testList()
        {
            DeclarationList stmts = parseString("define testMethod as method receiving Text value doing:\r\n" +
                        "\tlist = [ \"john\" , \"jim\" ]\r\n" +
                        "\telem = list[1]\r\n");
            stmts.register(context);
            stmts.check(context);
        }

        [Test]
        public void testDict()
        {
			DeclarationList stmts = parseString("define testMethod as method receiving Text value doing:\r\n" +
                        "\tdict = { \"john\":123, \"jim\":345 }\r\n" +
                        "\telem = dict[\"john\"]\r\n");
            stmts.register(context);
            stmts.check(context);
        }
    }

}
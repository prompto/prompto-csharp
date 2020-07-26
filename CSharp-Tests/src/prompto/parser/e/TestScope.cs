using prompto.parser;
using NUnit.Framework;
using prompto.error;
using prompto.runtime;
using prompto.declaration;

namespace prompto.parser.e
{

    [TestFixture]
    public class TestScope : BaseEParserTest
    {

        [Test]
        public void testAttribute()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				Context context = Context.newGlobalsContext();
				Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("id"));
				DeclarationList stmts = parseString("define id as Integer attribute");
				Assert.IsNotNull(stmts);
				stmts.register(context);
				IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("id");
				Assert.IsNotNull(actual);
				Assert.IsTrue(actual is AttributeDeclaration);
				stmts = parseString("define id as Integer attribute");
				stmts.register(context);
			});
        }

        [Test]
        public void testCategory()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				Context context = Context.newGlobalsContext();
				Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("Person"));
				DeclarationList stmts = parseString("define Person as category with attributes id and name");
				Assert.IsNotNull(stmts);
				stmts.register(context);
				IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("Person");
				Assert.IsNotNull(actual);
				Assert.IsTrue(actual is CategoryDeclaration);
				stmts = parseString("define Person as category with attributes id and name");
				stmts.register(context);
			});
        }

        [Test]
        public void testMethod()
        {
			context = Context.newGlobalsContext();
            Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("printName"));
            DeclarationList stmts = parseString("define name as Text attribute\r\n"
                    + "define printName as method receiving name doing:\r\n"
                    + "\tprint with \"name\" + name as value");
            Assert.IsNotNull(stmts);
            stmts.register(context);
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("printName");
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual is MethodDeclarationMap);
            stmts = parseString("define printName as method receiving Person p doing:"
                    + "\r\n\tprint with \"person\" + p.name as value");
            Assert.IsNotNull(stmts);
            stmts.register(context);
        }
    }

}
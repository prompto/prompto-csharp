using presto.parser;
using NUnit.Framework;
using presto.error;
using presto.grammar;
using presto.runtime;
using presto.declaration;
namespace presto.e.runtime
{

    [TestFixture]
    public class TestScope : BaseEParserTest
    {

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testAttribute()
        {
			context = Context.newGlobalContext();
            Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("id"));
            DeclarationList stmts = parseString("define id as: Integer attribute");
            Assert.IsNotNull(stmts);
            stmts.register(context);
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("id");
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual is AttributeDeclaration);
            stmts.register(context);
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testCategory()
        {
			context = Context.newGlobalContext();
			Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("Person"));
            DeclarationList stmts = parseString("define Person as: category with attributes: id and name");
            Assert.IsNotNull(stmts);
            stmts.register(context);
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("Person");
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual is CategoryDeclaration);
            stmts.register(context);
        }

        [Test]
        public void testMethod()
        {
			context = Context.newGlobalContext();
            Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("printName"));
            DeclarationList stmts = parseString("define name as: Text attribute\r\n"
                    + "define printName as: method receiving: name doing:\r\n"
                    + "\tprint with \"name\" + name as value");
            Assert.IsNotNull(stmts);
            stmts.register(context);
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("printName");
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual is MethodDeclarationMap);
            stmts = parseString("define printName as: method receiving: Person p doing:"
                    + "\r\n\tprint with \"person\" + p.name as value");
            Assert.IsNotNull(stmts);
            stmts.register(context);
        }
    }

}
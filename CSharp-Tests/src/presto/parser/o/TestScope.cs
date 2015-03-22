using presto.parser;
using NUnit.Framework;
using presto.grammar;
using presto.error;
using presto.runtime;
using presto.declaration;
namespace presto.o.runtime
{

    [TestFixture]
    public class TestScope : BaseOParserTest
    {

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testAttribute()
        {
			context = Context.newGlobalContext();
            Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("id"));
            DeclarationList stmts = parseString("attribute id: Integer; ");
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
            DeclarationList stmts = parseString("category Person(id, name);");
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
            DeclarationList stmts = parseString("attribute name: Text;"
                    + "method printName( name ) {"
                    + "print (value=name); }");
            Assert.IsNotNull(stmts);
            stmts.register(context);
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("printName");
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual is MethodDeclarationMap);
            stmts = parseString("method printName (Person p ) {"
                    + "print (value = \"person\" + p.name ); } ");
            Assert.IsNotNull(stmts);
            stmts.register(context);
        }
    }

}
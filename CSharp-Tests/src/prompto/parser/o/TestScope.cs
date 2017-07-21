using NUnit.Framework;
using prompto.error;
using prompto.runtime;
using prompto.declaration;

namespace prompto.parser.o
{

    [TestFixture]
    public class TestScope : BaseOParserTest
    {

        [Test]
        public void testAttribute()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				context = Context.newGlobalContext();
				Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("id"));
				DeclarationList stmts = parseString("attribute id: Integer; ");
				Assert.IsNotNull(stmts);
				stmts.register(context);
				IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("id");
				Assert.IsNotNull(actual);
				Assert.IsTrue(actual is AttributeDeclaration);
				stmts = parseString("attribute id: Integer; ");
				stmts.register(context);
			});
        }

        [Test]
        public void testCategory()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				context = Context.newGlobalContext();
				Assert.IsNull(context.getRegisteredDeclaration<IDeclaration>("Person"));
				DeclarationList stmts = parseString("category Person(id, name);");
				Assert.IsNotNull(stmts);
				stmts.register(context);
				IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>("Person");
				Assert.IsNotNull(actual);
				Assert.IsTrue(actual is CategoryDeclaration);
				stmts = parseString("category Person(id, name);");
				stmts.register(context);
			});
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
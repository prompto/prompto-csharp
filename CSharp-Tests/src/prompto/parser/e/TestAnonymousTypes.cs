using prompto.parser;
using NUnit.Framework;
using prompto.grammar;
using prompto.declaration;
using prompto.runtime;
using prompto.type;
using prompto.argument;


namespace prompto.parser.e
{

    [TestFixture]
    public class TestAnonymousTypes : BaseEParserTest
    {

        [SetUp]
        public void register()
        {
			context = Context.newGlobalContext();
            DeclarationList stmts = parseString("define id as Integer attribute\r\n" +
                    "define name as String attribute\r\n" +
                    "define other as String attribute\r\n" +
                    "define Simple as category with attribute name\r\n" +
                    "define Root as category with attribute id\r\n" +
                    "define DerivedWithOther as Root with attribute other\r\n" +
                    "define DerivedWithName as Root with attribute name\r\n");
            stmts.register(context);
        }

        [Test]
        public void testAnonymousAnyType()
        {
            // any x
            IArgument argument = new CategoryArgument(AnyType.Instance, "x");
            argument.register(context);
            IType st = argument.GetIType(context);
            Assert.IsTrue(st is AnyType);
            Assert.IsTrue(st.isAssignableFrom(context, BooleanType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, IntegerType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DecimalType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, TextType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DateType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DateTimeType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, MissingType.Instance)); 
            Assert.IsTrue(st.isAssignableFrom(context, AnyType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Simple")));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Root")));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithOther")));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithName")));
        }

        [Test]
        public void testAnonymousAnyTypeWithAttribute()
        {
            // any x with attribute name
            IdentifierList list = new IdentifierList("name");
			IArgument argument = new ExtendedArgument(AnyType.Instance, "x", list);
            argument.register(context);
            IType st = argument.GetIType(context);
            Assert.IsTrue(st is CategoryType);
            Assert.IsFalse(st.isAssignableFrom(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, MissingType.Instance)); 
            Assert.IsFalse(st.isAssignableFrom(context, AnyType.Instance)); // any type never compatible
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Simple"))); // since Simple has a name
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("Root"))); // since Root has no name
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("DerivedWithOther"))); // since DerivedWithOther has no name
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithName"))); // since DerivedWithName has a name
        }

        [Test]
        public void testAnonymousCategoryType()
        {
            // Root x
            IArgument argument = new CategoryArgument(new CategoryType("Root"), "x");
            argument.register(context);
            IType st = argument.GetIType(context);
            Assert.IsTrue(st is CategoryType);
            Assert.IsFalse(st.isAssignableFrom(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, MissingType.Instance)); 
            Assert.IsFalse(st.isAssignableFrom(context, AnyType.Instance)); // any type never compatible
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("Simple")));  // since Simple does not extend Root
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Root"))); // since Root is Root
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithOther"))); // since DerivedWithOther : Root
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithName"))); // since DerivedWithName : Root
        }

        [Test]
        public void testAnonymousCategoryTypeWithAttribute()
        {
            // Root x with attribute name
            IdentifierList list = new IdentifierList("name");
			IArgument argument = new ExtendedArgument(new CategoryType("Root"), "test", list);
            argument.register(context);
            IType st = argument.GetIType(context);
            Assert.IsTrue(st is CategoryType);
            Assert.IsFalse(st.isAssignableFrom(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableFrom(context, MissingType.Instance)); 
            Assert.IsFalse(st.isAssignableFrom(context, AnyType.Instance)); // any type never compatible
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("Simple")));  // since Simple does not extend Root
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("Root"))); // since Root has no name
            Assert.IsFalse(st.isAssignableFrom(context, new CategoryType("DerivedWithOther"))); // since DerivedWithOther has no name
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("DerivedWithName"))); // since DerivedWithName has a name
        }

    }

}
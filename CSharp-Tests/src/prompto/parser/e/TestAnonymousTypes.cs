using prompto.parser;
using NUnit.Framework;
using prompto.grammar;
using prompto.declaration;
using prompto.runtime;
using prompto.type;
using prompto.argument;


namespace prompto.e.runtime
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
            Assert.IsTrue(BooleanType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(IntegerType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(DecimalType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(TextType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(DateType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(DateTimeType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableTo(context, st)); // missing type always compatible
            Assert.IsTrue(AnyType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(new CategoryType("Simple").isAssignableTo(context, st));
            Assert.IsTrue(new CategoryType("Root").isAssignableTo(context, st));
            Assert.IsTrue(new CategoryType("DerivedWithOther").isAssignableTo(context, st));
            Assert.IsTrue(new CategoryType("DerivedWithName").isAssignableTo(context, st));
			Assert.IsTrue(false);
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
            Assert.IsFalse(BooleanType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableTo(context, st)); // missing type always compatible
            Assert.IsFalse(AnyType.Instance.isAssignableTo(context, st)); // any type never compatible
            Assert.IsTrue(new CategoryType("Simple").isAssignableTo(context, st)); // since Simple has a name
            Assert.IsFalse(new CategoryType("Root").isAssignableTo(context, st)); // since Root has no name
            Assert.IsFalse(new CategoryType("DerivedWithOther").isAssignableTo(context, st)); // since DerivedWithOther has no name
            Assert.IsTrue(new CategoryType("DerivedWithName").isAssignableTo(context, st)); // since DerivedWithName has a name
        }

        [Test]
        public void testAnonymousCategoryType()
        {
            // Root x
            IArgument argument = new CategoryArgument(new CategoryType("Root"), "x");
            argument.register(context);
            IType st = argument.GetIType(context);
            Assert.IsTrue(st is CategoryType);
            Assert.IsFalse(BooleanType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableTo(context, st)); // missing type always compatible
            Assert.IsFalse(AnyType.Instance.isAssignableTo(context, st)); // any type never compatible
            Assert.IsFalse(new CategoryType("Simple").isAssignableTo(context, st));  // since Simple does not extend Root
            Assert.IsTrue(new CategoryType("Root").isAssignableTo(context, st)); // since Root is Root
            Assert.IsTrue(new CategoryType("DerivedWithOther").isAssignableTo(context, st)); // since DerivedWithOther : Root
            Assert.IsTrue(new CategoryType("DerivedWithName").isAssignableTo(context, st)); // since DerivedWithName : Root
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
            Assert.IsFalse(BooleanType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableTo(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableTo(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableTo(context, st)); // missing type always compatible
            Assert.IsFalse(AnyType.Instance.isAssignableTo(context, st)); // any type never compatible
            Assert.IsFalse(new CategoryType("Simple").isAssignableTo(context, st));  // since Simple does not extend Root
            Assert.IsFalse(new CategoryType("Root").isAssignableTo(context, st)); // since Root has no name
            Assert.IsFalse(new CategoryType("DerivedWithOther").isAssignableTo(context, st)); // since DerivedWithOther has no name
            Assert.IsTrue(new CategoryType("DerivedWithName").isAssignableTo(context, st)); // since DerivedWithName has a name
        }

    }

}
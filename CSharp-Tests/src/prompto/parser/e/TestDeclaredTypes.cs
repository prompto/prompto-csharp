using prompto.parser;
using NUnit.Framework;
using prompto.declaration;
using prompto.type;

namespace prompto.parser.e
{

    [TestFixture]
    public class TestDeclaredTypes : BaseEParserTest
    {

        [SetUp]
        public void registerCategoryTypes()
        {
            DeclarationList stmts = parseString("define id as Integer attribute\r\n" +
                    "define name as String attribute\r\n" +
                    "define Root as category with attribute id\r\n" +
                    "define Derived as Root with attribute name\r\n" +
                    "define Unrelated as category with attributes id and name\r\n");
            stmts.register(context);
        }

        [Test]
        public void testBooleanType()
        {
            IType st = BooleanType.Instance;
            Assert.AreEqual(st, BooleanType.Instance);
            Assert.IsTrue(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testIntegerType()
        {
            IType st = IntegerType.Instance;
            Assert.AreEqual(st, IntegerType.Instance);
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testDecimalType()
        {
            IType st = DecimalType.Instance;
            Assert.AreEqual(st, DecimalType.Instance);
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testStringType()
        {
            IType st = TextType.Instance;
            Assert.AreEqual(st, TextType.Instance);
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testDateType()
        {
            IType st = DateType.Instance;
            Assert.AreEqual(st, DateType.Instance);
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testInstantType()
        {
            IType st = DateTimeType.Instance;
            Assert.AreEqual(st, DateTimeType.Instance);
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(DateTimeType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testMissingType()
        {
            IType st = MissingType.Instance;
            Assert.AreEqual(st, MissingType.Instance);
            Assert.IsTrue(st.isAssignableFrom(context, BooleanType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, IntegerType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DecimalType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, TextType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DateType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, DateTimeType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, AnyType.Instance));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Root")));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Derived")));
            Assert.IsTrue(st.isAssignableFrom(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testRootCategoryType()
        {
            IType st = new CategoryType("Root");
            Assert.AreEqual(st, new CategoryType("Root"));
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
			Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testDerivedCategoryType()
        {
            IType st = new CategoryType("Derived");
            Assert.AreEqual(st, new CategoryType("Derived"));
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
			Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsTrue(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

        [Test]
        public void testUnrelatedCategoryType()
        {
            IType st = new CategoryType("Unrelated");
            Assert.AreEqual(st, new CategoryType("Unrelated"));
            Assert.IsFalse(BooleanType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(IntegerType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DecimalType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(TextType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(DateTimeType.Instance.isAssignableFrom(context, st));
			Assert.IsTrue(MissingType.Instance.isAssignableFrom(context, st));
            Assert.IsTrue(AnyType.Instance.isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Root").isAssignableFrom(context, st));
            Assert.IsFalse(new CategoryType("Derived").isAssignableFrom(context, st));
            Assert.IsTrue(new CategoryType("Unrelated").isAssignableFrom(context, st));
        }

    }
}

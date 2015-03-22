using presto.parser;
using NUnit.Framework;
using presto.grammar;
using presto.type;
namespace presto.e.runtime
{

    [TestFixture]
    public class TestDeclaredTypes : BaseEParserTest
    {

        [SetUp]
        public void registerCategoryTypes()
        {
            DeclarationList stmts = parseString("define id as: Integer attribute\r\n" +
                    "define name as: String attribute\r\n" +
                    "define Root as: category with attribute: id\r\n" +
                    "define Derived as: Root with attribute: name\r\n" +
                    "define Unrelated as: category with attributes: id and name\r\n");
            stmts.register(context);
        }

        [Test]
        public void testBooleanType()
        {
            IType st = BooleanType.Instance;
            Assert.AreEqual(st, BooleanType.Instance);
            Assert.IsTrue(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testIntegerType()
        {
            IType st = IntegerType.Instance;
            Assert.AreEqual(st, IntegerType.Instance);
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testDecimalType()
        {
            IType st = DecimalType.Instance;
            Assert.AreEqual(st, DecimalType.Instance);
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testStringType()
        {
            IType st = TextType.Instance;
            Assert.AreEqual(st, TextType.Instance);
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testDateType()
        {
            IType st = DateType.Instance;
            Assert.AreEqual(st, DateType.Instance);
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testInstantType()
        {
            IType st = DateTimeType.Instance;
            Assert.AreEqual(st, DateTimeType.Instance);
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DateType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testMissingType()
        {
            IType st = MissingType.Instance;
            Assert.AreEqual(st, MissingType.Instance);
            Assert.IsTrue(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, TextType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DateType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testRootCategoryType()
        {
            IType st = new CategoryType("Root");
            Assert.AreEqual(st, new CategoryType("Root"));
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testDerivedCategoryType()
        {
            IType st = new CategoryType("Derived");
            Assert.AreEqual(st, new CategoryType("Derived"));
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

        [Test]
        public void testUnrelatedCategoryType()
        {
            IType st = new CategoryType("Unrelated");
            Assert.AreEqual(st, new CategoryType("Unrelated"));
            Assert.IsFalse(st.isAssignableTo(context, BooleanType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, IntegerType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DecimalType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, TextType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, DateTimeType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, MissingType.Instance));
            Assert.IsTrue(st.isAssignableTo(context, AnyType.Instance));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Root")));
            Assert.IsFalse(st.isAssignableTo(context, new CategoryType("Derived")));
            Assert.IsTrue(st.isAssignableTo(context, new CategoryType("Unrelated")));
        }

    }
}

using NUnit.Framework;
using prompto.grammar;
using System;
using prompto.value;
using DateTime = prompto.value.DateTime;
using prompto.parser;
using prompto.literal;
using prompto.expression;
using prompto.statement;
using prompto.declaration;
using prompto.type;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using prompto.utils;
using prompto.runtime;

namespace prompto.parser
{

    [TestFixture]
    public class TestEParserAtoms
    {

        [Test]
        public void testTuple()
        {
            String statement = "(1,\"John\",'12:12:12')";
            ETestParser parser = new ETestParser(statement, false);
			TupleLiteral literal = parser.parse_tuple_literal();
			Assert.IsNotNull(literal);
			ExpressionList list = ((TupleLiteral)literal).Expressions;
            Assert.AreEqual("1", list[0].ToString());
			Assert.AreEqual("\"John\"", list[1].ToString());
			Assert.AreEqual("'12:12:12'", list[2].ToString());
			Assert.AreEqual("1, \"John\", '12:12:12'", list.ToString());
        }

        [Test]
        public void testRange()
        {
            String statement = "[1..100]";
            ETestParser parser = new ETestParser(statement, false);
            RangeLiteral rl = parser.parse_range_literal();
            Assert.IsNotNull(rl);
            Assert.AreEqual("1", rl.getFirst().ToString());
            Assert.AreEqual("100", rl.getLast().ToString());
			Assert.AreEqual("[1..100]", generate(rl));
        }

        [Test]
        public void testAttribute()
        {
            String statement = "define id as Integer attribute";
            ETestParser parser = new ETestParser(statement, false);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("id", ad.GetName());
            Assert.AreEqual("Integer", ad.getType().GetName());
        }

        [Test]
        public void testArrayAttribute()
        {
            String statement = "define id as Integer[] attribute";
            ETestParser parser = new ETestParser(statement, false);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
			Assert.AreEqual("id", ad.GetName());
			Assert.AreEqual("Integer[]", ad.getType().GetName());
        }

        [Test]
        public void testCategory1Attribute()
        {
            String statement = "define Person as category with attribute id ";
            ETestParser parser = new ETestParser(statement, false);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
			Assert.AreEqual("Person", cd.GetName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("id"));
        }

        [Test]
        public void testCategory2Attributes()
        {
            String statement = "define Person as category with attributes id, name";
            ETestParser parser = new ETestParser(statement, false);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
			Assert.AreEqual("Person", cd.GetName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("id"));
            Assert.IsTrue(cd.getAttributes().Contains("name"));
        }

        [Test]
        public void testCategory1Derived1Attribute()
        {
            String statement = "define Employee as Person with attribute company";
            ETestParser parser = new ETestParser(statement, false);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
			Assert.AreEqual("Employee", cd.GetName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("company"));
        }

        [Test]
        public void testCategory2DerivedNoAttribute()
        {
            String statement = "define Entrepreneur as Person and Company";
            ETestParser parser = new ETestParser(statement, false);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
			Assert.AreEqual("Entrepreneur", cd.GetName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsTrue(cd.getDerivedFrom().Contains("Company"));
            Assert.IsNull(cd.getAttributes());
        }

        [Test]
        public void testMemberExpression()
        {
            String statement = "p.name";
            ETestParser parser = new ETestParser(statement, false);
            IExpression e = parser.parse_instance_expression();
            Assert.IsTrue(e is MemberSelector);
            MemberSelector me = (MemberSelector)e;
            Assert.AreEqual("name", me.getName());
            Assert.IsTrue(me.getParent() is UnresolvedIdentifier);
            UnresolvedIdentifier uie = (UnresolvedIdentifier)me.getParent();
            Assert.AreEqual("p", uie.getName());
        }

        [Test]
        public void testArgument()
        {
            String statement = "Person p";
            ETestParser parser = new ETestParser(statement, false);
            ITypedArgument a = parser.parse_typed_argument();
            Assert.IsNotNull(a);
			Assert.AreEqual("Person", a.getType().GetName());
            Assert.AreEqual("p", a.GetName());
        }

        [Test]
        public void testList1Argument()
        {
            String statement = "Person p";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void testList2ArgumentsComma()
        {
            String statement = "Person p, Employee e";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
        }

        [Test]
        public void testList2ArgumentsAnd()
        {
            String statement = "Person p and Employee e";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
        }

        [Test]
        public void testMethodCallExpression()
        {
            String statement = "print \"person\" + p.name";
            ETestParser parser = new ETestParser(statement, false);
            UnresolvedCall ac = parser.parse_method_call();
            Assert.IsNotNull(ac);
        }

        [Test]
        public void testSimpleArgumentAssignment()
        {
            String statement = "p.name as value";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentAssignment ars = parser.parse_argument_assignment();
			Assert.AreEqual("value", ars.GetName());
            IExpression exp = ars.getExpression();
            Assert.IsNotNull(exp);
			Assert.AreEqual("p.name as value", generate(ars));
        }

        [Test]
        public void testComplexArgumentAssignment()
        {
            String statement = "\"person\" + p.name as value";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentAssignment ars = parser.parse_argument_assignment();
			Assert.AreEqual("value", ars.GetName());
            IExpression exp = ars.getExpression();
            Assert.IsTrue(exp is AddExpression);
			Assert.AreEqual("\"person\" + p.name as value", generate(ars));
        }

        [Test]
        public void testArgumentAssignmentList1Arg()
        {
            String statement = "with \"person\" + p.name as value";
            ETestParser parser = new ETestParser(statement, false);
            ArgumentAssignmentList ls = parser.parse_argument_assignment_list();
            ArgumentAssignment ars = ls[0];
			Assert.AreEqual("value", ars.GetName());
            IExpression exp = ars.getExpression();
            Assert.IsTrue(exp is AddExpression);
			Assert.AreEqual("\"person\" + p.name as value", generate(ars));

        }

        [Test]
        public void testMethodCallWith()
        {
            String statement = "print with \"person\" + p.name as value";
            ETestParser parser = new ETestParser(statement, false);
            UnresolvedCall mc = parser.parse_method_call();
            Assert.IsNotNull(mc);
            Assert.AreEqual("print", mc.getCaller().ToString());
            Assert.IsNotNull(mc.getAssignments());
            ArgumentAssignment ars = mc.getAssignments()[0];
			Assert.AreEqual("value", ars.GetName());
            IExpression exp = ars.getExpression();
            Assert.IsTrue(exp is AddExpression);
			Assert.AreEqual("print with \"person\" + p.name as value", generate(mc));

        }

        [Test]
        public void testMethod1Parameter1Statement()
        {
            String statement = "define printName as method receiving Person p doing:\r\n"
                    + "\tprint with \"person\" + p.name as value";
            ETestParser parser = new ETestParser(statement, false);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
			Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getArguments());
            Assert.IsTrue(ad.getArguments().Contains(new CategoryArgument(new CategoryType("Person"), "p", null)));
            Assert.IsNotNull(ad.getStatements());
            Assert.AreEqual("print with \"person\" + p.name as value", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testMethod1Extended1Statement()
        {
            String statement = "define printName as method receiving Object o with attribute name doing:\r\n"
                    + "\tprint with \"object\" + o.name as value";
            ETestParser parser = new ETestParser(statement, false);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
			Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getArguments());
            IArgument expected = new CategoryArgument(new CategoryType("Object"), "o", new IdentifierList("name"));
            Assert.IsTrue(ad.getArguments().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
			Assert.AreEqual("print with \"object\" + o.name as value", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testMethod1Array1Statement()
        {
            String statement = "define printName as method receiving Option[] options doing:\r\n"
                    + "\tprint with \"array\" + args as value";
            ETestParser parser = new ETestParser(statement, false);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
			Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getArguments());
            IArgument expected = new CategoryArgument(new ListType(new CategoryType("Option")), "options", null);
            Assert.IsTrue(ad.getArguments().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
            Assert.AreEqual("print with \"array\" + args as value", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testConstructor1Attribute()
        {
            String statement = "Company with 1 as id";
            ETestParser parser = new ETestParser(statement, false);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void testConstructorFrom1Attribute()
        {
            String statement = "Company from entity with 1 as id";
            ETestParser parser = new ETestParser(statement, false);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void testConstructor2AttributesComma()
        {
            String statement = "Company with 1 as id, \"IBM\" as name";
            ETestParser parser = new ETestParser(statement, false);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
            ArgumentAssignmentList l = c.getAssignments();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
            ArgumentAssignment a = l[0];
            Assert.IsNotNull(a);
			Assert.AreEqual("id", a.GetName());
            IExpression e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is IntegerLiteral);
            a = l[1];
            Assert.IsNotNull(a);
			Assert.AreEqual("name", a.GetName());
            e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is TextLiteral);
        }

        [Test]
        public void testConstructor2AttributesAnd()
        {
            String statement = "Company with 1 as id and \"IBM\" as name";
            ETestParser parser = new ETestParser(statement, false);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
            ArgumentAssignmentList l = c.getAssignments();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
            ArgumentAssignment a = l[0];
            Assert.IsNotNull(a);
			Assert.AreEqual("id", a.GetName());
            IExpression e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is IntegerLiteral);
            a = l[1];
            Assert.IsNotNull(a);
			Assert.AreEqual("name", a.GetName());
            e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is TextLiteral);
        }

        [Test]
        public void testAssignmentConstructor()
        {
            String statement = "c = Company from x with 1 as id and \"IBM\" as name";
            ETestParser parser = new ETestParser(statement, false);
            AssignInstanceStatement a = parser.parse_assign_instance_statement();
            Assert.IsNotNull(a);
            Assert.IsTrue(a.getExpression() is ConstructorExpression);
        }

        [Test]
        public void testNativeJava()
        {
            String statement = "Java: System.out.println(value);";
            ETestParser parser = new ETestParser(statement, false);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void testNativeCSharp()
        {
            String statement = "C#: Console.println(value);";
            ETestParser parser = new ETestParser(statement, false);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void testNativeMethod()
        {
            String statement = "define print as native method receiving String value doing:\r\n"
                    + "\tJava: System.str.println(value); \r\n"
                    + "\tC#: Console.println(value); ";

            ETestParser parser = new ETestParser(statement, false);
            NativeMethodDeclaration method = parser.parse_native_method_declaration();
            Assert.IsNotNull(method);
            Assert.IsTrue(method is NativeMethodDeclaration);
        }

        [Test]
        public void testBooleanLiteral()
        {
            String statement = "true";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is BooleanLiteral);
            Assert.AreEqual("true", literal.ToString());
            Assert.AreEqual(true, ((BooleanLiteral)literal).getValue().Value);
            statement = "false";
            parser = new ETestParser(statement, false);
            literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is BooleanLiteral);
            Assert.AreEqual("false", literal.ToString());
            Assert.AreEqual(false, ((BooleanLiteral)literal).getValue().Value);
        }

        [Test]
        public void testStringLiteral()
        {
            String statement = "\"hello\"";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is TextLiteral);
			Assert.AreEqual("\"hello\"", generate(literal));
            Assert.AreEqual("hello", ((TextLiteral)literal).getValue().Value);
        }

        [Test]
        public void testIntegerLiteral()
        {
            String statement = "1234";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is IntegerLiteral);
            Assert.AreEqual("1234", literal.ToString());
            Assert.AreEqual(1234, ((IntegerLiteral)literal).getValue().IntegerValue);
        }

        [Test]
        public void testParseHexa()
        {
            Assert.AreEqual(0x0A11, HexaLiteral.parseHexa("0x0A11").IntegerValue);
        }

        [Test]
        public void testHexaLiteral()
        {
            String statement = "0x0A11";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is HexaLiteral);
			Assert.AreEqual("0x0A11", generate(literal));
            Assert.AreEqual(0x0A11, ((HexaLiteral)literal).getValue().IntegerValue);
        }

        [Test]
        public void testDecimalLiteral()
        {
            String statement = "1234.13";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DecimalLiteral);
			Assert.AreEqual("1234.13", generate(literal));
            Assert.AreEqual(1234.13, ((DecimalLiteral)literal).getValue().DecimalValue, 0.0000001);
        }

        [Test]
        public void testEmptyListLiteral()
        {
            String statement = "[]";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is ListLiteral);
            Assert.AreEqual("[]", literal.ToString());
            Assert.AreEqual(0, ((ListLiteral)literal).getValue().Count);
        }

        [Test]
        public void testSimpleListLiteral()
        {
            String statement = "[ john, 123 ]";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.AreEqual("[john, 123]", literal.ToString());
            Assert.IsTrue(literal is ListLiteral);
			ExpressionList list = ((ListLiteral)literal).Expressions;
			Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list[0] is UnresolvedIdentifier);
            Assert.IsTrue(list[1] is IntegerLiteral);
        }

        [Test]
        public void testEmptyDictLiteral()
        {
            String statement = "{}";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
            Assert.AreEqual("{}", literal.ToString());
        }

        [Test]
        public void testSimpleDictLiteral()
        {
            String statement = "{ \"john\" : 1234, eric : 5678 }";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
			Assert.AreEqual("{\"john\":1234, eric:5678}", generate(literal));
        }

        [Test]
        public void testSimpleDate()
        {
            String statement = "'2012-10-09'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateLiteral);
            Assert.AreEqual("'2012-10-09'", generate(literal));
            Assert.AreEqual(new Date(2012, 10, 9), ((DateLiteral)literal).getValue());
        }

        [Test]
        public void testSimpleTime()
        {
            String statement = "'15:03:10'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is TimeLiteral);
            Assert.AreEqual("'15:03:10'", literal.ToString());
            Assert.AreEqual(new Time(15, 03, 10, 0), ((TimeLiteral)literal).getValue());
        }

        [Test]
        public void testDateTime()
        {
            String statement = "'2012-10-09T15:18:17Z'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17Z'", literal.ToString());
            Assert.AreEqual(new DateTime(2012, 10, 9, 15, 18, 17), ((DateTimeLiteral)literal).getValue());
        }

        [Test]
        public void testDateTimeWithMillis()
        {
            String statement = "'2012-10-09T15:18:17.487Z'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17.487Z'", literal.ToString());
            Assert.AreEqual(new DateTime(2012, 10, 9, 15, 18, 17, 487), ((DateTimeLiteral)literal).getValue());
        }

        [Test]
        public void testDateTimeWithTZ()
        {
            String statement = "'2012-10-09T15:18:17+02:00'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17+02:00'", literal.ToString());
            TimeSpan offset = new TimeSpan(2, 0, 0);
            DateTime expected = new DateTime(2012, 10, 9, 15, 18, 17, 0, offset);
            DateTime actual = ((DateTimeLiteral)literal).getValue();
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test]
        public void testPeriod()
        {
            String statement = "'P3Y'";
            ETestParser parser = new ETestParser(statement, false);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is PeriodLiteral);
			Assert.AreEqual("'P3Y'", generate(literal));
            Assert.AreEqual(3, ((PeriodLiteral)literal).getValue().Years);
        }

        [Test]
        public void testNativeSymbol()
        {
            String statement = "ENTITY_1 with \"1\" as value";
            ETestParser parser = new ETestParser(statement, false);
            IExpression symbol = parser.parse_native_symbol();
            Assert.IsNotNull(symbol);
            Assert.IsTrue(symbol is NativeSymbol);
			Assert.AreEqual(statement, generate(symbol));
        }

        [Test]
        public void testExpressionWith()
        {
            String statement = "x = print with \"1\" as value";
            ETestParser parser = new ETestParser(statement, false);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
            Assert.AreEqual(statement, generate(stmt));
        }
        [Test]
        public void testMethodWith()
        {
            String statement = "print \"a\" with \"1\" as value";
            ETestParser parser = new ETestParser(statement, false);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
			Assert.AreEqual(statement, generate(stmt));
        }

		public String generate(IDialectElement elem) {
			Context context = Context.newGlobalContext ();
			CodeWriter writer = new CodeWriter (Dialect.E, context);
			elem.ToDialect (writer);
			return writer.ToString ();
		}
	}


    class ETestParser : ECleverParser {
		
		public ETestParser(String code, bool addLF) 
    		: base(code)
	    {
            ITokenStream stream = (ITokenStream)this.InputStream;
            EIndentingLexer lexer = (EIndentingLexer)stream.TokenSource;
            lexer.AddLF = addLF;
		}

		public IAssignableInstance parse_assignable() {
			IParseTree tree = assignable_instance();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IAssignableInstance>(tree);
		}

		public IntegerLiteral parse_atomic_literal() {
			IParseTree tree = atomic_literal();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
			return builder.GetNodeValue<IntegerLiteral>(tree);
		}

		public ArgumentAssignmentList parse_argument_assignment_list() {
			IParseTree tree = argument_assignment_list();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ArgumentAssignmentList>(tree);
		}

		public ArgumentAssignment parse_argument_assignment() {
			IParseTree tree = argument_assignment();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ArgumentAssignment>(tree);
		}

		public IExpression parse_instance_expression() {
			IParseTree tree = instance_expression();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

		public RangeLiteral parse_range_literal() {
			IParseTree tree = range_literal();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<RangeLiteral>(tree);
		}
		
		public TupleLiteral parse_tuple_literal() {
			IParseTree tree = tuple_literal();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<TupleLiteral>(tree);
		}

		public AttributeDeclaration parse_attribute_declaration() {
			IParseTree tree = attribute_declaration();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<AttributeDeclaration>(tree);
		}

		public CategoryDeclaration parse_category_declaration() {
			IParseTree tree = category_declaration();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<CategoryDeclaration>(tree);
		}

		public ITypedArgument parse_typed_argument() {
			IParseTree tree = typed_argument();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ITypedArgument>(tree);
		}

		public ArgumentList parse_argument_list() {
			IParseTree tree = full_argument_list();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ArgumentList>(tree);
		}

        public UnresolvedCall parse_method_call()
        {
			IParseTree tree = method_call_statement();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
            return builder.GetNodeValue<UnresolvedCall>(tree);
		}

		public NativeMethodDeclaration parse_native_method_declaration() {
			IParseTree tree = native_method_declaration();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<NativeMethodDeclaration>(tree);
		}

		public ConcreteMethodDeclaration parse_concrete_method_declaration() {
			IParseTree tree = concrete_method_declaration();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ConcreteMethodDeclaration>(tree);
		}

		public ConstructorExpression parse_constructor_expression() {
			IParseTree tree = constructor_expression();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ConstructorExpression>(tree);
		}

		public AssignInstanceStatement parse_assign_instance_statement() {
			IParseTree tree = assign_instance_statement();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<AssignInstanceStatement>(tree);
		}

		public NativeCall parse_native_statement() {
			IParseTree tree = native_statement();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<NativeCall>(tree);
		}

		public IExpression parse_literal_expression() {
			IParseTree tree = literal_expression();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

        public IExpression parse_native_symbol() {
			IParseTree tree = native_symbol();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

		public IStatement parse_statement() {
			IParseTree tree = statement();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IStatement>(tree);
		}

		public IExpression parse_expression() {
			IParseTree tree = expression();
			EPrestoBuilder builder = new EPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}
	}
}

using NUnit.Framework;
using System;
using presto.grammar;
using presto.value;
using DateTime = presto.value.DateTime;
using presto.parser;
using presto.literal;
using presto.declaration;
using presto.statement;
using presto.expression;
using presto.type;
using Antlr4.Runtime.Tree;
using presto.runtime;
using presto.utils;

namespace presto.parser
{

    [TestFixture]
    public class TestOParserAtoms
    {

        [Test]
        public void testTuple()
        {
            String statement = "(1,\"John\",'12:12:12')";
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
            RangeLiteral rl = parser.parse_range_literal();
            Assert.IsNotNull(rl);
            Assert.AreEqual("1", rl.getFirst().ToString());
            Assert.AreEqual("100", rl.getLast().ToString());
			Assert.AreEqual("[1..100]", generate(rl));
        }

        [Test]
        public void testAttribute()
        {
            String statement = "attribute id : Integer; ";
            OTestParser parser = new OTestParser(statement);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("id", ad.getName());
            Assert.AreEqual("Integer", ad.getType().getName());
        }

        [Test]
        public void testArrayAttribute()
        {
            String statement = "attribute id : Integer[]; ";
            OTestParser parser = new OTestParser(statement);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("id", ad.getName());
            Assert.AreEqual("Integer[]", ad.getType().getName());
        }

        [Test]
        public void testCategory1Attribute()
        {
            String statement = "category Person ( id );";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Person", cd.getName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("id"));
        }

        [Test]
        public void testCategory2Attributes()
        {
            String statement = "category Person ( id, name );";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Person", cd.getName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("id"));
            Assert.IsTrue(cd.getAttributes().Contains("name"));
        }

        [Test]
        public void testCategory1Derived1Attribute()
        {
            String statement = "category Employee( company ) extends Person ;";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Employee", cd.getName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsNotNull(cd.getAttributes());
            Assert.IsTrue(cd.getAttributes().Contains("company"));
        }

        [Test]
        public void testCategory2DerivedNoAttribute()
        {
            String statement = "category Entrepreneur extends Person, Company;";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Entrepreneur", cd.getName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsTrue(cd.getDerivedFrom().Contains("Company"));
            Assert.IsNull(cd.getAttributes());
        }

        [Test]
        public void testMemberExpression()
        {
            String statement = "p.name";
            OTestParser parser = new OTestParser(statement);
            IExpression e = parser.parse_instance_expression();
            Assert.IsTrue(e is MemberSelector);
            MemberSelector me = (MemberSelector)e;
            Assert.AreEqual("name", me.getName());
            Assert.IsTrue(me.getParent() is InstanceExpression);
            InstanceExpression uie = (InstanceExpression)me.getParent();
            Assert.AreEqual("p", uie.getName());
        }

        [Test]
        public void testArgument()
        {
            String statement = "Person p";
            OTestParser parser = new OTestParser(statement);
            ITypedArgument a = parser.parse_typed_argument();
            Assert.IsNotNull(a);
            Assert.AreEqual("Person", a.getType().getName());
            Assert.AreEqual("p", a.getName());
        }

        [Test]
        public void testList1Argument()
        {
            String statement = "Person p";
            OTestParser parser = new OTestParser(statement);
            ArgumentList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void testList2ArgumentsComma()
        {
            String statement = "Person p, Employee e";
            OTestParser parser = new OTestParser(statement);
            ArgumentList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
        }


        [Test]
        public void testMethodCallExpression()
        {
            String statement = "print (\"person\" + p.name );";
            OTestParser parser = new OTestParser(statement);
            MethodCall ac = parser.parse_method_call();
            Assert.IsNotNull(ac);
        }

        [Test]
        public void testMethodCallWith()
        {
            String statement = "print( value = \"person\" + p.name ); ";
            OTestParser parser = new OTestParser(statement);
            MethodCall mc = parser.parse_method_call();
            Assert.IsNotNull(mc);
            Assert.AreEqual("print", mc.getMethod().getName());
            Assert.IsNotNull(mc.getAssignments());
            ArgumentAssignment ars = mc.getAssignments()[0];
            Assert.AreEqual("value", ars.getName());
            IExpression exp = ars.getExpression();
            Assert.IsTrue(exp is AddExpression);
			Assert.AreEqual("print(value = \"person\" + p.name)", generate(mc));

        }

        [Test]
        public void testMethod1Parameter1Statement()
        {
            String statement = "method printName ( Person p ) { print ( value = \"person\" + p.name); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.getName());
            Assert.IsNotNull(ad.getArguments());
            Assert.IsTrue(ad.getArguments().Contains(new CategoryArgument(new CategoryType("Person"), "p", null)));
            Assert.IsNotNull(ad.getStatements());
			Assert.AreEqual("print(value = \"person\" + p.name)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testMethod1Extended1Statement()
        {
            String statement = "method printName ( Object(name) o ) { print ( value = \"object\" + o.name ); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.getName());
            Assert.IsNotNull(ad.getArguments());
            IArgument expected = new CategoryArgument(new CategoryType("Object"), "o", new IdentifierList("name"));
            Assert.IsTrue(ad.getArguments().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
			Assert.AreEqual("print(value = \"object\" + o.name)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testMethod1Array1Statement()
        {
            String statement = "method printName ( Option[] options ) { print ( value = \"array\" + options ); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.getName());
            Assert.IsNotNull(ad.getArguments());
            IArgument expected = new CategoryArgument(new ListType(new CategoryType("Option")), "options", null);
            Assert.IsTrue(ad.getArguments().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
			Assert.AreEqual("print(value = \"array\" + options)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void testConstructor1Attribute()
        {
            String statement = "Company(id=1)";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void testConstructorFrom1Attribute()
        {
            String statement = "Company(entity,id=1)";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void testConstructor2AttributesComma()
        {
            String statement = "Company(id=1, name=\"IBM\")";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
            ArgumentAssignmentList l = c.getAssignments();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
            ArgumentAssignment a = l[0];
            Assert.IsNotNull(a);
            Assert.AreEqual("id", a.getName());
            IExpression e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is IntegerLiteral);
            a = l[1];
            Assert.IsNotNull(a);
            Assert.AreEqual("name", a.getName());
            e = a.getExpression();
            Assert.IsNotNull(e);
            Assert.IsTrue(e is TextLiteral);
        }

        [Test]
        public void testAssignmentConstructor()
        {
            String statement = "c = Company ( id = 1, name = \"IBM\" );";
            OTestParser parser = new OTestParser(statement);
            AssignInstanceStatement a = parser.parse_assign_instance_statement();
            Assert.IsNotNull(a);
            Assert.IsTrue(a.getExpression() is ConstructorExpression);
        }

        [Test]
        public void testNativeJava()
        {
            String statement = "Java: System.str.println(value);";
            OTestParser parser = new OTestParser(statement);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void testNativeCSharp()
        {
            String statement = "C#: Console.println(value);";
            OTestParser parser = new OTestParser(statement);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void testNativeMethod()
        {
            String statement = "native method print (String value) {\r\n"
                    + "\tJava: System.str.println(value); \r\n"
                    + "\tC#: Console.println(value); }";

            OTestParser parser = new OTestParser(statement);
            NativeMethodDeclaration method = parser.parse_native_method_declaration();
            Assert.IsNotNull(method);
            Assert.IsTrue(method is NativeMethodDeclaration);
        }

        [Test]
        public void testBooleanLiteral()
        {
            String statement = "true";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is BooleanLiteral);
            Assert.AreEqual("true", literal.ToString());
            Assert.AreEqual(true, ((BooleanLiteral)literal).getValue().Value);
            statement = "false";
            parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.AreEqual("[john, 123]", generate(literal));
            Assert.IsTrue(literal is ListLiteral);
			ExpressionList list = ((ListLiteral)literal).Expressions;
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list[0] is InstanceExpression);
            Assert.IsTrue(list[1] is IntegerLiteral);
        }

        [Test]
        public void testEmptyDictLiteral()
        {
            String statement = "{}";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
            Assert.AreEqual("{}", literal.ToString());
        }

        [Test]
        public void testSimpleDictLiteral()
        {
            String statement = "{ \"john\" : 1234, eric : 5678 }";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
            Assert.AreEqual("{\"john\":1234, eric:5678}", generate(literal)); // not very relevant since the literal is not evaluated yet
        }

        [Test]
        public void testSimpleDate()
        {
            String statement = "'2012-10-09'";
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
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
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is PeriodLiteral);
            Assert.AreEqual("'P3Y'", generate(literal));
            Assert.AreEqual(3, ((PeriodLiteral)literal).getValue().Years);
        }

        [Test]
        public void testNativeSymbol()
        {
            String statement = "ENTITY_1 = \"1\"; ";
            OTestParser parser = new OTestParser(statement);
            IExpression symbol = parser.parse_native_symbol();
            Assert.IsNotNull(symbol);
            Assert.IsTrue(symbol is NativeSymbol);
			Assert.AreEqual("\"1\"", ((NativeSymbol)symbol).getExpression().ToString());
        }

        [Test]
        public void testExpressionWith()
        {
            String statement = "x = print ( value = \"1\" );";
            OTestParser parser = new OTestParser(statement);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
            // Assert.AreEqual(statement, stmt.ToString());
			Assert.AreEqual("x = print(value = \"1\")", generate(stmt));
        }

        [Test]
        public void testMethodWith()
        {
            String statement = "print (\"a\", value = \"1\");";
            OTestParser parser = new OTestParser(statement);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
            // Assert.AreEqual(statement, stmt.ToString());
			Assert.AreEqual("print(\"a\", value = \"1\")", generate(stmt));
        }

        [Test]
        public void testInstanceExpression()
        {
            String statement = "prefix ";
            OTestParser parser = new OTestParser(statement);
            IExpression exp = parser.parse_expression();
            Assert.IsTrue(exp is InstanceExpression);
        }

        /*
        	print ("a" + ((1,2,3,4) contains any (1,2)));
	print ("b" + ((1,2,3,4) contains any [1,2]));
	print ("c" + ((1,2,3,4) contains any (4,5)));
	print ("d" + ((1,2,3,4) contains any [4,5]));
        */
        [Test]
        public void testContainsAnyExpression()
        {
            String statement = "(1,2,3,4) contains any (1,2)";
            OTestParser parser = new OTestParser(statement);
            IExpression exp = parser.parse_expression();
            Assert.IsTrue(exp is ContainsExpression);
        }

		public String generate(IDialectElement elem) {
			Context context = Context.newGlobalContext ();
			CodeWriter writer = new CodeWriter (Dialect.O, context);
			elem.ToDialect (writer);
			return writer.ToString ();
		}

    }

    	class OTestParser : OCleverParser {
		
		public OTestParser(String code) 
     		: base(code)
	   {
		}

		public IExpression parse_instance_expression() {
			IParseTree tree = instance_expression();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

        public TupleLiteral parse_tuple_literal()
        {
            IParseTree tree = tuple_literal();
            OPrestoBuilder builder = new OPrestoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<TupleLiteral>(tree);
        }

        public RangeLiteral parse_range_literal()
        {
			IParseTree tree = range_literal();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<RangeLiteral>(tree);
		}
		
	
		public AttributeDeclaration parse_attribute_declaration() {
			IParseTree tree = attribute_declaration();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<AttributeDeclaration>(tree);
		}

		public CategoryDeclaration parse_category_declaration() {
			IParseTree tree = category_declaration();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<CategoryDeclaration>(tree);
		}

		public ITypedArgument parse_typed_argument() {
			IParseTree tree = typed_argument();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ITypedArgument>(tree);
		}

		public ArgumentList parse_argument_list() {
			IParseTree tree = argument_list();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ArgumentList>(tree);
		}

		public MethodCall parse_method_call() {
			IParseTree tree = method_call();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<MethodCall>(tree);
		}

		public NativeMethodDeclaration parse_native_method_declaration() {
			IParseTree tree = native_method_declaration();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<NativeMethodDeclaration>(tree);
		}

		public ConcreteMethodDeclaration parse_concrete_method_declaration() {
			IParseTree tree = concrete_method_declaration();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ConcreteMethodDeclaration>(tree);
		}

		public ConstructorExpression parse_constructor_expression() {
			IParseTree tree = constructor_expression();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<ConstructorExpression>(tree);
		}

		public AssignInstanceStatement parse_assign_instance_statement() {
			IParseTree tree = assign_instance_statement();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<AssignInstanceStatement>(tree);
		}

		public NativeCall parse_native_statement() {
			IParseTree tree = native_statement();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<NativeCall>(tree);
		}

		public IExpression parse_literal_expression() {
			IParseTree tree = literal_expression();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

		public IExpression parse_native_symbol() {
			IParseTree tree = native_symbol();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}

		public IStatement parse_statement() {
			IParseTree tree = statement();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IStatement>(tree);
		}

		public IExpression parse_expression() {
			IParseTree tree = expression();
			OPrestoBuilder builder = new OPrestoBuilder(this);
			ParseTreeWalker walker = new ParseTreeWalker();
			walker.Walk(builder, tree);
			return builder.GetNodeValue<IExpression>(tree);
		}
	}

}
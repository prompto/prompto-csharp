using NUnit.Framework;
using System;
using prompto.grammar;
using prompto.value;
using DateTimeValue = prompto.value.DateTimeValue;
using prompto.parser;
using prompto.literal;
using prompto.declaration;
using prompto.statement;
using prompto.expression;
using prompto.type;
using Antlr4.Runtime.Tree;
using prompto.runtime;
using prompto.utils;
using prompto.param;

namespace prompto.parser
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
        public void parsesRange()
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
        public void parsesAttribute()
        {
            String statement = "attribute id : Integer; ";
            OTestParser parser = new OTestParser(statement);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("id", ad.GetName());
            Assert.AreEqual("Integer", ad.getIType().GetTypeName());
        }

        [Test]
        public void parsesArrayAttribute()
        {
            String statement = "attribute id : Integer[]; ";
            OTestParser parser = new OTestParser(statement);
            AttributeDeclaration ad = parser.parse_attribute_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("id", ad.GetName());
            Assert.AreEqual("Integer[]", ad.getIType().GetTypeName());
        }

        [Test]
        public void parsesCategory1Attribute()
        {
            String statement = "category Person ( id );";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Person", cd.GetName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.GetLocalAttributes());
            Assert.IsTrue(cd.GetLocalAttributes().Contains("id"));
        }

        [Test]
        public void parsesCategory2Attributes()
        {
            String statement = "category Person ( id, name );";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Person", cd.GetName());
            Assert.IsNull(cd.getDerivedFrom());
            Assert.IsNotNull(cd.GetLocalAttributes());
            Assert.IsTrue(cd.GetLocalAttributes().Contains("id"));
            Assert.IsTrue(cd.GetLocalAttributes().Contains("name"));
        }

        [Test]
        public void parsesCategory1Derived1Attribute()
        {
            String statement = "category Employee( company ) extends Person ;";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Employee", cd.GetName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsNotNull(cd.GetLocalAttributes());
            Assert.IsTrue(cd.GetLocalAttributes().Contains("company"));
        }

        [Test]
        public void parsesCategory2DerivedNoAttribute()
        {
            String statement = "category Entrepreneur extends Person, Company;";
            OTestParser parser = new OTestParser(statement);
            CategoryDeclaration cd = parser.parse_category_declaration();
            Assert.IsNotNull(cd);
            Assert.AreEqual("Entrepreneur", cd.GetName());
            Assert.IsNotNull(cd.getDerivedFrom());
            Assert.IsTrue(cd.getDerivedFrom().Contains("Person"));
            Assert.IsTrue(cd.getDerivedFrom().Contains("Company"));
            Assert.IsNull(cd.GetLocalAttributes());
        }

        [Test]
        public void parsesMemberExpression()
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
        public void parsesArgument()
        {
            String statement = "Person p";
            OTestParser parser = new OTestParser(statement);
            ITypedParameter a = parser.parse_typed_argument();
            Assert.IsNotNull(a);
            Assert.AreEqual("Person", a.getType().GetTypeName());
            Assert.AreEqual("p", a.GetName());
        }

        [Test]
        public void parsesList1Argument()
        {
            String statement = "Person p";
            OTestParser parser = new OTestParser(statement);
            ParameterList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void parsesList2ArgumentsComma()
        {
            String statement = "Person p, Employee e";
            OTestParser parser = new OTestParser(statement);
            ParameterList l = parser.parse_argument_list();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
        }


        [Test]
        public void parsesMethodCallExpression()
        {
            String statement = "print (\"person\" + p.name );";
            OTestParser parser = new OTestParser(statement);
            UnresolvedCall ac = parser.parse_method_call_statement();
            Assert.IsNotNull(ac);
        }

        [Test]
        public void parsesMethodCallWith()
        {
            String statement = "print( value = \"person\" + p.name ); ";
            OTestParser parser = new OTestParser(statement);
            UnresolvedCall mc = parser.parse_method_call_statement();
            Assert.IsNotNull(mc);
            Assert.AreEqual("print", mc.getCaller().ToString());
            Assert.IsNotNull(mc.getArguments());
            Argument ars = mc.getArguments()[0];
            Assert.AreEqual("value", ars.GetName());
            IExpression exp = ars.getExpression();
            Assert.IsTrue(exp is PlusExpression);
            Assert.AreEqual("print(value = \"person\" + p.name)", generate(mc));

        }

        [Test]
        public void parsesMethod1Parameter1Statement()
        {
            String statement = "method printName ( Person p ) { print ( value = \"person\" + p.name); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getParameters());
            Assert.IsTrue(ad.getParameters().Contains(new CategoryParameter(new CategoryType("Person"), "p")));
            Assert.IsNotNull(ad.getStatements());
            Assert.AreEqual("print(value = \"person\" + p.name)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void parsesMethod1Extended1Statement()
        {
            String statement = "method printName ( Object(name) o ) { print ( value = \"object\" + o.name ); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getParameters());
            IParameter expected = new ExtendedParameter(new CategoryType("Object"), "o", new IdentifierList("name"));
            Assert.IsTrue(ad.getParameters().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
            Assert.AreEqual("print(value = \"object\" + o.name)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void parsesMethod1Array1Statement()
        {
            String statement = "method printName ( Option[] options ) { print ( value = \"array\" + options ); }";
            OTestParser parser = new OTestParser(statement);
            ConcreteMethodDeclaration ad = parser.parse_concrete_method_declaration();
            Assert.IsNotNull(ad);
            Assert.AreEqual("printName", ad.GetName());
            Assert.IsNotNull(ad.getParameters());
            IParameter expected = new CategoryParameter(new ListType(new CategoryType("Option")), "options");
            Assert.IsTrue(ad.getParameters().Contains(expected));
            Assert.IsNotNull(ad.getStatements());
            Assert.AreEqual("print(value = \"array\" + options)", generate(ad.getStatements()[0]));

        }

        [Test]
        public void parsesConstructor1Attribute()
        {
            String statement = "Company(id=1)";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void parsesConstructorFrom1Attribute()
        {
            String statement = "Company(entity,id=1)";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
        }

        [Test]
        public void parsesConstructor2AttributesComma()
        {
            String statement = "Company(id=1, name=\"IBM\")";
            OTestParser parser = new OTestParser(statement);
            ConstructorExpression c = parser.parse_constructor_expression();
            Assert.IsNotNull(c);
            ArgumentList l = c.getArguments();
            Assert.IsNotNull(l);
            Assert.AreEqual(2, l.Count);
            Argument a = l[0];
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
        public void parsesAssignmentConstructor()
        {
            String statement = "c = Company ( id = 1, name = \"IBM\" );";
            OTestParser parser = new OTestParser(statement);
            AssignInstanceStatement a = parser.parse_assign_instance_statement();
            Assert.IsNotNull(a);
            Assert.IsTrue(a.getExpression() is UnresolvedCall);
        }

        [Test]
        public void parsesNativeJava()
        {
            String statement = "Java: System.str.println(value);";
            OTestParser parser = new OTestParser(statement);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void parsesNativeCSharp()
        {
            String statement = "C#: Console.println(value);";
            OTestParser parser = new OTestParser(statement);
            NativeCall call = parser.parse_native_statement();
            Assert.IsNotNull(call);
            Assert.IsTrue(call is NativeCall);
        }

        [Test]
        public void parsesNativeMethod()
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
        public void parsesBooleanLiteral()
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
        public void parsesStringLiteral()
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
        public void parsesIntegerLiteral()
        {
            String statement = "1234";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is IntegerLiteral);
            Assert.AreEqual("1234", literal.ToString());
            Assert.AreEqual(1234, ((IntegerLiteral)literal).getValue().LongValue);
        }

        [Test]
        public void parsesParseHexa()
        {
            Assert.AreEqual(0x0A11, HexaLiteral.parseHexa("0x0A11").LongValue);
        }

        [Test]
        public void parsesHexaLiteral()
        {
            String statement = "0x0A11";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is HexaLiteral);
            Assert.AreEqual("0x0A11", generate(literal));
            Assert.AreEqual(0x0A11, ((HexaLiteral)literal).getValue().LongValue);
        }

        [Test]
        public void parsesDecimalLiteral()
        {
            String statement = "1234.13";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DecimalLiteral);
            Assert.AreEqual("1234.13", generate(literal));
            Assert.AreEqual(1234.13, ((DecimalLiteral)literal).getValue().DoubleValue, 0.0000001);
        }

        [Test]
        public void parsesEmptyListLiteral()
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
        public void parsesSimpleListLiteral()
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
        public void parsesEmptyDictLiteral()
        {
            String statement = "<:>";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
            Assert.AreEqual("<:>", literal.ToString());
        }

        [Test]
        public void parsesSimpleDictLiteral()
        {
            String statement = "< \"john\" : 1234, eric : 5678 >";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DictLiteral);
            Assert.AreEqual("<\"john\":1234, eric:5678>", generate(literal)); // not very relevant since the literal is not evaluated yet
        }

        [Test]
        public void parsesSimpleDate()
        {
            String statement = "'2012-10-09'";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateLiteral);
            Assert.AreEqual("'2012-10-09'", generate(literal));
            Assert.AreEqual(new DateValue(2012, 10, 9), ((DateLiteral)literal).getValue());
        }

        [Test]
        public void parsesSimpleTime()
        {
            String statement = "'15:03:10'";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is TimeLiteral);
            Assert.AreEqual("'15:03:10'", literal.ToString());
            Assert.AreEqual(new TimeValue(15, 03, 10, 0), ((TimeLiteral)literal).getValue());
        }

        [Test]
        public void parsesDateTime()
        {
            String statement = "'2012-10-09T15:18:17Z'";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17Z'", literal.ToString());
            Assert.AreEqual(new DateTimeValue(2012, 10, 9, 15, 18, 17), ((DateTimeLiteral)literal).getValue());
        }

        [Test]
        public void parsesDateTimeWithMillis()
        {
            String statement = "'2012-10-09T15:18:17.487Z'";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17.487Z'", literal.ToString());
            Assert.AreEqual(new DateTimeValue(2012, 10, 9, 15, 18, 17, 487), ((DateTimeLiteral)literal).getValue());
        }

        [Test]
        public void parsesDateTimeWithTZ()
        {
            String statement = "'2012-10-09T15:18:17+02:00'";
            OTestParser parser = new OTestParser(statement);
            IExpression literal = parser.parse_literal_expression();
            Assert.IsNotNull(literal);
            Assert.IsTrue(literal is DateTimeLiteral);
            Assert.AreEqual("'2012-10-09T15:18:17+02:00'", literal.ToString());
            TimeSpan offset = new TimeSpan(2, 0, 0);
            DateTimeValue expected = new DateTimeValue(2012, 10, 9, 15, 18, 17, 0, offset);
            DateTimeValue actual = ((DateTimeLiteral)literal).getValue();
            Assert.IsTrue(expected.Equals(actual));
        }


        [Test]
        public void parsesPeriod()
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
        public void parsesNativeSymbol()
        {
            String statement = "ENTITY_1 = \"1\"; ";
            OTestParser parser = new OTestParser(statement);
            IExpression symbol = parser.parse_native_symbol();
            Assert.IsNotNull(symbol);
            Assert.IsTrue(symbol is NativeSymbol);
            Assert.AreEqual("\"1\"", ((NativeSymbol)symbol).getExpression().ToString());
        }

        [Test]
        public void parsesExpressionWith()
        {
            String statement = "x = print ( value = \"1\" );";
            OTestParser parser = new OTestParser(statement);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
            // Assert.AreEqual(statement, stmt.ToString());
            Assert.AreEqual("x = print(value = \"1\")", generate(stmt));
        }

        [Test]
        public void parsesMethodWith()
        {
            String statement = "print (\"a\", value = \"1\");";
            OTestParser parser = new OTestParser(statement);
            IStatement stmt = parser.parse_statement();
            Assert.IsNotNull(stmt);
            // Assert.AreEqual(statement, stmt.ToString());
            Assert.AreEqual("print(\"a\", value = \"1\")", generate(stmt));
        }

        [Test]
        public void parsesInstanceExpression()
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
        public void parsesHasAnyExpression()
        {
            String statement = "(1,2,3,4) has any (1,2)";
            OTestParser parser = new OTestParser(statement);
            IExpression exp = parser.parse_expression();
            Assert.IsTrue(exp is ContainsExpression);
        }

        [Test]
        public void parsesVersionLiterals() 
        {
            foreach(String literal in new String[]{ "'v1.3'", "'v1.3.15'", "'v1.3-alpha'", "'v1.3-beta'", "'v1.3-candidate'",
                "'v1.3.15-alpha'", "'v1.3.15-beta'", "'v1.3.15-candidate'",
                "'latest'", "'development'" })
            {
                parsesVersionLiteral(literal);
            }
        }

        private void parsesVersionLiteral(String literal)
        {
            OTestParser parser = new OTestParser(literal);
            IExpression exp = parser.parse_expression();
            Assert.IsTrue(exp is VersionLiteral);
            Assert.AreEqual(literal, "'" + ((VersionLiteral)exp).getValue().ToString() + "'");
        }


    public String generate(IDialectElement elem)
        {
            Context context = Context.newGlobalsContext();
            CodeWriter writer = new CodeWriter(Dialect.O, context);
            elem.ToDialect(writer);
            return writer.ToString();
        }

    }

    class OTestParser : OCleverParser
    {

        public OTestParser(String code)
             : base(code)
        {
        }

        public IExpression parse_instance_expression()
        {
            IParseTree tree = instance_expression();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }

        public TupleLiteral parse_tuple_literal()
        {
            IParseTree tree = tuple_literal();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<TupleLiteral>(tree);
        }

        public RangeLiteral parse_range_literal()
        {
            IParseTree tree = range_literal();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<RangeLiteral>(tree);
        }


        public AttributeDeclaration parse_attribute_declaration()
        {
            IParseTree tree = attribute_declaration();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<AttributeDeclaration>(tree);
        }

        public CategoryDeclaration parse_category_declaration()
        {
            IParseTree tree = category_declaration();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<CategoryDeclaration>(tree);
        }

        public ITypedParameter parse_typed_argument()
        {
            IParseTree tree = typed_argument();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<ITypedParameter>(tree);
        }

        public ParameterList parse_argument_list()
        {
            IParseTree tree = argument_list();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<ParameterList>(tree);
        }

        public UnresolvedCall parse_method_call_statement()
        {
            IParseTree tree = method_call_statement();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<UnresolvedCall>(tree);
        }

        public NativeMethodDeclaration parse_native_method_declaration()
        {
            IParseTree tree = native_method_declaration();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<NativeMethodDeclaration>(tree);
        }

        public ConcreteMethodDeclaration parse_concrete_method_declaration()
        {
            IParseTree tree = concrete_method_declaration();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<ConcreteMethodDeclaration>(tree);
        }

        public ConstructorExpression parse_constructor_expression()
        {
            IParseTree tree = constructor_expression();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<ConstructorExpression>(tree);
        }

        public AssignInstanceStatement parse_assign_instance_statement()
        {
            IParseTree tree = assign_instance_statement();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<AssignInstanceStatement>(tree);
        }

        public NativeCall parse_native_statement()
        {
            IParseTree tree = native_statement();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<NativeCall>(tree);
        }

        public IExpression parse_literal_expression()
        {
            IParseTree tree = literal_expression();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }

        public IExpression parse_native_symbol()
        {
            IParseTree tree = native_symbol();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }

        public IStatement parse_statement()
        {
            IParseTree tree = statement();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IStatement>(tree);
        }

        public IExpression parse_expression()
        {
            IParseTree tree = expression();
            OPromptoBuilder builder = new OPromptoBuilder(this);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(builder, tree);
            return builder.GetNodeValue<IExpression>(tree);
        }
    }

}
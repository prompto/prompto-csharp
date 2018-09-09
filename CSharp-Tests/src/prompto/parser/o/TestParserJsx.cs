using System;
using NUnit.Framework;
using prompto.runtime;
using prompto.statement;
using prompto.utils;

namespace prompto.parser.o
{

	[TestFixture]
	public class TestParserJsx : BaseOParserTest
	{

		[Test]
		public void canParseAndTranslateMultilineElements()
		{
			String jsx = "return <a>\n\t<b/>\n\t<b/>\n</a>;";
			OCleverParser parser = new OCleverParser(jsx);
			ReturnStatement stmt = parser.doParse<ReturnStatement>(parser.return_statement);
			Assert.IsNotNull(stmt.getExpression());
			CodeWriter writer = new CodeWriter(Dialect.M, Context.newGlobalContext());
			stmt.ToDialect(writer);
			writer.append(";");
			String result = writer.ToString();
			Assert.AreEqual(jsx, result);
		}

		[Test]
		public void canParseAndTranslateMultilineAttributes()
		{
			String jsx = "return <a \n\tx=\"abc\"\n\ty=\"def\"\n\tz=\"stuff\" />;";
			OCleverParser parser = new OCleverParser(jsx);
			ReturnStatement stmt = parser.doParse<ReturnStatement>(parser.return_statement);
			Assert.IsNotNull(stmt.getExpression());
			CodeWriter writer = new CodeWriter(Dialect.M, Context.newGlobalContext());
			stmt.ToDialect(writer);
			writer.append(";");
			String result = writer.ToString();
			Assert.AreEqual(jsx, result);
		}

	}

}

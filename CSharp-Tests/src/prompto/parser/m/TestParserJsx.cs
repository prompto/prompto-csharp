using System;
using NUnit.Framework;
using prompto.runtime;
using prompto.statement;
using prompto.utils;

namespace prompto.parser.m
{

	[TestFixture]
	public class TestParserJsx : BaseMParserTest
	{

		[Test]
		public void canParseAndTranslateMultilineElements()
		{
			String jsx = "return <a>\n\t<b/>\n\t<b/>\n</a>";
			MCleverParser parser = new MCleverParser(jsx);
			ReturnStatement stmt = parser.doParse<ReturnStatement>(parser.return_statement, true);
			Assert.IsNotNull(stmt.getExpression());
			CodeWriter writer = new CodeWriter(Dialect.M, Context.newGlobalContext());
			stmt.ToDialect(writer);
			String result = writer.ToString();
			Assert.AreEqual(jsx, result);
		}

		[Test]
		public void canParseAndTranslateMultilineAttributes()
		{
			String jsx = "return <a \n\tx=\"abc\"\n\ty=\"def\"\n\tz=\"stuff\" />";
			MCleverParser parser = new MCleverParser(jsx);
			ReturnStatement stmt = parser.doParse<ReturnStatement>(parser.return_statement, true);
			Assert.IsNotNull(stmt.getExpression());
			CodeWriter writer = new CodeWriter(Dialect.M, Context.newGlobalContext());
			stmt.ToDialect(writer);
			String result = writer.ToString();
			Assert.AreEqual(jsx, result);
		}

	}

}

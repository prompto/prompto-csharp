using System;
using Antlr4.Runtime;
using NUnit.Framework;
using prompto.runtime;
using prompto.statement;
using prompto.utils;

namespace prompto.parser.m
{

	[TestFixture]
	public class TestParserCss : BaseMParserTest
	{

		[Test]
		public void canParseCommentLikeCssValue()
        {
			String css = "#999";
			MIndentingLexer lexer = new MIndentingLexer(CharStreams.fromString(css));
			IToken token = lexer.NextToken();
			Assert.AreEqual(MLexer.CSS_DATA, token.Type);
		}

		[Test]
		public void canParseAndTranslateCss()
		{
			String css = "s2 = {color:#999;}";
			MCleverParser parser = new MCleverParser(css);
			IStatement stmt = parser.doParse<AssignInstanceStatement>(parser.assign_instance_statement, false);
			Assert.IsNotNull(stmt);
			CodeWriter writer = new CodeWriter(Dialect.M, Context.newGlobalsContext());
			stmt.ToDialect(writer);
			String result = writer.ToString();
			Assert.AreEqual(css, result);
		}

	}

}

using System;
using NUnit.Framework;
using prompto.runtime;
using prompto.statement;
using prompto.utils;

namespace prompto.parser.e
{

	[TestFixture]
	public class TestParserCss : BaseEParserTest
	{

		[Test]
		public void canParseAndTranslateCss()
		{
			String css = "s2 = {color:#999;}";
			ECleverParser parser = new ECleverParser(css);
			IStatement stmt = parser.doParse<AssignInstanceStatement>(parser.assign_instance_statement, false);
			Assert.IsNotNull(stmt);
			CodeWriter writer = new CodeWriter(Dialect.E, Context.newGlobalsContext());
			stmt.ToDialect(writer);
			String result = writer.ToString();
			Assert.AreEqual(css, result);
		}

	}

}

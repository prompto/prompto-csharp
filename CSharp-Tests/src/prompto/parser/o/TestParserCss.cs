using System;
using NUnit.Framework;
using prompto.runtime;
using prompto.statement;
using prompto.utils;

namespace prompto.parser.o
{

	[TestFixture]
	public class TestParserCss : BaseOParserTest
	{

		[Test]
		public void canParseAndTranslateCss()
		{
			String css = "s2 = {color:#999;};";
			OCleverParser parser = new OCleverParser(css);
			IStatement stmt = parser.doParse<AssignInstanceStatement>(parser.assign_instance_statement);
			Assert.IsNotNull(stmt);
			CodeWriter writer = new CodeWriter(Dialect.O, Context.newGlobalsContext());
			stmt.ToDialect(writer);
			String result = writer.ToString() + ";";
			Assert.AreEqual(css, result);
		}

	}

}

using presto.runtime;
using NUnit.Framework;
using presto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using presto.utils;

namespace presto.parser
{

	public abstract class BaseSParserTest  : BaseParserTest
    {

		public DeclarationList parseString(String code)
		{
			return parsePString(code);
		}

		override
		public DeclarationList parseResource(String resourceName)
		{
			return parseSResource(resourceName);
		}
    }
}
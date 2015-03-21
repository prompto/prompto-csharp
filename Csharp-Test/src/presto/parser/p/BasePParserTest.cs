using presto.runtime;
using NUnit.Framework;
using presto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using presto.utils;

namespace presto.parser
{

	public abstract class BasePParserTest  : BaseParserTest
    {

		public DeclarationList parseString(String code)
		{
			return parsePString(code);
		}

		override
		public DeclarationList parseResource(String resourceName)
		{
			return parsePResource(resourceName);
		}
    }
}
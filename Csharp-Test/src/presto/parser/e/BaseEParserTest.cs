using presto.runtime;
using presto.grammar;
using System;
using System.IO;
using NUnit.Framework;
using presto.utils;
using System.Collections.Generic;

namespace presto.parser
{

	public abstract class BaseEParserTest : BaseParserTest
    {

		public DeclarationList parseString(String code)
		{
			return parseEString(code);
		}

		override
		public DeclarationList parseResource(String resourceName)
		{
			return parseEResource (resourceName);
		}

    }
}

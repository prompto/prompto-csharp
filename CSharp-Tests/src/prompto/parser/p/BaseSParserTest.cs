using prompto.runtime;
using NUnit.Framework;
using prompto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.parser
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
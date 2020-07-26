using prompto.runtime;
using prompto.declaration;
using System;
using System.IO;
using NUnit.Framework;
using prompto.utils;
using System.Collections.Generic;

namespace prompto.parser
{

	public abstract class BaseEParserTest : BaseParserTest
    {

		public DeclarationList parseString(String code)
		{
			return parseEString(code);
		}

		
		public override DeclarationList parseResource(String resourceName)
		{
			return parseEResource (resourceName);
		}

    }
}

using prompto.runtime;
using NUnit.Framework;
using prompto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.parser
{

	public abstract class BaseOParserTest : BaseParserTest
    {

        public DeclarationList parseString(String code)
        {
			return parseOString(code);
       }

		override
        public DeclarationList parseResource(String resourceName)
        {
			return parseOResource(resourceName);
        }
			
    }
}
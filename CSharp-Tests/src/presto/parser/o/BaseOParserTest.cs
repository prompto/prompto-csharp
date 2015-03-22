using presto.runtime;
using NUnit.Framework;
using presto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using presto.utils;

namespace presto.parser
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
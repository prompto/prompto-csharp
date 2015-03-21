using presto.runtime;
using NUnit.Framework;
using presto.grammar;
using System;
using System.IO;
using System.Collections.Generic;
using presto.utils;

namespace presto.parser
{

    public abstract class BasePParserTest
    {

		static char SEP = System.IO.Path.DirectorySeparatorChar;

        protected Context context;

        public DeclarationList parseString(String code)
        {
			context = Context.newGlobalContext();
			PCleverParser parser = new PCleverParser(code);
            return parser.parse_declaration_list();
        }

        public DeclarationList parseResource(String resourceName)
        {
			context = Context.newGlobalContext();
            Stream input = OpenResource(resourceName);
            try
            {
				PCleverParser parser = new PCleverParser(input);
                return parser.parse_declaration_list();
            }
            finally
            {
                input.Close();
            }
        }

        private Stream OpenResource(String resourceName)
        {
            String resourceDir = Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
			String fullPath = resourceDir + SEP + "Test" + SEP + "resources" + SEP + resourceName;
			Assert.IsTrue(File.Exists(fullPath), "resource not found:" + fullPath);
			return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }

        protected void loadResource(String resourceName)
        {
            DeclarationList stmts = parseResource(resourceName);
			Assert.IsNotNull (stmts);
			stmts.register(context);
            stmts.check(context);
        }

        protected void runResource(String resourceName)
        {
            loadResource(resourceName);
            Interpreter.interpretMainNoArgs(context);
        }

        protected void runResource(String resourceName, String methodName, String cmdLineArgs)
        {
            loadResource(resourceName);
            Interpreter.interpret(context, methodName, cmdLineArgs);
        }

        protected void CheckOutput(String resourceName)
        {
            runResource(resourceName);
            String read = Out.read();
            List<String> expected = ReadExpected(resourceName);
            if (expected.Count == 1)
                Assert.AreEqual(expected[0], read);
            else
            {
                foreach (String ex in expected)
                {
                    if (ex.Equals(read))
                        return;
                }
                Assert.AreEqual(expected[0], read);
            }
        }

        private List<String> ReadExpected(String resourceName)
        {
            resourceName = resourceName.Replace(".p", ".txt");
            Stream input = OpenResource(resourceName);
            try
            {
                List<String> expected = new List<String>();
                StreamReader reader = new StreamReader(input);
                for (; ; )
                {
                    String read = reader.ReadLine();
                    if (read == null || read.Length == 0)
                        return expected;
                    expected.Add(read);
                }
            }
            finally
            {
                input.Close();
            }

        }
    }
}
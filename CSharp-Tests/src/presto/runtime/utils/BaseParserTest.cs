using presto.runtime;
using presto.grammar;
using System;
using System.IO;
using NUnit.Framework;
using presto.utils;
using System.Collections.Generic;

namespace presto.parser
{

	public abstract class BaseParserTest
	{

		static char SEP = System.IO.Path.DirectorySeparatorChar;

		protected Context context;

		public DeclarationList parseEString (String code)
		{
			context = Context.newGlobalContext ();
			ECleverParser parser = new ECleverParser (code);
			return parser.parse_declaration_list ();
		}

		public DeclarationList parsePString (String code)
		{
			context = Context.newGlobalContext ();
			SCleverParser parser = new SCleverParser (code);
			return parser.parse_declaration_list ();
		}

		public DeclarationList parseOString (String code)
		{
			context = Context.newGlobalContext ();
			OCleverParser parser = new OCleverParser (code);
			return parser.parse_declaration_list ();
		}

		public DeclarationList parseEResource (String resourceName)
		{
			context = Context.newGlobalContext ();
			Stream input = OpenResource (resourceName);
			try {
				ECleverParser parser = new ECleverParser (input);
				return parser.parse_declaration_list ();
			} finally {
				input.Close ();
			}
		}

		public DeclarationList parseOResource (String resourceName)
		{
			context = Context.newGlobalContext ();
			Stream input = OpenResource (resourceName);
			try {
				OCleverParser parser = new OCleverParser (input);
				return parser.parse_declaration_list ();
			} finally {
				input.Close ();
			}
		}

		public DeclarationList parseSResource (String resourceName)
		{
			context = Context.newGlobalContext ();
			Stream input = OpenResource (resourceName);
			try {
				SCleverParser parser = new SCleverParser (input);
				return parser.parse_declaration_list ();
			} finally {
				input.Close ();
			}
		}

		public abstract DeclarationList parseResource (String resourceName);

		private Stream OpenResource (String resourceName)
		{
			String resourceDir = Path.GetDirectoryName (Path.GetDirectoryName (
				                     Path.GetDirectoryName (Path.GetDirectoryName (Directory.GetCurrentDirectory ()))));
			String fullPath = resourceDir + SEP + "presto-tests" + SEP + "Tests" + SEP + "resources" + SEP + resourceName;
			Assert.IsTrue (File.Exists (fullPath), "resource not found:" + fullPath);
			return new FileStream (fullPath, FileMode.Open, FileAccess.Read);
		}

		protected void loadResource (String resourceName)
		{
			DeclarationList stmts = parseResource (resourceName);
			Assert.IsNotNull (stmts);
			stmts.register (context);
			stmts.check (context);
		}

		protected void runResource (String resourceName)
		{
			loadResource (resourceName);
			if(context.hasTests())
				Interpreter.interpretTests(context);
			else
				Interpreter.interpretMainNoArgs(context);
		}

		protected void runResource (String resourceName, String methodName, String cmdLineArgs)
		{
			loadResource (resourceName);
			Interpreter.interpret (context, methodName, cmdLineArgs);
		}

		protected void CheckOutput (String resourceName)
		{
			runResource (resourceName);
			String read = Out.read ();
			List<String> expected = ReadExpected (resourceName);
			if (expected.Count == 1)
				Assert.AreEqual (expected [0], read);
			else {
				foreach (String ex in expected) {
					if (ex.Equals (read))
						return;
				}
				Assert.AreEqual (expected [0], read);
			}
		}

		private List<String> ReadExpected (String resourceName)
		{
			int idx = resourceName.LastIndexOf (".");
			resourceName = resourceName.Substring(0, idx) + ".txt";
			Stream input = OpenResource (resourceName);
			try {
				List<String> expected = new List<String> ();
				StreamReader reader = new StreamReader (input);
				for (; ;) {
					String read = reader.ReadLine ();
					if (read == null || read.Length == 0)
						return expected;
					expected.Add (read);
				}
			} finally {
				input.Close ();
			}

		}

		private String getResourceAsString(String resourceName) {
			Stream stream = OpenResource(resourceName);
			StreamReader reader = new StreamReader(stream);
			String result = reader.ReadToEnd ();
			reader.Close ();
			stream.Close();
			return result;
		}

		public static void assertEquivalent(String expected, String actual) {
			expected = removeWhitespace(expected).Replace("modulo","%");
			actual = removeWhitespace(actual).Replace("modulo","%");
			Assert.AreEqual(expected, actual);
		}

		private static String removeWhitespace(String s) {
			return s.Replace(" ", "").Replace("\t", "").Replace("\n", "");
		}

		public void compareResourceEOE(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse e source code
			DeclarationList dle = parseEString(expected);
			context = Context.newGlobalContext();
			dle.register(context);
			// rewrite as o
			CodeWriter writer = new CodeWriter(Dialect.O, context);
			dle.ToDialect(writer);
			String o = writer.ToString();
			// Console.WriteLine(o);
			// parse o source code
			DeclarationList dlo = parseOString(o);
			context = Context.newGlobalContext();
			dlo.register(context);
			// rewrite as e
			writer = new CodeWriter(Dialect.E, context);
			dlo.ToDialect(writer);
			String actual = writer.ToString();
			// Console.WriteLine(actual);
			// ensure equivalent
			assertEquivalent(expected, actual);
		}

		public void compareResourceESE(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse e source code
			DeclarationList dle = parseEString(expected);
			context = Context.newGlobalContext();
			dle.register(context);
			// rewrite as p
			CodeWriter writer = new CodeWriter(Dialect.S, context);
			dle.ToDialect(writer);
			String p = writer.ToString();
			// Console.WriteLine(p);
			// parse p source code
			DeclarationList dlp = parsePString(p);
			context = Context.newGlobalContext();
			dlp.register(context);
			// rewrite as e
			writer = new CodeWriter(Dialect.E, context);
			dlp.ToDialect(writer);
			String actual = writer.ToString();
			// Console.WriteLine(actual);
			// ensure equivalent
			assertEquivalent(expected, actual);
		}

		public void compareResourceOEO(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse o source code
			DeclarationList dlo = parseOString(expected);
			context = Context.newGlobalContext();
			dlo.register(context);
			// rewrite as e
			CodeWriter writer = new CodeWriter(Dialect.E, context);
			dlo.ToDialect(writer);
			String e = writer.ToString();
			// Console.WriteLine(e);
			// parse e source code
			DeclarationList dle = parseEString(e);
			context = Context.newGlobalContext();
			dle.register(context);
			// rewrite as o
			writer = new CodeWriter(Dialect.O, context);
			dle.ToDialect(writer);
			String actual = writer.ToString();
			// Console.WriteLine(actual);
			// ensure equivalent
			assertEquivalent(expected, actual);
		}

		public void compareResourceOSO(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse o source code
			DeclarationList dlo = parseOString(expected);
			context = Context.newGlobalContext();
			dlo.register(context);
			// rewrite as p
			CodeWriter writer = new CodeWriter(Dialect.S, context);
			dlo.ToDialect(writer);
			String p = writer.ToString();
			// Console.WriteLine(p);
			// parse p source code
			DeclarationList dlp = parsePString(p);
			context = Context.newGlobalContext();
			dlp.register(context);
			// rewrite as o
			writer = new CodeWriter(Dialect.O, context);
			dlp.ToDialect(writer);
			String actual = writer.ToString();
			// Console.WriteLine(actual);
			// ensure equivalent
			assertEquivalent(expected, actual);
		}

	}

}
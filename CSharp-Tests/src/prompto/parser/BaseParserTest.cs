using prompto.runtime;
using prompto.grammar;
using System;
using System.IO;
using NUnit.Framework;
using prompto.utils;
using System.Collections.Generic;
using prompto.declaration;
using prompto.store;
using Antlr4.Runtime;
using prompto.memstore;

namespace prompto.parser
{

	public abstract class BaseParserTest
	{

		static char SEP = System.IO.Path.DirectorySeparatorChar;

		protected Context coreContext;
		protected Context context;

		public void LoadDependency(String libraryName)
		{
			if(coreContext==null)
				coreContext = Context.newGlobalContext();
			DeclarationList allStmts = null;
			List<String> files = ListLibraryFiles(libraryName);
			foreach(String file in files) {
				String resourceName = libraryName + "/" + Path.GetFileName(file);
				DeclarationList stmts = parseResource(resourceName);
				if (allStmts == null)
					allStmts = stmts;
				else
					allStmts.AddRange (stmts);
			}
			allStmts.register(coreContext);
		}

		public List<String> ListLibraryFiles(String libraryName) {
			String currentDir = TestContext.CurrentContext.TestDirectory;
			String parentDir = currentDir.Substring(0, currentDir.IndexOf("CSharp-Tests"));
			String libsDir = parentDir + SEP + "prompto-libraries" + SEP;
			List<String> files = new List<String> ();
			foreach(String file in Directory.GetFiles(libsDir + libraryName)) {
				if(file.EndsWith(".pec") || file.EndsWith(".poc") || file.EndsWith(".psc"))
					files.Add(file);
			}
			return files;
		}

		protected void CheckTests(String resource) {
			DeclarationList stmts = parseResource(resource);
			foreach(IDeclaration decl in stmts) {
				if(!(decl is TestMethodDeclaration))
					continue;
				Out.reset();
				Interpreter.InterpretTest(coreContext, decl.GetName());
				String expected = decl.GetName() + " test successful";
				String read = Out.read();
				Assert.AreEqual(expected, read);
			}
		}

		class FatalErrorStrategy : DefaultErrorStrategy {
			
			protected override void ReportUnwantedToken (Parser recognizer)
			{
				base.ReportUnwantedToken (recognizer);
				// throw new Exception();
			}
		}

		public DeclarationList parseEString (String code)
		{
			context = Context.newGlobalContext ();
			ECleverParser parser = new ECleverParser (code);
			parser.ErrorHandler = new FatalErrorStrategy ();
			return parser.parse_declaration_list ();
		}

		public DeclarationList parseMString (String code)
		{
			context = Context.newGlobalContext ();
			MCleverParser parser = new MCleverParser (code);
			parser.ErrorHandler = new FatalErrorStrategy ();
			return parser.parse_declaration_list ();
		}

		public DeclarationList parseOString (String code)
		{
			context = Context.newGlobalContext ();
			OCleverParser parser = new OCleverParser (code);
			parser.ErrorHandler = new FatalErrorStrategy ();
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

		public DeclarationList parseMResource (String resourceName)
		{
			context = Context.newGlobalContext ();
			Stream input = OpenResource (resourceName);
			try {
				MCleverParser parser = new MCleverParser (input);
				return parser.parse_declaration_list ();
			} finally {
				input.Close ();
			}
		}

		public abstract DeclarationList parseResource (String resourceName);

		private Stream OpenResource (String resourceName)
		{
			resourceName = resourceName.Replace ('/', SEP);
			String currentDir = TestContext.CurrentContext.TestDirectory;
			String parentDir = currentDir.Substring(0, currentDir.IndexOf("CSharp-Tests"));
			String fullPath = parentDir + SEP + "prompto-tests" + SEP + "Tests" + SEP + "resources" + SEP + resourceName;
			if(!File.Exists (fullPath))
				fullPath = parentDir + SEP + "prompto-libraries" + SEP + resourceName;
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
				Interpreter.InterpretTests(context);
			else
				Interpreter.InterpretMainNoArgs(context);
		}

		protected void runResource (String resourceName, String methodName, String cmdLineArgs)
		{
			loadResource (resourceName);
			Interpreter.Interpret (context, methodName, cmdLineArgs);
		}

		protected void CheckOutput (String resourceName)
		{
			DataStore.Instance = new MemStore ();
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
			int idx = resourceName.LastIndexOf ('.');
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

		public void compareResourceEME(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse e source code
			DeclarationList dle = parseEString(expected);
			context = Context.newGlobalContext();
			dle.register(context);
			// rewrite as p
			CodeWriter writer = new CodeWriter(Dialect.M, context);
			dle.ToDialect(writer);
			String m = writer.ToString();
			// Console.WriteLine(p);
			// parse m source code
			DeclarationList dlp = parseMString(m);
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

		public void compareResourceOMO(String resourceName) {
			String expected = getResourceAsString(resourceName);
			// Console.WriteLine(expected);
			// parse o source code
			DeclarationList dlo = parseOString(expected);
			context = Context.newGlobalContext();
			dlo.register(context);
			// rewrite as p
			CodeWriter writer = new CodeWriter(Dialect.M, context);
			dlo.ToDialect(writer);
			String m = writer.ToString();
			// Console.WriteLine(p);
			// parse m source code
			DeclarationList dlp = parseMString(m);
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
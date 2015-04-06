using presto.parser;
using System.Threading;
using NUnit.Framework;
using presto.utils;
using System;
using presto.runtime;
using presto.grammar;
using System.Collections;
using System.Collections.Generic;
using presto.declaration;

namespace presto.debug
{

    [TestFixture]
    public class TestDebugger : BaseEParserTest
    {
		static readonly int MAIN_LINE = 1;
		static readonly int LEVEL_1_LINE = 5;
		static readonly int LEVEL_2_LINE = 9;

		protected Thread thread; // in debug mode
        protected Debugger debugger;
        protected Context debugged;

        [SetUp]
        public void before()
        {
            Out.init();
        }

        [TearDown]
        public void after()
        {
            Out.restore();
        }

        void interpret()
        {
            Interpreter.interpretMainNoArgs(debugged);
        }

        protected void debugResource(String resourceName)
        {
            loadResource(resourceName);
            debugger = new Debugger();
            debugged = context.newLocalContext();
            debugged.setDebugger(debugger);
            thread = new Thread(interpret);
        }

        void waitBlocked()
        {
			do {
				Thread.Sleep (1);
			} while (thread.ThreadState != ThreadState.WaitSleepJoin);
        }

		[Test]
        public void testStackNoDebug()
        {
            runResource("debug/stack.pec");
            Assert.AreEqual("test123-ok", Out.read());
        }

		[Test]
        public void testResume()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }

		[Test]
        public void testStepOver()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(MAIN_LINE + 1, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(MAIN_LINE + 2, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }

		[Test]
        public void testStepInto()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(MAIN_LINE + 1, debugger.getLine());
            debugger.stepInto(); // printLevel1
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 1, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }

		[Test]
        public void testSilentStepInto()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(MAIN_LINE + 1, debugger.getLine());
            debugger.stepInto(); // printLevel1
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 1, debugger.getLine());
            debugger.stepInto(); // value = value + "1", should step over
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 2, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }


		[Test]
        public void testStepOut()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(MAIN_LINE + 1, debugger.getLine());
            debugger.stepInto(); // printLevel1
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 1, debugger.getLine());
            debugger.stepOver();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 2, debugger.getLine());
            debugger.stepInto(); // printLevel2
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_2_LINE, debugger.getLine());
            debugger.stepOut(); // printLevel1
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_1_LINE + 2, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }

		[Test]
        public void testBreakpoint()
        {
            debugResource("debug/stack.pec");
            thread.Start();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
            Assert.AreEqual(MAIN_LINE, debugger.getLine());
            MethodDeclarationMap mdm = context.getRegisteredDeclaration<MethodDeclarationMap>("printLevel2");
            IEnumerator<IMethodDeclaration> emd = mdm.Values.GetEnumerator();
            emd.MoveNext();
            ConcreteMethodDeclaration cmd = (ConcreteMethodDeclaration)emd.Current;
            ISection section = cmd.getStatements()[0];
			Assert.AreEqual(LEVEL_2_LINE + 1, section.Start.Line);
            section.Breakpoint = true;
            debugger.resume();
            waitBlocked();
            Assert.AreEqual(Status.SUSPENDED, debugger.getStatus());
			Assert.AreEqual(LEVEL_2_LINE + 1, debugger.getLine());
            debugger.resume();
            thread.Join();
            Assert.AreEqual("test123-ok", Out.read());
        }

    }

}

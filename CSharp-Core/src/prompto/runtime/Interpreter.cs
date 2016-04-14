using prompto.grammar;
using System;
using System.Collections.Generic;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.declaration;
using prompto.expression;
using prompto.statement;
using prompto.type;
using prompto.literal;
using prompto.argument;


namespace prompto.runtime {


    public class Interpreter
    {

        private static IType argsType = new DictType(TextType.Instance);

        private Interpreter()
        {
        }

		public static void InterpretTests(Context context) {
			foreach(TestMethodDeclaration test in context.getTests()) {
				Context local = context.newLocalContext();
				test.interpret(local);
			}

		}

		public static void InterpretTest(Context context, String testName) {
			TestMethodDeclaration test = context.getTest(testName);
			Context local = context.newLocalContext();
			test.interpret(local);
		}

        public static void InterpretMainNoArgs(Context context)
        {
            Interpret(context, "main", "");
        }

        public static void Interpret(Context context, String methodName, String cmdLineArgs)
        {
            try
            {
                IMethodDeclaration method = locateMethod(context, methodName, cmdLineArgs);
                ArgumentAssignmentList assignments = buildAssignments(method, cmdLineArgs);
                MethodCall call = new MethodCall(new MethodSelector(methodName), assignments);
                call.interpret(context);
            }
            finally
            {
                context.fireTerminated();
            }
        }

        private static ArgumentAssignmentList buildAssignments(IMethodDeclaration method, String cmdLineArgs)
        {
            ArgumentAssignmentList assignments = new ArgumentAssignmentList();
            if (method.getArguments().Count == 1)
            {
                String name = method.getArguments()[0].GetName();
                IExpression value = parseCmdLineArgs(cmdLineArgs);
                assignments.Add(new ArgumentAssignment(new UnresolvedArgument(name), value));
            }
            return assignments;
        }

        private static IExpression parseCmdLineArgs(String cmdLineArgs)
        {
            try
            {
                Dictionary<Text, IValue> args = null; // CmdLineParser.parse(cmdLineArgs);
                Dict dict = new Dict(TextType.Instance, args);
				return new ExpressionValue(argsType, dict);
            }
            catch (Exception )
            {
                // TODO
                return new DictLiteral();
            }
        }

        private static IMethodDeclaration locateMethod(Context context, String methodName, String cmdLineArgs)
        {
            MethodDeclarationMap map = context.getRegisteredDeclaration<MethodDeclarationMap>(methodName);
            if (map == null)
                throw new SyntaxError("Could not find a \"" + methodName + "\" method.");
            return locateMethod(map, cmdLineArgs);
        }

        private static IMethodDeclaration locateMethod(MethodDeclarationMap map, String cmdLineArgs)
        {
            if(cmdLineArgs==null)
                return locateMethod(map);
            else
                return locateMethod(map, new DictType(TextType.Instance)); 
        }

        private static IMethodDeclaration locateMethod(MethodDeclarationMap map, params IType[] argTypes) {
           // try exact match first
           foreach(IMethodDeclaration method in map.Values) {
               if(identicalArguments(method.getArguments(), argTypes))
                   return method;
           }
           // match Text{} argument, will pass null 
           if(argTypes.Length==0) foreach(IMethodDeclaration method in map.Values) {
               if(isSingleTextDictArgument(method.getArguments()))
                   return method;
           }
           // match no argument, will ignore options
           foreach(IMethodDeclaration method in map.Values) {
               if(method.getArguments().Count==0)
                   return method;
           }
			throw new SyntaxError("Could not find a compatible \"" + map.GetName() + "\" method.");
       } 

        private static bool isSingleTextDictArgument(ArgumentList arguments)
        {
            if (arguments.Count != 1)
                return false;
            IArgument arg = arguments[0];
            if (!(arg is ITypedArgument))
                return false;
            return ((ITypedArgument)arg).getType().Equals(argsType);
        }

        private static bool identicalArguments(ArgumentList arguments, IType[] argTypes)
        {
            if (arguments.Count != argTypes.Length)
                return false;
            int idx = 0;
            foreach (IArgument argument in arguments)
            {
                if (!(argument is ITypedArgument))
                    return false;
                IType argType = argTypes[idx++];
                if (!argType.Equals(((ITypedArgument)argument).getType()))
                    return false;
            }
            return true;
        }

    }
}

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
using prompto.param;


namespace prompto.runtime
{


    public class Interpreter
    {

        private static IType argsType = new DictType(TextType.Instance);

        private Interpreter()
        {
        }

        public static void InterpretTests(Context context)
        {
            foreach (TestMethodDeclaration test in context.getTests())
            {
                Context local = context.newLocalContext();
                test.interpret(local);
            }

        }

        public static void InterpretTest(Context context, String testName)
        {
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
                ArgumentList arguments = buildArguments(method, cmdLineArgs);
                MethodCall call = new MethodCall(new MethodSelector(methodName), arguments);
                call.interpret(context);
            }
            finally
            {
                context.fireTerminated();
            }
        }

        private static ArgumentList buildArguments(IMethodDeclaration method, String cmdLineArgs)
        {
            ArgumentList arguments = new ArgumentList();
            if (method.getParameters().Count == 1)
            {
                String name = method.getParameters()[0].GetName();
                IExpression value = parseCmdLineArgs(cmdLineArgs);
                arguments.Add(new Argument(new UnresolvedParameter(name), value));
            }
            return arguments;
        }

        private static IExpression parseCmdLineArgs(String cmdLineArgs)
        {
            try
            {
                Dictionary<String, String> args = CmdLineParser.parse(cmdLineArgs);
                Dictionary<Text, IValue> valueArgs = new Dictionary<Text, IValue>();
                foreach (String key in args.Keys)
                {
                    valueArgs[new Text(key)] = new Text(args[key]);
                }
                Dict dict = new Dict(TextType.Instance, false, valueArgs);
                return new ValueExpression(argsType, dict);
            }
            catch (Exception)
            {
                // TODO
                return new DictLiteral(false);
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
            if (cmdLineArgs == null)
                return locateMethod(map);
            else
                return locateMethod(map, new DictType(TextType.Instance));
        }

        private static IMethodDeclaration locateMethod(MethodDeclarationMap map, params IType[] argTypes)
        {
            // try exact match first
            foreach (IMethodDeclaration method in map.Values)
            {
                if (identicalArguments(method.getParameters(), argTypes))
                    return method;
            }
            // match Text{} argument, will pass null 
            if (argTypes.Length == 0) foreach (IMethodDeclaration method in map.Values)
                {
                    if (isSingleTextDictArgument(method.getParameters()))
                        return method;
                }
            // match no argument, will ignore options
            foreach (IMethodDeclaration method in map.Values)
            {
                if (method.getParameters().Count == 0)
                    return method;
            }
            throw new SyntaxError("Could not find a compatible \"" + map.GetName() + "\" method.");
        }

        private static bool isSingleTextDictArgument(ParameterList arguments)
        {
            if (arguments.Count != 1)
                return false;
            IParameter arg = arguments[0];
            if (!(arg is ITypedParameter))
                return false;
            return ((ITypedParameter)arg).getType().Equals(argsType);
        }

        private static bool identicalArguments(ParameterList arguments, IType[] argTypes)
        {
            if (arguments.Count != argTypes.Length)
                return false;
            int idx = 0;
            foreach (IParameter argument in arguments)
            {
                if (!(argument is ITypedParameter))
                    return false;
                IType argType = argTypes[idx++];
                if (!argType.Equals(((ITypedParameter)argument).getType()))
                    return false;
            }
            return true;
        }

    }
}

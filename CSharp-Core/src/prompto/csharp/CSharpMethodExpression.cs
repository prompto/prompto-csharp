
using prompto.runtime;
using System;
using System.Reflection;
using prompto.value;
using prompto.expression;
using prompto.type;
using prompto.declaration;
using prompto.utils;

namespace prompto.csharp
{

	public class CSharpMethodExpression : CSharpSelectorExpression
	{

		String name;
		CSharpExpressionList arguments;

		public CSharpMethodExpression (String name)
		{
			this.name = name;
			this.arguments = new CSharpExpressionList ();
		}

		public CSharpMethodExpression (string name, CSharpExpressionList arguments)
		{
			this.name = name;
			this.arguments = arguments != null ? arguments : new CSharpExpressionList ();
		}

		public void addArgument (CSharpExpression expression)
		{
			arguments.Add (expression);
		}

		override
    	public String ToString ()
		{
			return parent.ToString () + "." + name + "(" + arguments.ToString () + ")";
		}

		override
		public void ToDialect (CodeWriter writer)
		{
			parent.ToDialect (writer);
			writer.append ('.');
			writer.append (name);
			writer.append ('(');
			if (arguments != null)
				arguments.ToDialect (writer);
			writer.append (')');
		}

		override
    	public IType check (Context context)
		{
			MethodInfo method = findMethod (context);
			if (method == null)
				return null;
			else
				return new CSharpClassType (method.ReturnType);
		}

		override
    	public Object interpret (Context context)
		{
			Object instance = parent.interpret (context);
			if (instance is NativeInstance)
				instance = ((NativeInstance)instance).getInstance ();
			MethodInfo method = findMethod (context, instance);
			Object[] args = evaluate_arguments (context, method);
			Type klass = instance is Type ? (Type)instance : instance.GetType (); 
			if (instance == (Object)klass)
				instance = null;
			else if (instance is NativeInstance)
				instance = ((NativeInstance)instance).getInstance ();
			return method.Invoke (instance, args);
		}

		Object[] evaluate_arguments (Context context, MethodInfo method)
		{
			ParameterInfo[] parms = method.GetParameters ();
			Object[] args = new Object[arguments.Count];
			for (int i = 0; i < args.Length; i++)
				args [i] = evaluate_argument (context, arguments [i], parms [i]);
			return args;
		}

		Object evaluate_argument (Context context, CSharpExpression expression, ParameterInfo parm)
		{
			Object value = expression.interpret (context);
			if (value is IExpression)
				value = ((IExpression)value).interpret (context);
			if (value is IValue)
				value = ((IValue)value).ConvertTo (parm.ParameterType);
			return value;
		}

		bool validPrototype (MethodInfo method, Object[] args)
		{
			ParameterInfo[] parms = method.GetParameters ();
			if (parms.Length != args.Length)
				return false;
			for (int i = 0; i < parms.Length; i++) {
				if (!validArgument (parms [i].ParameterType, args [i]))
					return false;
			}
			return true;
		}

		bool validArgument (System.Type klass, Object value)
		{
			return value == null || klass.IsAssignableFrom (value.GetType ());
		}

		public MethodInfo findMethod (Context context)
		{
			IType type = parent.check (context);
			Type klass = null;
			if (type is CSharpClassType)
				klass = ((CSharpClassType)type).type;
			else if (type is CategoryType) {
				IDeclaration named = context.getRegisteredDeclaration<IDeclaration> (type.GetTypeName ());
				if (named is NativeCategoryDeclaration)
					klass = ((NativeCategoryDeclaration)named).getBoundClass (true);
			} else
				klass = type.ToCSharpType ();
			return findMethod (context, klass);
		}

		public MethodInfo findMethod(Context context, Object instance) 
		{
			if(instance is Type)
				return findMethod(context, (Type)instance);
			else
				return findMethod(context, instance.GetType());
		}

		public MethodInfo findMethod (Context context, Type klass)
		{
			if (klass == null)
				return null;
			MethodInfo[] methods = klass.GetMethods ();
			foreach (MethodInfo m in methods) {
				if (!name.Equals (m.Name))
					continue;
				if (validPrototype (context, m))
					return m;
			}
			return null; 
		}

		bool validPrototype (Context context, MethodInfo method)
		{
			ParameterInfo[] parms = method.GetParameters ();
			if (parms.Length != arguments.Count)
				return false;
			for (int i = 0; i < parms.Length; i++) {
				if (!validArgument (context, parms [i].ParameterType, arguments [i]))
					return false;
			}
			return true;
		}

		bool validArgument (Context context, Type klass, CSharpExpression argument)
		{
			Type type = argument.check (context).ToCSharpType ();
			return validArgument (klass, type);
		}

		bool validArgument (Type wanted, Type actual) {
			if (wanted.IsAssignableFrom (actual))
				return true;
			// work around IsAssignableFrom nightmare, where IList<Object> is not assignable from IList<String>
			// in our case, since values are immutable, it is safe to cast it to a broader type
			if (!wanted.IsGenericType)
				return false;
			Type wantedParent = wanted.GetGenericTypeDefinition ();
			if (!actual.IsGenericType)
				return false;
			Type actualParent = actual.GetGenericTypeDefinition ();
			if (!validArgument (wantedParent, actualParent))
				return false;
			Type[] wantedArgs = wanted.GetGenericArguments ();
			Type[] actualArgs = actual.GetGenericArguments ();
			if (wantedArgs.Length != actualArgs.Length)
				return false;
			for (int i = 0; i < wantedArgs.Length; i++) {
				if (!validArgument (wantedArgs [i], actualArgs [i]))
					return false;
			}
			return true;
		}		
	
	}

}
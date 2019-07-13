using prompto.grammar;
using prompto.utils;
using prompto.debug;
using prompto.error;
using prompto.parser;
using System.Collections.Generic;
using System;
using System.Text;
using prompto.value;
using prompto.declaration;
using prompto.expression;
using prompto.statement;
using prompto.type;

namespace prompto.runtime
{

	public class Context : IContext
	{

		public static Context newGlobalContext()
		{
			Context context = new Context();
			context.globals = context;
			context.calling = null;
			context.parent = null;
			context.debugger = null;
			return context;
		}

		Context globals;
		protected Context calling;
		Context parent; // for inner methods
		Debugger debugger;

		Dictionary<String, IDeclaration> declarations = new Dictionary<String, IDeclaration>();
		Dictionary<String, TestMethodDeclaration> tests = new Dictionary<String, TestMethodDeclaration>();
		protected Dictionary<String, INamed> instances = new Dictionary<String, INamed>();
		Dictionary<String, IValue> values = new Dictionary<String, IValue>();
		Dictionary<Type, NativeCategoryDeclaration> nativeBindings = new Dictionary<Type, NativeCategoryDeclaration>();

		protected Context()
		{
		}

		public bool isGlobalContext()
		{
			return this == globals;
		}

		public void setDebugger(Debugger debugger)
		{
			this.debugger = debugger;
		}

		public Debugger getDebugger()
		{
			return debugger;
		}

		override public String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			if (this != globals)
			{
				sb.Append("globals:");
				sb.Append(globals);
			}
			sb.Append(",calling:");
			sb.Append(calling);
			sb.Append(",parent:");
			sb.Append(parent);
			sb.Append(",declarations:");
			sb.Append(declarations);
			sb.Append(",instances:");
			sb.Append(instances);
			sb.Append(",values:");
			sb.Append(values);
			sb.Append("}");
			return sb.ToString();
		}

		public IContext getCallingContext()
		{
			return calling;
		}

		public virtual InstanceContext getClosestInstanceContext()
		{
			if (parent == null)
				return null;
			else
				return parent.getClosestInstanceContext();
		}

		public Context getParentMostContext()
		{
			if (parent == null)
				return this;
			else
				return parent.getParentMostContext();
		}

		public Context getParentContext()
		{
			return parent;
		}

		public void setParentContext(Context parent)
		{
			this.parent = parent;
		}

		public Context newResourceContext()
		{
			Context context = new ResourceContext();
			context.globals = this.globals;
			context.calling = this.calling;
			context.parent = this;
			context.debugger = this.debugger;
			return context;
		}

		public Context newLocalContext()
		{
			Context context = new Context();
			context.globals = this.globals;
			context.calling = this;
			context.parent = null;
			context.debugger = this.debugger;
			return context;
		}

		public Context newBuiltInContext(IValue value)
		{
			return initInstanceContext(new BuiltInContext(value), false);
		}

		public Context newInstanceContext(CategoryType type, bool isChild)
		{
			return initInstanceContext(new InstanceContext(type), isChild);
		}

		public Context newInstanceContext(IInstance instance, bool isChild)
		{
			return initInstanceContext(new InstanceContext(instance), isChild);
		}

		public Context newDocumentContext(Document document, bool isChild)
		{
			return initInstanceContext(new DocumentContext(document), isChild);
		}

		private Context initInstanceContext(Context context, bool isChild)
		{
			context.globals = this.globals;
			context.calling = isChild ? this.calling : this;
			context.parent = isChild ? this : null;
			context.debugger = this.debugger;
			return context;
		}

		public Context newChildContext()
		{
			Context context = new Context();
			context.globals = this.globals;
			context.calling = this.calling;
			context.parent = this;
			context.debugger = this.debugger;
			return context;
		}

		public AttributeDeclaration findAttribute(String name)
		{
			return getRegisteredDeclaration<AttributeDeclaration>(name);
		}

		public List<AttributeDeclaration> getAllAttributes()
		{
			if (globals != this)
				return globals.getAllAttributes();
			List<AttributeDeclaration> list = new List<AttributeDeclaration>();
			foreach (IDeclaration decl in declarations.Values)
			{
				if (decl is AttributeDeclaration)
					list.Add((AttributeDeclaration)decl);
			}
			return list;
		}

		public virtual INamed getRegistered(String name)
		{
			// resolve upwards, since local names override global ones
			IDeclaration actual;
			declarations.TryGetValue(name, out actual);
			if (actual != null)
				return actual;
			INamed actualName;
			instances.TryGetValue(name, out actualName);
			if (actualName != null)
				return actualName;
			if (parent != null)
				return parent.getRegistered(name);
			if (globals != this)
				return globals.getRegistered(name);
			return null;
		}

		public virtual T getRegisteredDeclaration<T>(String name) where T : IDeclaration
		{
			// resolve upwards, since local names override global ones
			IDeclaration actual;
			declarations.TryGetValue(name, out actual);
			if (actual == null && parent != null)
				actual = parent.getRegisteredDeclaration<T>(name);
			if (actual == null && globals != this)
				actual = globals.getRegisteredDeclaration<T>(name);
			if (actual != null)
				return TypeUtils.downcast<T>(actual);
			else
				return default(T);
		}

		public virtual T getLocalDeclaration<T>(String name) where T : IDeclaration
		{
			IDeclaration actual;
			declarations.TryGetValue(name, out actual);
			if (actual != null)
				return TypeUtils.downcast<T>(actual);
			else if (parent != null)
				return parent.getLocalDeclaration<T>(name);
			else
				return default(T);	
		}


		public void registerDeclaration(IDeclaration declaration)
		{
			INamed actual = getRegistered(declaration.GetName());
			if (actual != null && actual != declaration)
				throw new SyntaxError("Duplicate name: \"" + declaration.GetName() + "\"");
			declarations[declaration.GetName()] = declaration;
		}

		public void registerDeclaration(IMethodDeclaration declaration)
		{
			INamed actual = getRegistered(declaration.GetName());
			if (actual != null && !(actual is MethodDeclarationMap))
				throw new SyntaxError("Duplicate name: \"" + declaration.GetName() + "\"");
			if (actual == null)
			{
				actual = new MethodDeclarationMap(declaration.GetName());
				declarations[declaration.GetName()] = (MethodDeclarationMap)actual;
			}
			((MethodDeclarationMap)actual).register(declaration, this);
		}

		public void registerDeclaration(TestMethodDeclaration declaration)
		{
			if (tests.ContainsKey(declaration.GetName()))
				throw new SyntaxError("Duplicate test: \"" + declaration.GetName() + "\"");
			tests[declaration.GetName()] = declaration;
		}

		public bool hasTests()
		{
			return tests.Count > 0;
		}

		public Dictionary<String, TestMethodDeclaration>.ValueCollection getTests()
		{
			return tests.Values;
		}

		public TestMethodDeclaration getTest(String testName)
		{
			TestMethodDeclaration test;
			if (!tests.TryGetValue(testName, out test))
				return null;
			return test;
		}

		public T getRegisteredValue<T>(String name) where T : INamed
		{
			Context context = contextForValue(name);
			if (context == null)
				return default(T);
			else
				return context.readRegisteredValue<T>(name);
		}

		protected virtual T readRegisteredValue<T>(String name) where T : INamed
		{
			INamed actual = null;
			instances.TryGetValue(name, out actual);
			if (actual != null)
				return TypeUtils.downcast<T>(actual);
			else
				return default(T);
		}

		public void registerValue(INamed named)
		{
			registerValue(named, true);
		}

		public void registerValue(INamed named, bool checkDuplicate)
		{
			if (checkDuplicate)
			{
				// only explore current context
				if (instances.ContainsKey(named.GetName()))
					throw new SyntaxError("Duplicate name: \"" + named.GetName() + "\"");
			}
			instances[named.GetName()] = named;
		}

		public bool hasValue(String name)
		{
			return contextForValue(name) != null;
		}

		public IValue getValue(String name)
		{
			return getValue(name, () => null);
		}

		public IValue getValue(String name, Func<IValue> supplier)
		{
			Context context = contextForValue(name);
			if (context == null)
				throw new SyntaxError(name + " is not defined");
			return context.readValue(name, supplier);
		}


		protected virtual IValue readValue(String name, Func<IValue> supplier)
		{
			IValue value;
			if (!values.TryGetValue(name, out value))
			{
				value = supplier.Invoke();
				if (value != null)
					values[name] = value;
			}
			if(value==null)
				throw new SyntaxError(name + " has no value");
			if (value is LinkedValue)
				return ((LinkedValue)value).getContext().getValue(name);
			else
				return value;
		}

		public void setValue(String name, IValue value)
		{
			Context context = contextForValue(name);
			if (context == null)
				throw new SyntaxError(name + " is not defined");
			context.writeValue(name, value);
		}

		protected virtual void writeValue(String name, IValue value)
		{
			if (value is IExpression)
				value = ((IExpression)value).interpret(this);
			IValue current;
			values.TryGetValue(name, out current);
			if (current is LinkedValue)
				((LinkedValue)current).getContext().setValue(name, value);
			else
				values[name] = value;
		}

		public virtual Context contextForValue(String name)
		{
			// resolve upwards, since local names override global ones
			INamed actual;
			instances.TryGetValue(name, out actual);
			if (actual != null)
				return this;
			if (parent != null)
				return parent.contextForValue(name);
			if (globals != this)
				return globals.contextForValue(name);
			return null;
		}

		public void enterMethod(IDeclaration method)
		{
			if (debugger != null)
				debugger.enterMethod(this, method);
		}

		public void leaveMethod(IDeclaration method)
		{
			if (debugger != null)
				debugger.leaveMethod(this, method);
		}

		public void enterStatement(IStatement statement)
		{
			if (debugger != null)
				debugger.enterStatement(this, statement);
		}

		public void leaveStatement(IStatement statement)
		{
			if (debugger != null)
				debugger.leaveStatement(this, statement);
		}

		public void fireTerminated()
		{
			if (debugger != null)
				debugger.whenTerminated();
		}

		public ConcreteInstance loadSingleton(Context context, CategoryType type)
		{
			if (this == globals)
			{
				IValue value = null;
				values.TryGetValue(type.GetTypeName(), out value);
				if (value == null)
				{
					IDeclaration decl = declarations[type.GetTypeName()];
					if (!(decl is ConcreteCategoryDeclaration))
						throw new InternalError("No such singleton:" + type.GetTypeName());
					value = new ConcreteInstance(context, (ConcreteCategoryDeclaration)decl);
					((IInstance)value).setMutable(true); // a singleton is protected by "with x do", so always mutable in that context
					values[type.GetTypeName()] = value;
				}
				if (value is ConcreteInstance)
					return (ConcreteInstance)value;
				else
					throw new InternalError("Not a concrete instance:" + value.GetType().Name);
			}
			else
				return this.globals.loadSingleton(context, type);
		}

		public void registerNativeBinding(Type type, NativeCategoryDeclaration declaration)
		{
			if (this == globals)
				nativeBindings[type] = declaration;
			else
				globals.registerNativeBinding(type, declaration);
		}

		public NativeCategoryDeclaration getNativeBinding(Type type)
		{
			if (this == globals)
			{
				NativeCategoryDeclaration ncd;
				if (nativeBindings.TryGetValue(type, out ncd))
					return ncd;
				else
					return null;
			}
			else
				return globals.getNativeBinding(type);
		}

	}

	class ResourceContext : Context
	{

		internal ResourceContext()
		{
		}

	}

	public class BuiltInContext : Context
	{

		IValue value;


		internal BuiltInContext(IValue value)
		{
			this.value = value;
		}

		public IValue getValue()
		{
			return value;
		}
	}


	public class InstanceContext : Context
	{
		IInstance instance;
		CategoryType type;

		internal InstanceContext(IInstance instance)
		{
			this.instance = instance;
			this.type = instance.getType();
		}

		internal InstanceContext(CategoryType type)
		{
			this.type = type;
		}

		public override InstanceContext getClosestInstanceContext()
		{
			return this;
		}
	
		public IInstance getInstance()
		{
			return instance;
		}

		public CategoryType getInstanceType()
		{
			return type;
		}

		public override INamed getRegistered(string name)
		{
			INamed actual = base.getRegistered(name);
			if (actual != null)
				return actual;
			ConcreteCategoryDeclaration decl = getDeclaration();
			MethodDeclarationMap methods = decl.getMemberMethods(this, name);
			if (methods != null && methods.Count > 0)
				return methods;
			else if (decl.hasAttribute(this, name))
				return getRegisteredDeclaration<AttributeDeclaration>(name);
			else
         		return null;
		}

		public override T getRegisteredDeclaration<T>(string name)
		{
			if(typeof(T)==typeof(MethodDeclarationMap)) {
				ConcreteCategoryDeclaration decl = getDeclaration();
				if(decl!=null) {
					MethodDeclarationMap methods = decl.getMemberMethods(this, name);
					if(methods!=null && methods.Count>0)
						return TypeUtils.downcast<T>(methods);
				}
			}
			return base.getRegisteredDeclaration<T>(name);
		}

		protected override T readRegisteredValue<T>(String name)
		{
			INamed actual = null;
			instances.TryGetValue(name, out actual);
			// not very pure, but avoids a lot of complexity when registering a value
			if (actual == null)
			{
				AttributeDeclaration attr = getRegisteredDeclaration<AttributeDeclaration>(name);
				IType type = attr.getIType();
				actual = new Variable(name, type);
				instances[name] = actual;
			}
			return TypeUtils.downcast<T>(actual);
		}

		public override Context contextForValue(String name)
		{
			// params and variables have precedence over members
			// so first look in context values
			Context context = base.contextForValue(name);
			if (context != null)
				return context;
			ConcreteCategoryDeclaration decl = getDeclaration();
			if (decl.hasAttribute(this, name) || decl.hasMethod(this, name))
				return this;
			else
				return null;
		}

		private ConcreteCategoryDeclaration getDeclaration()
		{
			if (instance != null)
				return instance.getDeclaration();
			else
				return getRegisteredDeclaration<ConcreteCategoryDeclaration>(type.GetTypeName());
		}


		protected override IValue readValue(String name, Func<IValue> supplier)
		{
			ConcreteCategoryDeclaration decl = getDeclaration();
			if (decl.hasAttribute(this, name))
			{
				IValue value = instance.GetMember(calling, name, false);
				return value != null ? value : supplier.Invoke();
			}
			else if (decl.hasMethod(this, name))
			{
				IMethodDeclaration method = decl.getMemberMethods(this, name).GetFirst();
				return new ClosureValue(this, new MethodType(method));
			}
			else
				return supplier.Invoke();
		}


		protected override void writeValue(String name, IValue value)
		{
			if (value is IExpression)
				value = ((IExpression)value).interpret(this);
			instance.SetMember(calling, name, (IValue)value);
		}
	}

	public class DocumentContext : Context
	{

		Document document;

		internal DocumentContext(Document document)
		{
			this.document = document;
		}

		internal IValue getDocument()
		{
			return this.document;
		}

		public override Context contextForValue(String name)
		{
			// params and variables have precedence over members
			// so first look in context values
			Context context = base.contextForValue(name);
			if (context != null)
				return context;
			// since any name is valid in the context of a document
			// simply return this document context
			else
				return this;
		}

		protected override IValue readValue(String name, Func<IValue> supplier)
		{
			return document.GetMember(calling, name, false);
		}

		protected override void writeValue(String name, IValue value)
		{
			document.SetMember(calling, name, value);
		}

	}

	public class MethodDeclarationMap : Dictionary<String, IMethodDeclaration>, IDeclaration
	{

		String name;

		public MethodDeclarationMap(String name)
		{
			this.name = name;
		}

		public IMethodDeclaration ClosureOf
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public List<CommentStatement> Comments
		{
			get
			{
				throw new Exception("Should never get there!");
			}
			set
			{
				throw new Exception("Should never get there!");
			}
		}

		public List<Annotation> Annotations
		{
			get
			{
				throw new Exception("Should never get there!");
			}
			set
			{
				throw new Exception("Should never get there!");
			}
		}

		public void ToDialect(CodeWriter writer)
		{
			throw new Exception("Should never get there!");
		}

		public String GetName()
		{
			return name;
		}

		public IType check(Context context)
		{
			throw new Exception("Should never get there!");
		}

		public void register(Context context)
		{
			throw new Exception("Should never get there!");
		}

		public void register(IMethodDeclaration declaration, Context context)
		{
			String proto = declaration.getProto();
			if (this.ContainsKey(proto))
				throw new SyntaxError("Duplicate prototype for name: \"" + declaration.GetName() + "\"");
			this[proto] = declaration;
		}

		public void registerIfMissing(IMethodDeclaration declaration, Context context)
		{
			String proto = declaration.getProto();
			if (!this.ContainsKey(proto))
				this[proto] = declaration;
		}

		public IType GetIType(Context context)
		{
			throw new SyntaxError("Should never get there!");
		}

		public IMethodDeclaration GetFirst()
		{
			IEnumerator<IMethodDeclaration> en = this.Values.GetEnumerator();
			en.MoveNext();
			return en.Current;
		}

		public String Path
		{
			get { throw new Exception("Should never get there!"); }
		}

		public ILocation Start
		{
			get { throw new Exception("Should never get there!"); }
		}

		public ILocation End
		{
			get { throw new Exception("Should never get there!"); }
		}

		public Dialect Dialect
		{
			get { throw new Exception("Should never get there!"); }
		}

		public bool Breakpoint
		{
			get { throw new Exception("Should never get there!"); }
			set { throw new Exception("Should never get there!"); }
		}

	}
}
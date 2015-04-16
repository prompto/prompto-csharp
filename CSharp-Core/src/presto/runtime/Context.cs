using presto.grammar;
using presto.utils;
using presto.debug;
using presto.error;
using presto.parser;
using System.Collections.Generic;
using System;
using System.Text;
using presto.value;
using presto.declaration;
using presto.expression;
using presto.statement;
using presto.type;

namespace presto.runtime
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
		Dictionary<String,TestMethodDeclaration> tests = new Dictionary<String, TestMethodDeclaration>();
		protected Dictionary<String, INamed> instances = new Dictionary<String, INamed>();
		Dictionary<String, IValue> values = new Dictionary<String, IValue>();

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

		public Context newInstanceContext(IType type) 
		{
			return initInstanceContext(new InstanceContext(type));
		}

		public Context newInstanceContext(ConcreteInstance instance) 
		{
			return initInstanceContext(new InstanceContext(instance));
		}

		private Context initInstanceContext(InstanceContext context)
        {
            context.globals = this.globals;
            context.calling = this;
            context.parent = null;
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

        public INamed getRegistered(String name)
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

        public T getRegisteredDeclaration<T>(String name)
        {
            // resolve upwards, since local names override global ones
            IDeclaration actual;
            declarations.TryGetValue(name, out actual);
            if (actual == null && parent != null)
                actual = parent.getRegisteredDeclaration<IDeclaration>(name);
            if (actual == null && globals != this)
                actual = globals.getRegisteredDeclaration<IDeclaration>(name);
            if (actual != null)
                return presto.utils.Utils.downcast<T>(actual);
            else
                return default(T);
        }

        public void registerDeclaration(IDeclaration declaration)
        {
            INamed actual = getRegistered(declaration.getName());
            if (actual != null)
                throw new SyntaxError("Duplicate name: \"" + declaration.getName() + "\"");
            declarations[declaration.getName()] = declaration;
        }

        public void registerDeclaration(IMethodDeclaration declaration)
        {
            INamed actual = getRegistered(declaration.getName());
            if (actual != null && !(actual is MethodDeclarationMap))
                throw new SyntaxError("Duplicate name: \"" + declaration.getName() + "\"");
            if (actual == null)
            {
                actual = new MethodDeclarationMap(declaration.getName());
                declarations[declaration.getName()] = (MethodDeclarationMap)actual;
            }
            ((MethodDeclarationMap)actual).register(declaration, this);
        }

		public void registerDeclaration(TestMethodDeclaration declaration) {
			if(tests.ContainsKey(declaration.getName()))
				throw new SyntaxError("Duplicate test: \"" + declaration.getName() + "\"");
			tests[declaration.getName()] = declaration;
		}

		public bool hasTests() {
			return tests.Count>0;
		}

		public Dictionary<String,TestMethodDeclaration>.ValueCollection getTests() {
			return tests.Values;
		}

		public T getRegisteredValue<T>(String name) where T : INamed
        {
			Context context = contextForValue(name);
			if(context==null)
				return default(T);
			else
				return context.readRegisteredValue<T>(name);
       }

		protected virtual T readRegisteredValue<T>(String name) where T : INamed
		{
			INamed actual = null;
			instances.TryGetValue(name, out actual);
			if(actual!=null)
				return Utils.downcast<T>(actual);
			else
				return default(T);
		}

        public void registerValue(INamed named)
		{
			registerValue(named, true);
		}

		public void registerValue(INamed named, bool checkDuplicate) {
			if (checkDuplicate) {
				// only explore current context
				if(instances.ContainsKey(named.getName()))
					throw new SyntaxError ("Duplicate name: \"" + named.getName () + "\"");
			}
			instances[named.getName()] = named;
        }

        public IValue getValue(String name)
        {
            Context context = contextForValue(name);
            if (context == null)
                throw new SyntaxError(name + " is not defined");
            return context.readValue(name);
        }


		protected virtual IValue readValue(String name)
        {
			IValue value;
			if(!values.TryGetValue(name, out value))
                throw new SyntaxError(name + " has no value");
			if(value is LinkedValue)
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
			if(current is LinkedValue)
				((LinkedValue)current).getContext().setValue(name, value);
			else
				values[name] = value;
        }

        protected virtual Context contextForValue(String name)
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

		public ConcreteInstance loadSingleton(CategoryType type) {
			if(this==globals) {
				IValue value = null;
				values.TryGetValue(type.getName(), out value);
				if(value==null) {
					IDeclaration decl = declarations[type.getName()];
					if(!(decl is ConcreteCategoryDeclaration))
						throw new InternalError("No such singleton:" + type.getName());
					value = new ConcreteInstance((ConcreteCategoryDeclaration)decl);
					((IInstance)value).setMutable(true); // a singleton is protected by "with x do", so always mutable in that context
					values[type.getName()] = value;
				}
				if(value is ConcreteInstance)
					return (ConcreteInstance)value;
				else
					throw new InternalError("Not a concrete instance:" + value.GetType().Name);
			} else
				return this.globals.loadSingleton(type);
		}

    }

    class ResourceContext : Context
    {

		internal ResourceContext()
        {
        }

    }

	public class InstanceContext : Context
    {
        ConcreteInstance instance;
		IType type;

        internal InstanceContext(ConcreteInstance instance)
        {
            this.instance = instance;
			this.type = instance.getType();
        }

		internal InstanceContext(IType type) {
			this.type = type;
		}

		public ConcreteInstance getInstance() {
			return instance;
		}

		public IType getInstanceType() {
			return type;
		}

		protected override T readRegisteredValue<T>(String name)
		{
			INamed actual = null;
			instances.TryGetValue(name, out actual);
			// not very pure, but avoids a lot of complexity when registering a value
			if(actual==null) {
				AttributeDeclaration attr = getRegisteredDeclaration<AttributeDeclaration>(name);
				IType type = attr.getType();
				actual = new Variable(name, type);
				instances[name] = actual;
			}
			return Utils.downcast<T>(actual);
		}

		protected override Context contextForValue(String name)
        {
            // params and variables have precedence over members
            // so first look in context values
            Context context = base.contextForValue(name);
            if (context != null)
                return context;
            else if (getDeclaration().hasAttribute(this, name))
                return this;
            else
                return null;
        }

		private ConcreteCategoryDeclaration getDeclaration() {
			if(instance!=null)
				return instance.getDeclaration();
			else
				return getRegisteredDeclaration<ConcreteCategoryDeclaration>(type.getName());
		}


		protected override IValue readValue(String name)
        {
            return instance.GetMember(calling, name);
        }

        
		protected override void writeValue(String name, IValue value)
        {
            if (value is IExpression)
                value = ((IExpression)value).interpret(this);
            instance.set(calling, name, (IValue)value);
        }
    }

    public class MethodDeclarationMap : Dictionary<String, IMethodDeclaration>, IDeclaration
    {

        String name;

        public MethodDeclarationMap(String name)
        {
            this.name = name;
        }

        public void ToDialect(CodeWriter writer)
        {
            // should never get there
        }

        public String getName()
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
            String proto = declaration.getProto(context);
            if (this.ContainsKey(proto))
                throw new SyntaxError("Duplicate prototype for name: \"" + declaration.getName() + "\"");
            this[proto] = declaration;
        }

        public void registerIfMissing(IMethodDeclaration declaration, Context context)
        {
            String proto = declaration.getProto(context);
            if (!this.ContainsKey(proto))
                this[proto] = declaration;
        }

        public IType GetType(Context context)
        {
            throw new SyntaxError("Should never get there!");
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
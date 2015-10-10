using prompto.error;
using System;
using prompto.grammar;
using System.Collections.Generic;
using System.Reflection;
using prompto.value;
using prompto.csharp;
using prompto.declaration;
using prompto.type;
using prompto.runtime;
using prompto.expression;
using System.Threading;

namespace prompto.value
{

    public class NativeInstance : BaseValue, IInstance
    {

		static BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic 
			| BindingFlags.Static | BindingFlags.Instance
			| BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy;
		
        NativeCategoryDeclaration declaration;
        protected Object instance;
		bool mutable = false;

        public NativeInstance(NativeCategoryDeclaration declaration)
			: base(new CategoryType(declaration.GetName()))
		{
            this.declaration = declaration;
            this.instance = makeInstance();
        }

		public NativeInstance(NativeCategoryDeclaration declaration, Object instance)
			: base(new CategoryType(declaration.GetName()))
		{
			this.declaration = declaration;
			this.instance = instance;
		}


		public bool setMutable(bool set)
		{
			bool result = mutable;
			mutable = set;
			return result;
		}

		public override bool IsMutable()
		{
			return mutable;
		}

		public ConcreteCategoryDeclaration getDeclaration ()
		{
			return declaration;
		}

		public Object getInstance()
        {
            return instance;
        }

        private Object makeInstance()
        {
            Type mapped = declaration.getBoundClass(true);
            return Activator.CreateInstance(mapped);
        }

        public CategoryType getType()
        {
			return (CategoryType)this.type;
        }

        public ICollection<String> getMemberNames()
        {
            // TODO Auto-generated method stub
            return null;
        }

		static Dictionary<String, Context> Factory ()
		{
			return new Dictionary<String, Context> ();
		}

		// don't call getters from getters, so register them
		ThreadLocal<Dictionary<String, Context>> activeGetters = new ThreadLocal<Dictionary<String, Context>> (Factory);


		public override IValue GetMember (Context context, String attrName)
		{
			Context stacked;
			activeGetters.Value.TryGetValue (attrName, out stacked);
			bool first = stacked == null;
			if (first)
				activeGetters.Value [attrName] = context;
			try {
				return GetMember (context, attrName, stacked == null);
			} finally {
				if (first)
					activeGetters.Value [attrName] = null;
			}
		}

		protected IValue GetMember (Context context, String attrName, bool allowGetter)
		{
			GetterMethodDeclaration getter = allowGetter ? declaration.findGetter (context, attrName) : null;
			if (getter != null) {
				context = context.newInstanceContext (this).newChildContext(); // mimic method call
				return getter.interpret(context);
			} else {
	            Object value = getPropertyOrField(attrName);
	            CSharpClassType ct = new CSharpClassType(value.GetType());
	            return ct.ConvertCSharpValueToPromptoValue(context, value, null);
			}
        }

        private Object getPropertyOrField(String attrName)
        {
            Object value = null;
            if (TryGetProperty(attrName, out value))
                return value;
            if (TryGetField(attrName, out value))
                return value;
            throw new SyntaxError("Missing property or field:" + attrName);
        }

        private bool TryGetField(string attrName, out object value)
        {
			Type type = instance.GetType ();
			FieldInfo field = type.GetField(attrName, bindingFlags);
            if (field != null)
                value = field.GetValue(instance);
            else
                value = null;
            return field != null;
        }

        private bool TryGetProperty(String attrName, out Object value)
        {
			Type type = instance.GetType ();
			PropertyInfo property = type.GetProperty(attrName, bindingFlags);
            if (property != null)
                value = property.GetValue(instance, null);
            else
                value = null;
            return property != null;
        }
	
   
		// don't call setters from setters, so register them
		ThreadLocal<Dictionary<String, Context>> activeSetters = new ThreadLocal<Dictionary<String, Context>> (Factory);

		public override void SetMember (Context context, String attrName, IValue value)
		{
			if (!mutable)
				throw new NotMutableError ();
			Context stacked;
			bool first = !activeSetters.Value.TryGetValue (attrName, out stacked);
			if (first)
				activeSetters.Value [attrName] = context;
			try {
				SetMember (context, attrName, value, stacked == null);
			} finally {
				if (first)
					activeSetters.Value [attrName] = null;
			}
		}

		public void SetMember (Context context, String attrName, IValue value, bool allowSetter)
		{
			AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration> (attrName);
			SetterMethodDeclaration setter = allowSetter ? declaration.findSetter (context, attrName) : null;
			if (setter != null) {
				// use attribute name as parameter name for incoming value
				context = context.newInstanceContext (this).newChildContext();
				context.registerValue (new Variable (attrName, decl.getType ()));
				context.setValue (attrName, value);
				value = setter.interpret (context);
			}
            setPropertyOrField(value, attrName);
        }

        private void setPropertyOrField(IValue value, String attrName)
        {
            if (TrySetProperty(value, attrName))
                return;
            if (TrySetField(value, attrName))
                return;
            throw new SyntaxError("Missing property or field:" + attrName);
        }

        private bool TrySetField(IValue value, String attrName)
        {
			FieldInfo field = instance.GetType().GetField(attrName, bindingFlags);
            if (field != null)
                field.SetValue(instance, value.ConvertTo(field.FieldType));
            return field != null;
        }

        private bool TrySetProperty(IValue value, String attrName)
        {
			PropertyInfo property = instance.GetType().GetProperty(attrName, bindingFlags);
            if(property!=null)
                property.SetValue(instance, value.ConvertTo(property.PropertyType), null);
            return property!=null;
        }


   
    }

}

using presto.runtime;
using System;
using presto.error;
using System.Reflection;
using presto.type;
using presto.grammar;
using presto.utils;

namespace presto.csharp
{

    public class CSharpIdentifierExpression : CSharpExpression
    {

        public static CSharpIdentifierExpression parse(String ids)
        {
            String[] parts = ids.Split("\\.".ToCharArray());
            CSharpIdentifierExpression result = null;
            foreach (String part in parts)
                result = new CSharpIdentifierExpression(result, part);
            return result;
        }

        CSharpIdentifierExpression parent;
        String identifier;

        public CSharpIdentifierExpression(String identifier)
        {
            this.identifier = identifier;
        }

        public CSharpIdentifierExpression(CSharpIdentifierExpression parent, String identifier)
        {
            this.parent = parent;
            this.identifier = identifier;
        }

        public CSharpIdentifierExpression getParent()
        {
            return parent;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        override
        public String ToString()
        {
            if (parent == null)
                return identifier;
            else
                return parent.ToString() + "." + identifier;
        }

		public void ToDialect(CodeWriter writer) {
			if(parent!=null) {
				parent.ToDialect(writer);
				writer.append('.');
			}
			writer.append(identifier);
		}

		public Object interpret(Context context)
        {
            if (parent == null)
                return evaluate_root(context);
            else
                return evaluate_child(context);
        }

        Object evaluate_root(Context context)
        {
            Object o = evaluate_instance(context);
            if (o != null)
                return o;
            else
                return evaluate_class(); // as an instance for static field/method
        }

        Object evaluate_instance(Context context)
        {
            try
            {
                return context.getValue(identifier);
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        public Type evaluate_class()
        {
            String fullName = this.ToString();
            Type klass = Type.GetType(fullName, false);
            if (klass != null)
                return klass;
            klass = Assembly.GetExecutingAssembly().GetType(fullName, false);
            if (klass != null)
                return klass;
            klass = Assembly.GetCallingAssembly().GetType(fullName, false);
            if (klass != null)
                return klass;
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                klass = asm.GetType(fullName, false);
                if (klass != null)
                    return klass;
            }
            return null;
         }

        Object evaluate_child(Context context)
        {
            Object o = parent.interpret(context);
            if (o != null)
                return evaluate_field_or_property(o);
            else
                return evaluate_class();
        }

        Object evaluate_field_or_property(Object o)
        {
            Type klass = null;
            if (o is Type)
            {
                klass = (Type)o;
                o = null;
            }
            else
                klass = o.GetType();
            FieldInfo field = klass.GetField(identifier);
            if (field != null)
                return field.GetValue(o);
            PropertyInfo prop = klass.GetProperty(identifier);
            if (prop != null)
                return prop.GetValue(o, null);
            return null;
        }

        public IType check(Context context)
        {
            if (parent == null)
                return check_root(context);
            else
                return check_child(context);
        }

        IType check_root(Context context)
        {
            IType t = check_instance(context);
            if (t != null)
                return t;
            else
                return check_class(); // as an instance for accessing static field/method
        }

        IType check_instance(Context context)
        {
            INamed named = context.getRegisteredValue<INamed>(identifier);
            if (named == null)
                return null;
            try
            {
                return named.GetType(context);
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        IType check_class()
        {
            Type klass = evaluate_class();
            if(klass==null)
                return null;
            else
                return new CSharpClassType(klass);
         }

        IType check_child(Context context)
        {
            IType t = parent.check(context);
            if (t != null)
                return check_field_or_property(t);
            else
                return check_class();
        }

        IType check_field_or_property(IType t)
        {
            if (!(t is CSharpClassType))
                return null;
            Type klass = ((CSharpClassType)t).klass;
            FieldInfo field = klass.GetField(identifier);
            if (field != null)
                 return new CSharpClassType(field.FieldType);
            PropertyInfo prop = klass.GetProperty(identifier);
            if (prop != null)
                 return new CSharpClassType(prop.PropertyType);
            else
                return null;
        }
    }

}

using prompto.runtime;
using System;
using prompto.error;
using System.Reflection;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using System.Collections.Generic;
using prompto.store;

namespace prompto.csharp
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
        string name;

        public CSharpIdentifierExpression(String identifier)
        {
            this.name = identifier;
        }

        public CSharpIdentifierExpression(CSharpIdentifierExpression parent, String identifier)
        {
            this.parent = parent;
            this.name = identifier;
        }

        public CSharpIdentifierExpression getParent()
        {
            return parent;
        }

        public string getIdentifier()
        {
            return name;
        }


        public override string ToString()
        {
            if (parent == null)
                return name;
            else
                return parent.ToString() + "." + name;
        }

        public override void ToDialect(CodeWriter writer)
        {
            if (parent != null)
            {
                parent.ToDialect(writer);
                writer.append('.');
            }
            writer.append(name);
        }

        public override object interpret(Context context)
        {
            if (parent == null)
                return interpret_root(context);
            else
                return interpret_child(context);
        }

        object interpret_root(Context context)
        {
            Object o = interpret_prompto(context);
            if (o != null)
                return o;
            o = interpret_instance(context);
            if (o != null)
                return o;
            else
                return interpret_type(); // as an instance for static field/method
        }

        object interpret_prompto(Context context)
        {
            switch (name)
            {
                case "$context":
                    return context;
                case "$store":
                    return DataStore.Instance;
            }
            return null;
        }

        object interpret_instance(Context context)
        {
            if ("null".Equals(name))
                return null;
            else try
            {
                return context.getValue(name);
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        public Type interpret_type()
        {
            String fullName = this.ToString();
            Type klass = Type.GetType(fullName, false);
            if (klass != null)
                return klass;
            HashSet<Assembly> assemblies = GetAllAssemblies();
            foreach (Assembly asm in assemblies)
            {
                klass = asm.GetType(fullName, false);
                if (klass != null)
                    return klass;
            }
            return null;
        }

        static HashSet<Assembly> allAssemblies = null;

        HashSet<Assembly> GetAllAssemblies()
        {
            if (allAssemblies == null)
            {
                allAssemblies = new HashSet<Assembly>();
                AddAll(Assembly.GetEntryAssembly());
                AddAll(Assembly.GetCallingAssembly());
                AddAll(Assembly.GetExecutingAssembly());
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    AddAll(asm);
            }
            return allAssemblies;
        }

        void AddAll(Assembly asm)
        {
            if (asm == null || allAssemblies.Contains(asm))
                return;
            allAssemblies.Add(asm);
            foreach (AssemblyName name in asm.GetReferencedAssemblies())
            {
                try
                {
                    AddAll(Assembly.Load(name));
                }
                catch (Exception)
                {
                    // ok
                }
            }
        }

        object interpret_child(Context context)
        {
            object o = parent.interpret(context);
            if (o != null)
                return interpret_field_or_property(o);
            else
                return interpret_type();
        }

        object interpret_field_or_property(object o)
        {
            Type klass = null;
            if (o is Type)
            {
                klass = (Type)o;
                o = null;
            }
            else
                klass = o.GetType();
            FieldInfo field = klass.GetField(name);
            if (field != null)
                return field.GetValue(o);
            PropertyInfo prop = klass.GetProperty(name);
            if (prop != null)
                return prop.GetValue(o, null);
            return null;
        }

        public override IType check(Context context)
        {
            if (parent == null)
                return check_root(context);
            else
                return check_child(context);
        }

        IType check_root(Context context)
        {
            IType t = check_prompto(context);
            if (t != null)
                return t;
            t = check_instance(context);
            if (t != null)
                return t;
            else
                return check_class(); // as an instance for accessing static field/method
        }

        IType check_prompto(Context context)
        {
            switch (name)
            {
                case "$context":
                    return new CSharpClassType(context.GetType());
                case "$store":
                    return new CSharpClassType(typeof(IStore));
            }
            return null;
        }

        IType check_instance(Context context)
        {
            INamed named = context.getRegisteredValue<INamed>(name);
            if (named == null)
                return null;
            try
            {
                return named.GetIType(context);
            }
            catch (SyntaxError)
            {
                return null;
            }
        }

        IType check_class()
        {
            Type klass = interpret_type();
            if (klass == null)
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
            Type klass = ((CSharpClassType)t).type;
            FieldInfo field = klass.GetField(name);
            if (field != null)
                return new CSharpClassType(field.FieldType);
            PropertyInfo prop = klass.GetProperty(name);
            if (prop != null)
                return new CSharpClassType(prop.PropertyType);
            else
                return null;
        }
    }

}

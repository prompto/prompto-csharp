using System.Collections.Generic;
using System;
using prompto.value;
using DateTime = prompto.value.DateTimeValue;
using Boolean = System.Boolean;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.runtime;
using System.Collections;

namespace prompto.csharp
{
    public class CSharpClassType : CategoryType
    {

        static Dictionary<Type, IType> typeToPromptoMap = new Dictionary<Type, IType>();

        static CSharpClassType()
        {
            typeToPromptoMap[typeof(void)] = VoidType.Instance;
            typeToPromptoMap[typeof(bool)] = BooleanType.Instance;
            typeToPromptoMap[typeof(bool?)] = BooleanType.Instance;
            typeToPromptoMap[typeof(char)] = CharacterType.Instance;
            typeToPromptoMap[typeof(char?)] = CharacterType.Instance;
            typeToPromptoMap[typeof(int)] = IntegerType.Instance;
            typeToPromptoMap[typeof(int?)] = IntegerType.Instance;
            typeToPromptoMap[typeof(long)] = IntegerType.Instance;
            typeToPromptoMap[typeof(long?)] = IntegerType.Instance;
            typeToPromptoMap[typeof(float)] = DecimalType.Instance;
            typeToPromptoMap[typeof(float?)] = DecimalType.Instance;
            typeToPromptoMap[typeof(double)] = DecimalType.Instance;
            typeToPromptoMap[typeof(double?)] = DecimalType.Instance;
            typeToPromptoMap[typeof(string)] = TextType.Instance;
            typeToPromptoMap[typeof(DateTimeOffset)] = DateTimeType.Instance;
            typeToPromptoMap[typeof(DateTimeOffset?)] = DateTimeType.Instance;
            typeToPromptoMap[typeof(Guid)] = UUIDType.Instance;
            typeToPromptoMap[typeof(DocumentValue)] = DocumentType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(DateValue)] = DateType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(TimeValue)] = TimeType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(DateTimeValue)] = DateTimeType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(PeriodValue)] = PeriodType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(VersionValue)] = VersionType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(JsxValue)] = JsxType.Instance; // TODO until we have a compiler
            typeToPromptoMap[typeof(object)] = AnyType.Instance;
            typeToPromptoMap[typeof(IValue)] = AnyType.Instance;
        }

        internal Type type;

        public CSharpClassType(Type type)
            : base(type.FullName)
        {
            this.type = type;
        }

        public override Type ToCSharpType(Context context)
        {
            return this.type;
        }

        public IType ConvertCSharpTypeToPromptoType(Context context, IType returnType)
        {
            return ConvertCSharpTypeToPromptoType(context, type, returnType);
        }


        public IType ConvertCSharpTypeToPromptoType(Context context, Type type, IType returnType)
        {
            IType result;
            typeToPromptoMap.TryGetValue(type, out result);
            if (result != null)
                return result;
            result = GetITypeFromCollectionType(context, type);
            if (result != null)
                return result;
            NativeCategoryDeclaration decl = context.getNativeBinding(type);
            if (decl != null)
                return decl.GetIType(context);
            else if (returnType == AnyType.Instance)
                return returnType;
            else
                return null;
        }

        Type GetGenericType(Type type)
        {
            if (type == null)
                return null;
            else if (type.IsGenericType)
                return type;
            else 
                return GetGenericType(type.BaseType);
         }

        public IType GetITypeFromCollectionType(Context context, Type type)
        {
            type = GetGenericType(type);
            if (type==null)
                return null;
            Type generic = type.GetGenericTypeDefinition();
            if (typeof(IList).IsAssignableFrom(generic))
            {
                Type item = type.GetGenericArguments()[0];
                IType itemType = ConvertCSharpTypeToPromptoType(context, item, null);
                return new ListType(itemType);
            }
            else if (generic == typeof(IEnumerator<>))
            {
                Type item = type.GetGenericArguments()[0];
                IType itemType = ConvertCSharpTypeToPromptoType(context, item, null);
                return new IteratorType(itemType);
            }
            else if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
                return DocumentType.Instance;
            else if (generic == typeof(IDictionary<,>) && type.GetGenericArguments()[0] == typeof(string))
            {
                Type item = type.GetGenericArguments()[1];
                IType itemType = ConvertCSharpTypeToPromptoType(context, item, null);
                return new DictType(itemType);
            }
            else
                return null;
        }

        public IValue ConvertCSharpValueToPromptoValue(Context context, Object value, IType returnType)
        {
            return ConvertCSharpValueToPromptoValue(context, value, type, returnType);
        }

        public static IValue ConvertCSharpValueToPromptoValue(Context context, Object value, Type type, IType returnType)
        {
            IValue result = ConvertIValue(value);
            if (result != null)
                return result;
            result = ConvertNative(context, value, type);
            if (result != null)
                return result;
            result = ConvertDocument(context, value, type, returnType);
            if (result != null)
                return result;
            result = ConvertList(context, value, type, returnType);
            if (result != null)
                return result;
            result = ConvertSet(context, value, type, returnType);
            if (result != null)
                return result;
            result = ConvertDict(context, value, type, returnType);
            if (result != null)
                return result;
            result = ConvertIterator(context, value, type, returnType);
            if (result != null)
                return result;
            result = ConvertCategory(context, value, type, returnType);
            if (result != null)
                return result;
            else
                throw new InternalError("Unable to convert: " + value.GetType().Name);
        }

        private static bool IsA(Type expected, Type actual)
        {
            if (expected.IsAssignableFrom(actual))
                return true;
            if (actual.IsGenericType && expected.IsAssignableFrom(actual.GetGenericTypeDefinition()))
                return true;
            foreach (Type intf in actual.GetInterfaces())
            {
                if (IsA(expected, intf))
                    return true;
            }
            if (actual.BaseType != null)
                return IsA(expected, actual.BaseType);
            else
                return false;
        }

        private static IValue ConvertIterator(Context context, Object value, Type type, IType returnType)
        {
            if (returnType is IteratorType && IsA(typeof(IEnumerator<>), value.GetType()))
            {
                Type elemType = type.GetGenericArguments()[0];
                IType itemType = ((IteratorType)returnType).GetItemType();
                ConvertingEnumerator converter = new ConvertingEnumerator(context, (IEnumerator<Object>)value, elemType, itemType);
                return new IteratorValue(itemType, converter);
            }
            else
                return null;
        }

        private static IValue ConvertDict(Context context, Object value, Type type, IType returnType)
        {
            if (returnType is DictType && IsA(typeof(Dictionary<,>), value.GetType()))
            {
                Type keyType = type.GetGenericArguments()[0];
                Type elemType = type.GetGenericArguments()[1];
                IType itemType = ((DictType)returnType).GetItemType();
                Dictionary<TextValue, IValue> dict = new Dictionary<TextValue, IValue>();
                foreach (Object obj in ((Dictionary<Object, Object>)value).Keys)
                {
                    TextValue key = (TextValue)ConvertCSharpValueToPromptoValue(context, obj, keyType, TextType.Instance);
                    Object val = ((Dictionary<Object, Object>)value)[obj];
                    IValue ival = ConvertCSharpValueToPromptoValue(context, val, elemType, itemType);
                    dict[key] = ival;
                }
                return new DictValue(itemType, false, dict);
            }
            else
                return null;
        }

        private static IValue ConvertSet(Context context, Object value, Type type, IType returnType)
        {
            if (returnType is SetType && IsA(typeof(HashSet<>), value.GetType()))
            {
                Type elemType = type.GetGenericArguments()[0];
                IType itemType = ((SetType)returnType).GetItemType();
                HashSet<IValue> set = new HashSet<IValue>();
                foreach (Object obj in (IEnumerable)value)
                {
                    IValue val = ConvertCSharpValueToPromptoValue(context, obj, elemType, itemType);
                    set.Add(val);
                }
                return new SetValue(itemType, set);
            }
            else
                return null;
        }

        private static IValue ConvertList(Context context, Object value, Type type, IType returnType)
        {
            if (IsA(typeof(List<>), value.GetType()))
            {
                if (returnType is ListType)
                {
                    Type elemType = type.GetGenericArguments()[0];
                    IType itemType = ((ListType)returnType).GetItemType();
                    List<IValue> list = new List<IValue>();
                    foreach (Object obj in (IEnumerable)value)
                    {
                        IValue val = ConvertCSharpValueToPromptoValue(context, obj, elemType, itemType);
                        list.Add(val);
                    }
                    return new ListValue(itemType, list);
                }
                else if (returnType == AnyType.Instance)
                {
                    List<IValue> list = new List<IValue>();
                    foreach (Object obj in (IEnumerable)value)
                    {
                        IValue val = ConvertCSharpValueToPromptoValue(context, obj, null, AnyType.Instance);
                        list.Add(val);
                    }
                    return new ListValue(AnyType.Instance, list);

                }
            }
            return null;
        }

        private static IValue ConvertDocument(Context context, Object value, Type type, IType returnType)
        {
            if ((returnType == DocumentType.Instance || returnType == AnyType.Instance) && value is IDictionary<string, object>) {
                IDictionary<string, object> dict = (IDictionary<string, object>)value;
                DocumentValue doc = new DocumentValue();
                foreach(KeyValuePair< string, object> kvp in dict)
                {
                    doc.SetMemberValue(context, kvp.Key, ConvertCSharpValueToPromptoValue(context, kvp.Value, null, AnyType.Instance));
                }
                return doc;
            }

            return null; 
        }

        private static IValue ConvertCategory(Context context, Object value, Type type, IType returnType)
        {
            if (type == null)
                return null;
            NativeCategoryDeclaration decl = context.getNativeBinding(type);
            if (decl != null)
                return new NativeInstance(decl, value);
            else if (returnType == AnyType.Instance)
                return new NativeInstance(AnyNativeCategoryDeclaration.Instance, value);
            else
                return null;
        }

        private static IValue ConvertNative(Context context, Object value, Type type)
        {
            IType itype;
            if (type == null)
                type = typeof(object);
            if (type == typeof(object) && value != null)
                type = value.GetType();
            if (typeToPromptoMap.TryGetValue(type, out itype))
                return itype.ConvertCSharpValueToIValue(context, value);
            else
                return null;
        }


        private static IValue ConvertIValue(Object value)
        {
            if (value is IValue)
                return (IValue)value;
            else
                return null;
        }
    }

    class ConvertingEnumerator : IEnumerator<IValue>
    {
        Context context;
        IEnumerator<Object> source;
        Type itemType;
        IType elemType;
        IValue current;

        public ConvertingEnumerator(Context context, IEnumerator<Object> source, Type itemType, IType elemType)
        {
            this.context = context;
            this.source = source;
            this.itemType = itemType;
            this.elemType = elemType;
        }

        public object Current
        {
            get
            {
                return current;
            }
        }

        IValue IEnumerator<IValue>.Current
        {
            get
            {
                return current;
            }
        }

        public bool MoveNext()
        {
            current = null;
            if (!source.MoveNext())
                return false;
            current = CSharpClassType.ConvertCSharpValueToPromptoValue(context, source.Current, itemType, elemType);
            return true;
        }

        public void Reset()
        {
            source.Reset();
        }

        public void Dispose()
        {
            source.Dispose();
        }
    }

}
using prompto.runtime;
using System;
using prompto.value;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using prompto.store;
using prompto.expression;
using prompto.grammar;
using prompto.statement;
using prompto.error;
using prompto.parser;
using prompto.literal;
using prompto.declaration;
using prompto.utils;
using prompto.type.document;

namespace prompto.type
{

    public class DocumentType : NativeType
    {

        static DocumentType instance_ = new DocumentType();

        public static DocumentType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DocumentType()
            : base(TypeFamily.DOCUMENT)
        {
        }


        public override bool isAssignableFrom(Context context, IType other)
        {
            return base.isAssignableFrom(context, other)
                       || other == AnyType.Instance
                       || (other is CategoryType && "Any".Equals(other.GetTypeName()));
        }


        public override bool isMoreSpecificThan(Context context, IType other)
        {
            if ((other == NullType.Instance) || (other == AnyType.Instance) || (other == MissingType.Instance))
                return true;
            else
                return base.isMoreSpecificThan(context, other);
        }

        public override IType checkItem(Context context, IType itemType)
        {
            return AnyType.Instance;
        }

        public override IType checkMember(Context context, String name)
        {
            switch (name)
            {
                case "count":
                    return IntegerType.Instance;
                case "keys":
                    return new SetType(TextType.Instance);
                case "values":
                    return new ListType(AnyType.Instance);
                case "text":
                    return TextType.Instance;
                default:
                    return AnyType.Instance;
            }
        }


        public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other == this)
                return this;
            else
                return base.checkAdd(context, other, tryReverse);
        }

        public override Type ToCSharpType(Context context)
        {
            return typeof(IDictionary<string, object>);
        }

        public override IValue ReadJSONValue(Context context, JToken value, Dictionary<String, byte[]> parts)
        {
            if (!(value is JObject))
                throw new InvalidDataException("Expecting a JSON object!");
            JObject obj = (JObject)value;
            DocumentValue instance = new DocumentValue();
            foreach (KeyValuePair<String, JToken> prop in obj)
            {
                IValue item = ReadJSONField(context, prop.Value, parts);
                instance.SetMemberValue(context, prop.Key, item);
            }
            return instance;
        }

        private IValue ReadJSONField(Context context, JToken fieldData, Dictionary<String, byte[]> parts)
        {
            switch (fieldData.Type)
            {
                case JTokenType.Null:
                    return NullValue.Instance;
                case JTokenType.Boolean:
                    return prompto.value.BooleanValue.ValueOf(fieldData.Value<bool>());
                case JTokenType.Integer:
                    return new prompto.value.IntegerValue(fieldData.Value<long>());
                case JTokenType.Float:
                    return new prompto.value.DecimalValue(fieldData.Value<double>());
                case JTokenType.String:
                    return new prompto.value.TextValue(fieldData.Value<String>());
                case JTokenType.Array:
                    throw new NotSupportedException("Array");
                case JTokenType.Object:
                    throw new NotSupportedException("Object");
                default:
                    throw new NotSupportedException(fieldData.Type.ToString());
            }
        }

        public override Comparer<IValue> getComparer(Context context, IExpression key, bool descending)
        {
            if (key == null)
                key = new TextLiteral("\"key\"");
            if (globalMethodExists(context, key.ToString()))
                return new GlobalMethodComparer(context, key.ToString(), descending);
            else if (key is TextLiteral)
                return new EntryComparer(context, (TextLiteral)key, descending);
            else
                return new ExpressionComparer(context, key, descending);
        }


        private bool globalMethodExists(Context context, String name)
        {
            MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
            if (methods == null)
                return false;
            else
                return methods.ContainsKey(this.GetTypeName());
        }

    }

}

namespace prompto.type.document
{

    class GlobalMethodComparer : ValueComparer<DocumentValue>
    {
        MethodCall methodCall;

        public GlobalMethodComparer(Context context, String methodName, bool descending)
            : base(context, descending)
        {
            this.methodCall = buildMethodCall(methodName);
        }

        private MethodCall buildMethodCall(String methodName)
        {
            IExpression exp = new ValueExpression(DocumentType.Instance, new DocumentValue());
            Argument arg = new Argument(null, exp);
            ArgumentList args = new ArgumentList();
            args.Add(arg);
            return new MethodCall(new MethodSelector(methodName), args);
        }

        protected override int DoCompare(DocumentValue o1, DocumentValue o2)
        {
            Argument argument = methodCall.getArguments()[0];
            argument.Expression = new ValueExpression(AnyType.Instance, o1);
            Object value1 = methodCall.interpret(context);
            argument.Expression = new ValueExpression(AnyType.Instance, o2);
            Object value2 = methodCall.interpret(context);
            return ObjectUtils.CompareValues(value1, value2);
        }

    }

    class EntryComparer : ValueComparer<DocumentValue>
    {
        String name;

        public EntryComparer(Context context, TextLiteral entry, bool descending)
                : base(context, descending)
        {
            this.name = entry.getValue().Value;
        }


        protected override int DoCompare(DocumentValue o1, DocumentValue o2)
        {
            Object value1 = o1.GetMember(name, false);
            Object value2 = o2.GetMember(name, false);
            return ObjectUtils.CompareValues(value1, value2);
        }
    }

    class ExpressionComparer : ValueComparer<DocumentValue>
    {
        IExpression key;

        public ExpressionComparer(Context context, IExpression key, bool descending)
                : base(context, descending)
        {
            this.key = key;
        }


        protected override int DoCompare(DocumentValue o1, DocumentValue o2)
        {
            Context co = context.newDocumentContext(o1, false);
            Object value1 = key.interpret(co);
            co = context.newDocumentContext(o2, false);
            Object value2 = key.interpret(co);
            return ObjectUtils.CompareValues(value1, value2);
        }
    }


}

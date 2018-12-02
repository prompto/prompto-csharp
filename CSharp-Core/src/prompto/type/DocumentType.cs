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
			if (itemType == TextType.Instance)
				return AnyType.Instance;
			else
				return base.checkItem(context, itemType);
		}

		public override IType checkMember(Context context, String name)
		{
			return AnyType.Instance;
		}


		public override Type ToCSharpType()
		{
			return typeof(Document);
		}

		public override IValue ReadJSONValue(Context context, JToken value, Dictionary<String, byte[]> parts)
		{
			if (!(value is JObject))
				throw new InvalidDataException("Expecting a JSON object!");
			JObject obj = (JObject)value;
			Document instance = new Document();
			foreach (KeyValuePair<String, JToken> prop in obj)
			{
				IValue item = ReadJSONField(context, prop.Value, parts);
				instance.SetMember(context, prop.Key, item);
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
					return prompto.value.Boolean.ValueOf(fieldData.Value<bool>());
				case JTokenType.Integer:
					return new prompto.value.Integer(fieldData.Value<long>());
				case JTokenType.Float:
					return new prompto.value.Decimal(fieldData.Value<double>());
				case JTokenType.String:
					return new prompto.value.Text(fieldData.Value<String>());
				case JTokenType.Array:
					throw new NotSupportedException("Array");
				case JTokenType.Object:
					throw new NotSupportedException("Object");
				default:
					throw new NotSupportedException(fieldData.Type.ToString());
			}
		}


		public ListValue sort(Context context, IContainer list, IExpression key, bool descending)
		{
			if (key == null)
				key = new TextLiteral("\"key\"");
			if (globalMethodExists(context, list, key.ToString()))
				return sortByGlobalMethod(context, list, key.ToString(), descending);
			else if (key is TextLiteral)
				return sortByEntry(context, list, (TextLiteral)key, descending);
			else
				return sortByExpression(context, list, key, descending);
		}


		ListValue sortByEntry(Context context, IContainer list, TextLiteral key, bool descending)
		{
			return this.doSort(context, list, new DocumentEntryComparer(context, key.getValue().Value, descending));
		}

		ListValue sortByExpression(Context context, IContainer list, IExpression key, bool descending)
		{
			return this.doSort(context, list, new DocumentExpressionComparer(context, key, descending));
		}

		private bool globalMethodExists(Context context, IContainer list, String name)
		{
			MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
			if (methods == null)
				return false;
			else
				return methods.ContainsKey(this.GetTypeName());
		}

		private ListValue sortByGlobalMethod(Context context, IContainer list, String name, bool descending)
		{
			IExpression exp = new ExpressionValue(this, new Document());
			ArgumentAssignment arg = new ArgumentAssignment(null, exp);
			ArgumentAssignmentList args = new ArgumentAssignmentList();
			args.Add(arg);
			MethodCall call = new MethodCall(new MethodSelector(name), args);
			return this.doSort(context, list, new DocumentGlobalMethodComparer(context, call, descending));
		}


		class DocumentGlobalMethodComparer : ExpressionComparer<Document>
		{
			MethodCall method;

			public DocumentGlobalMethodComparer(Context context, MethodCall method, bool descending)
				: base(context, descending)
			{
				this.method = method;
			}


			protected override int DoCompare(Document o1, Document o2)
			{
				ArgumentAssignment assignment = method.getAssignments()[0];
				assignment.setExpression(new ExpressionValue(AnyType.Instance, o1));
				Object value1 = method.interpret(context);
				assignment.setExpression(new ExpressionValue(AnyType.Instance, o2));
				Object value2 = method.interpret(context);
				return ObjectUtils.CompareValues(value1, value2);
			}

		}

		class DocumentEntryComparer : ExpressionComparer<Document>
		{
			String name;

			public DocumentEntryComparer(Context context, String name, bool descending)
					: base(context, descending)
			{
				this.name = name;
			}


			protected override int DoCompare(Document o1, Document o2)
			{
				Object value1 = o1.GetMember(name);
				Object value2 = o2.GetMember(name);
				return ObjectUtils.CompareValues(value1, value2);
			}
		}

		class DocumentExpressionComparer : ExpressionComparer<Document>
		{
			IExpression key;

			public DocumentExpressionComparer(Context context, IExpression key, bool descending)
					: base(context, descending)
			{
				this.key = key;
			}


			protected override int DoCompare(Document o1, Document o2)
			{
				Context co = context.newDocumentContext(o1, false);
				Object value1 = key.interpret(co);
				co = context.newDocumentContext(o2, false);
				Object value2 = key.interpret(co);
				return ObjectUtils.CompareValues(value1, value2);
			}
		}


	}



}



using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using prompto.store;
using prompto.declaration;
using prompto.argument;
using prompto.literal;
using prompto.utils;
using System.Globalization;
using System.Threading;

namespace prompto.type
{

	public class TextType : NativeType
	{

		static TextType instance_ = new TextType();


		public static TextType Instance
		{
			get
			{
				return instance_;
			}
		}

		private TextType()
			: base(TypeFamily.TEXT)
		{
		}


		public override Type ToCSharpType()
		{
			return typeof(string);
		}


		public override ICollection<IMethodDeclaration> getMemberMethods(Context context, string name)
		{
			List<IMethodDeclaration> list = new List<IMethodDeclaration>();

			switch (name)
			{
				case "toLowerCase":
					list.Add(TO_LOWERCASE_METHOD);
					return list;
				case "toUpperCase":
					list.Add(TO_UPPERCASE_METHOD);
					return list;
				case "toCapitalized":
					list.Add(TO_CAPITALIZED_METHOD);
					return list;
				case "replace":
					list.Add(REPLACE_METHOD);
					return list;
				case "split":
					list.Add(SPLIT_METHOD);
					return list;
				case "trim":
					list.Add(TRIM_METHOD);
					return list;
				default:
					return base.getMemberMethods(context, name);
			}
		}

		internal static IArgument SINGLE_SPACE_ARGUMENT = new CategoryArgument(TextType.Instance, "separator", new TextLiteral("\" \""));
		internal static IArgument TO_REPLACE_ARGUMENT = new CategoryArgument(TextType.Instance, "toReplace");
		internal static IArgument REPLACE_WITH_ARGUMENT = new CategoryArgument(TextType.Instance, "replaceWith");

		internal static IMethodDeclaration SPLIT_METHOD = new SplitMethodDeclaration();
		internal static IMethodDeclaration REPLACE_METHOD = new ReplaceMethodDeclaration();
		internal static IMethodDeclaration TO_LOWERCASE_METHOD = new ToLowerCaseMethodDeclaration();
		internal static IMethodDeclaration TO_UPPERCASE_METHOD = new ToUpperCaseMethodDeclaration();
		internal static IMethodDeclaration TO_CAPITALIZED_METHOD = new ToCapitalizedMethodDeclaration();
		internal static IMethodDeclaration TRIM_METHOD = new TrimMethodDeclaration();

		public override IType checkMember(Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}


		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || other == CharacterType.Instance;
		}


		public override IType checkAdd(Context context, IType other, bool tryReverse)
		{
			return this;
		}


		public override IType checkMultiply(Context context, IType other, bool tryReverse)
		{
			if (other is IntegerType)
				return this;
			else
				return base.checkMultiply(context, other, tryReverse);
		}

		override
		public IType checkCompare(Context context, IType other)
		{
			if (other is TextType || other is CharacterType)
				return BooleanType.Instance;
			return base.checkCompare(context, other);
		}

		override
		public IType checkItem(Context context, IType other)
		{
			if (other == IntegerType.Instance)
				return CharacterType.Instance;
			else
				return base.checkItem(context, other);
		}

		override
		public IType checkContains(Context context, IType other)
		{
			if (other is TextType || other is CharacterType)
				return BooleanType.Instance;
			return base.checkContains(context, other);
		}

		override
		public IType checkContainsAllOrAny(Context context, IType other)
		{
			return BooleanType.Instance;
		}

		override
		public IType checkSlice(Context context)
		{
			return this;
		}


		override
		public ListValue sort(Context context, IContainer list, bool descending)
		{
			return this.doSort(context, list, new TextComparer(context, descending));
		}

		override
		public String ToString(Object value)
		{
			if (value is Char)
				return "'" + value.ToString() + "'";
			else
				return "" + '"' + value + '"';
		}

		override
		public IValue ConvertCSharpValueToIValue(Context context, Object value)
		{
			if (value is String)
				return new Text((String)value);
			else
				return (IValue)value; // TODO for now
		}


	}

	class TextComparer : ExpressionComparer<Object>
	{
		public TextComparer(Context context, bool descending)
			: base(context, descending)
		{
		}

		override
		protected int DoCompare(Object o1, Object o2)
		{
			return o1.ToString().CompareTo(o2.ToString());
		}

	}


	class SplitMethodDeclaration : BuiltInMethodDeclaration
	{

		public SplitMethodDeclaration()
		: base("split", TextType.SINGLE_SPACE_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			string sep = (String)context.getValue("separator").GetStorableData();
			String[] parts = value.Split(sep[0]);
			List<IValue> list = new List<IValue>();
			foreach (String part in parts)
				list.Add(new Text(part));
			return new ListValue(TextType.Instance, list, false);
		}



		public override IType check(Context context)
		{
			return new ListType(TextType.Instance);
		}



	};

	class ReplaceMethodDeclaration : BuiltInMethodDeclaration
	{

		public ReplaceMethodDeclaration()
			: base("replace", TextType.TO_REPLACE_ARGUMENT, TextType.REPLACE_WITH_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			string toReplace = (String)context.getValue("toReplace").GetStorableData();
			string replaceWith = (String)context.getValue("replaceWith").GetStorableData();
			value = value.Replace(toReplace, replaceWith);
			return new Text(value);
		}



		public override IType check(Context context)
		{
			return TextType.Instance;
		}



	};

	class ToLowerCaseMethodDeclaration : BuiltInMethodDeclaration
	{
		public ToLowerCaseMethodDeclaration()
			: base("toLowerCase")
		{
		}

		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			String lower = value.ToLower();
			return new Text(lower);
		}

		public override IType check(Context context)
		{
			return TextType.Instance;
		}


	};

	class ToUpperCaseMethodDeclaration : BuiltInMethodDeclaration
	{
		public ToUpperCaseMethodDeclaration()
			: base("toUpperCase")
		{
		}

		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			String upper = value.ToUpper();
			return new Text(upper);
		}

		public override IType check(Context context)
		{
			return TextType.Instance;
		}


	};

	class TrimMethodDeclaration : BuiltInMethodDeclaration
	{
		public TrimMethodDeclaration()
			: base("trim")
		{
		}

		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			String trim = value.Trim();
			return new Text(trim);
		}

		public override IType check(Context context)
		{
			return TextType.Instance;
		}


	};

	class ToCapitalizedMethodDeclaration : BuiltInMethodDeclaration
	{
		public ToCapitalizedMethodDeclaration()
			: base("toCapitalized")
		{
		}


		public override IValue interpret(Context context)
		{
			string value = (String)getValue(context).GetStorableData();
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			string result = textInfo.ToTitleCase(value);
			return new Text(result);
		}

		public override IType check(Context context)
		{
			return TextType.Instance;
		}
	};


}
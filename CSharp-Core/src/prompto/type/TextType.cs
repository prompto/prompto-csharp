

using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using prompto.store;
using prompto.declaration;
using prompto.param;
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


        public override Type ToCSharpType(Context context)
        {
            return typeof(string);
        }


        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
            ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();

            switch (name)
            {
                case "startsWith":
                    list.Add(STARTS_WITH_METHOD);
                    return list;
                case "endsWith":
                    list.Add(ENDS_WITH_METHOD);
                    return list;
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
                case "replaceAll":
                    list.Add(REPLACE_ALL_METHOD);
                    return list;
                case "split":
                    list.Add(SPLIT_METHOD);
                    return list;
                case "trim":
                    list.Add(TRIM_METHOD);
                    return list;
                case "indexOf":
                    list.Add(INDEX_OF_METHOD);
                    return list;
                default:
                    return base.getMemberMethods(context, name);
            }
        }

        internal static IParameter SINGLE_SPACE_ARGUMENT = new CategoryParameter(TextType.Instance, "separator", new TextLiteral("\" \""));
        internal static IParameter TO_REPLACE_ARGUMENT = new CategoryParameter(TextType.Instance, "toReplace");
        internal static IParameter REPLACE_WITH_ARGUMENT = new CategoryParameter(TextType.Instance, "replaceWith");
        internal static IParameter VALUE_ARGUMENT = new CategoryParameter(TextType.Instance, "value");
        internal static IParameter FROM_INDEX_ARGUMENT = new CategoryParameter(IntegerType.Instance, "fromIndex", new IntegerLiteral("1"));

        internal static IMethodDeclaration STARTS_WITH_METHOD = new StartsWithMethodDeclaration();
        internal static IMethodDeclaration ENDS_WITH_METHOD = new EndsWithMethodDeclaration();
        internal static IMethodDeclaration SPLIT_METHOD = new SplitMethodDeclaration();
        internal static IMethodDeclaration REPLACE_METHOD = new ReplaceMethodDeclaration();
        internal static IMethodDeclaration REPLACE_ALL_METHOD = new ReplaceAllMethodDeclaration();
        internal static IMethodDeclaration TO_LOWERCASE_METHOD = new ToLowerCaseMethodDeclaration();
        internal static IMethodDeclaration TO_UPPERCASE_METHOD = new ToUpperCaseMethodDeclaration();
        internal static IMethodDeclaration TO_CAPITALIZED_METHOD = new ToCapitalizedMethodDeclaration();
        internal static IMethodDeclaration TRIM_METHOD = new TrimMethodDeclaration();
        internal static IMethodDeclaration INDEX_OF_METHOD = new TextIndexOfMethodDeclaration();

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

        
        public override void checkCompare(Context context, IType other)
        {
            if (other is TextType || other is CharacterType)
                return;
            else
                base.checkCompare(context, other);
        }

        
        public override IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return CharacterType.Instance;
            else
                return base.checkItem(context, other);
        }

        
        public override void checkContains(Context context, IType other)
        {
            if (other is TextType || other is CharacterType)
                return;
            else
                base.checkContains(context, other);
        }


        public override void checkContainsAllOrAny(Context context, IType other)
        {
            // nothing to do
        }


        public override IType checkSlice(Context context)
        {
            return this;
        }

        public override IType checkIterator(Context context)
        {
            return CharacterType.Instance;
        }




        public override String ToString(Object value)
        {
            if (value is Char)
                return "'" + value.ToString() + "'";
            else
                return "" + '"' + value + '"';
        }


        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is String)
                return new TextValue((String)value);
            else
                return (IValue)value; // TODO for now
        }

        public override Comparer<IValue> getNativeComparer(bool descending)
        {
            return new TextComparer(descending);
        }

    }

    class TextComparer : NativeComparer<IValue>
    {
        public TextComparer(bool descending)
            : base(descending)
        {
        }


        protected override int DoCompare(IValue o1, IValue o2)
        {
            return o1.ToString().CompareTo(o2.ToString());
        }

    }


    class StartsWithMethodDeclaration : BuiltInMethodDeclaration
    {

        public StartsWithMethodDeclaration()
        : base("startsWith", TextType.VALUE_ARGUMENT)
        { }

        public override IValue interpret(Context context)
        {
            string value = (String)getValue(context).GetStorableData();
            string find = (String)context.getValue("value").GetStorableData();
            bool test = value.StartsWith(find);
            return prompto.value.BooleanValue.ValueOf(test);
        }



        public override IType check(Context context)
        {
            return BooleanType.Instance;
        }

    };

    class EndsWithMethodDeclaration : BuiltInMethodDeclaration
    {

        public EndsWithMethodDeclaration()
        : base("endsWith", TextType.VALUE_ARGUMENT)
        { }

        public override IValue interpret(Context context)
        {
            string value = (String)getValue(context).GetStorableData();
            string find = (String)context.getValue("value").GetStorableData();
            bool test = value.EndsWith(find);
            return prompto.value.BooleanValue.ValueOf(test);
        }



        public override IType check(Context context)
        {
            return BooleanType.Instance;
        }

    };


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
                list.Add(new TextValue(part));
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
            IValue ivalue = getValue(context);
            string text = (String)ivalue.GetStorableData();
            string toReplace = (String)context.getValue("toReplace").GetStorableData();
            int idx = text.IndexOf(toReplace);
            if (idx < 0)
                return ivalue;
            string replaceWith = (String)context.getValue("replaceWith").GetStorableData();
            text = text.Substring(0, idx) + replaceWith + text.Substring(idx + toReplace.Length);
            return new TextValue(text);
        }

        public override IType check(Context context)
        {
            return TextType.Instance;
        }

    };

    class ReplaceAllMethodDeclaration : BuiltInMethodDeclaration
    {

        public ReplaceAllMethodDeclaration()
            : base("replaceAll", TextType.TO_REPLACE_ARGUMENT, TextType.REPLACE_WITH_ARGUMENT)
        { }

        public override IValue interpret(Context context)
        {
            string text = (String)getValue(context).GetStorableData();
            string toReplace = (String)context.getValue("toReplace").GetStorableData();
            string replaceWith = (String)context.getValue("replaceWith").GetStorableData();
            text = text.Replace(toReplace, replaceWith);
            return new TextValue(text);
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
            return new TextValue(lower);
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
            return new TextValue(upper);
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
            return new TextValue(trim);
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
            return new TextValue(result);
        }

        public override IType check(Context context)
        {
            return TextType.Instance;
        }
    };


    class TextIndexOfMethodDeclaration : BuiltInMethodDeclaration
    {

        public TextIndexOfMethodDeclaration()
            : base("indexOf", TextType.VALUE_ARGUMENT, TextType.FROM_INDEX_ARGUMENT)
        {
        }

        public override IValue interpret(Context context)
        {
            string value = (String)getValue(context).GetStorableData();
            string toFind = (String)context.getValue("value").GetStorableData();
            long fromIndex = (long)context.getValue("fromIndex").GetStorableData();
            int indexOf = value.IndexOf(toFind, (Int32)fromIndex - 1);
            return new prompto.value.IntegerValue(indexOf + 1);
        }


        public override IType check(Context context)
        {
            return IntegerType.Instance;
        }

    };



}
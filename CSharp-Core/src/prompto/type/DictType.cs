using prompto.runtime;
using System;
using prompto.value;
using System.Collections.Generic;
using prompto.store;
using prompto.declaration;
using prompto.param;
using prompto.error;

namespace prompto.type
{

	public class DictType : ContainerType
	{
	
		public DictType (IType itemType)
			: base (TypeFamily.DICTIONARY, itemType, itemType.GetTypeName() + "<:>")
		{
			this.itemType = itemType;
		}


		public override IterableType WithItemType(IType itemType)
		{
			return new DictType(itemType);
		}

		public override bool isAssignableFrom (Context context, IType other)
		{
			return base.isAssignableFrom(context, other) 
				       || (other is DictType && itemType.isAssignableFrom (context, ((DictType)other).GetItemType ()));
		}


		public override Type ToCSharpType (Context context)
		{
			Type elemType = this.itemType.ToCSharpType (context);
			Type[] types = new Type[] { typeof(String), elemType };
			Type type = typeof(IDictionary<,>).MakeGenericType (types);
			return type;
		}

    
		public override IType checkMember (Context context, String name)
		{
			switch (name)
			{
				case "count":
					return IntegerType.Instance;
				case "keys":
					return new SetType(TextType.Instance);
				case "values":
					return new ListType(GetItemType());
				default:
					return base.checkMember(context, name);
			}
		}

        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
			if (name == "swap")
			{
				ISet<IMethodDeclaration> methods = new HashSet<IMethodDeclaration>();
				methods.Add(SWAP_METHOD);
				return methods;
			}
			else if (name == "remove")
			{
				ISet<IMethodDeclaration> methods = new HashSet<IMethodDeclaration>();
				methods.Add(REMOVE_METHOD);
				return methods;
			}
			else
				return base.getMemberMethods(context, name);
        }

		internal static IParameter KEY_ARGUMENT = new CategoryParameter(TextType.Instance, "key");

		internal static IMethodDeclaration REMOVE_METHOD = new RemoveMethodDeclaration();
		internal static IMethodDeclaration SWAP_METHOD = new SwapMethodDeclaration();

		public override bool Equals (Object obj)
		{
			if (obj == this)
				return true; 
			if (obj == null)
				return false;
			if (!(obj is DictType))
				return false;
			DictType other = (DictType)obj;
			return this.GetItemType ().Equals (other.GetItemType ());
		}

	
		public override IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if (other is DictType
			  && this.GetItemType ().Equals (((DictType)other).GetItemType ()))
				return this;
			else
				return base.checkAdd (context, other, tryReverse);
		}


		public override void checkContains (Context context, IType other)
		{
			if (other == TextType.Instance)
				return;
			else
				base.checkContains (context, other);
		}


		public override IType checkItem (Context context, IType other)
		{
			if (other == TextType.Instance)
				return itemType;
			else
				return base.checkItem (context, other);
		}


		public override IType checkIterator (Context context)
		{
			return new EntryType (itemType);
		}

		public override void checkContainsAllOrAny(Context context, IType other)
		{
			// nothing to do
		}
	
	}

	class SwapMethodDeclaration : BuiltInMethodDeclaration
	{

		public SwapMethodDeclaration()
		: base("swap")
		{ }

		public override IValue interpret(Context context)
		{
			DictValue dict = (DictValue)getValue(context);
			return dict.Swap(context);
		}



		public override IType check(Context context)
		{
			return new DictType(TextType.Instance);
		}

	};

	class RemoveMethodDeclaration : BuiltInMethodDeclaration
	{

		public RemoveMethodDeclaration()
		: base("remove", DictType.KEY_ARGUMENT)
		{ }

		public override IValue interpret(Context context)
		{
			DictValue dict = (DictValue)getValue(context);
			if (!dict.IsMutable())
				throw new NotMutableError();
			TextValue key = (TextValue)context.getValue("key");
			dict.Remove(key);
			return null;
		}



		public override IType check(Context context)
		{
			return VoidType.Instance;
		}

	};

}

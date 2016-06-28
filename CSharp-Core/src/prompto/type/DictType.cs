using prompto.runtime;
using System;
using prompto.value;
using System.Collections.Generic;


namespace prompto.type
{

	public class DictType : ContainerType
	{
	
		public DictType (IType itemType)
			: base (itemType.GetName () + "{}", itemType)
		{
			this.itemType = itemType;
		}


		public override bool isAssignableTo (Context context, IType other)
		{
			return (other is DictType) && itemType.isAssignableTo (context, ((DictType)other).GetItemType ());
		}


		public override Type ToCSharpType ()
		{
			Type elemType = this.itemType.ToCSharpType ();
			Type[] types = new Type[] { typeof(String), elemType };
			Type type = typeof(IDictionary<,>).MakeGenericType (types);
			return type;
		}

    
		public override IType checkMember (Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else if ("keys" == name)
				return new SetType (TextType.Instance);
			else if ("values" == name)
				return new ListType (GetItemType ());
			else
				return base.checkMember (context, name);
		}


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


		public override IType checkContains (Context context, IType other)
		{
			if (other == TextType.Instance)
				return BooleanType.Instance;
			else
				return base.checkContains (context, other);
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
	
	}

}

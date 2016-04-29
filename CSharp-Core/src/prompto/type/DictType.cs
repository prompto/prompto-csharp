using prompto.runtime;
using System;
using prompto.value;
namespace prompto.type
{

public class DictType : ContainerType {
	
	public DictType(IType itemType) 
    	: base(itemType.GetName()+"{}",itemType)
	{
		this.itemType = itemType;
	}
	
	override
	public bool isAssignableTo(Context context, IType other) {
		return (other is DictType) && itemType.isAssignableTo(context, ((DictType)other).GetItemType());
	}

	override
	public Type ToCSharpType() {
        return typeof(Dict);
	}

    
		public override IType checkMember(Context context, String name)
    {
        if ("length" == name)
            return IntegerType.Instance;
        else if("keys" == name)
            return new ListType(TextType.Instance);
        else if ("values" == name)
            return new ListType(GetItemType());
        else
            return base.checkMember(context, name);
    }
    
    override
	public bool Equals(Object obj) {
		if(obj==this)
			return true; 
		if(obj==null)
			return false;
		if(!(obj is DictType))
			return false;
		DictType other = (DictType)obj;
		return this.GetItemType().Equals(other.GetItemType());
	}
	
	
	public override IType checkAdd(Context context, IType other, bool tryReverse) {
		if(other is DictType 
			&& this.GetItemType().Equals(((DictType)other).GetItemType()))
			return this;
		else
				return base.checkAdd(context, other, tryReverse);
	}
	
	override
	public IType checkContains(Context context, IType other) {
		if(other==TextType.Instance)
			return BooleanType.Instance;
		else
			return base.checkContains(context, other);
	}
	
	override
	public IType checkItem(Context context, IType other) {
		if(other==TextType.Instance)
			return itemType;
		else
			return base.checkItem(context,other);
	}
	
	override
	public IType checkIterator(Context context) {
		return new EntryType(itemType);
	}
	
}

}

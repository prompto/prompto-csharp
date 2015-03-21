using System;
using presto.parser;
using presto.runtime;
using presto.type;
using presto.utils;

namespace presto.declaration
{

public abstract class BaseDeclaration : Section, IDeclaration {

	protected String name;
	
	protected BaseDeclaration(String name) {
		this.name = name;
	}
		
	public String getName() {
		return name;
	}

    public abstract IType check(Context context);
    public abstract IType GetType(Context context);
    public abstract void register(Context context);
    public abstract void ToDialect(CodeWriter writer);
}

}
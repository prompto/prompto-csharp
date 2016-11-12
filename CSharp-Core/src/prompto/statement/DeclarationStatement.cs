using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.declaration;
using prompto.statement;
using prompto.type;
using prompto.value;
using prompto.utils;

namespace prompto.statement {

    public class DeclarationStatement<T> : BaseStatement where T : IDeclaration
    {

	T declaration;
	
	public DeclarationStatement(T declaration) {
		this.declaration = declaration;
	}

    override
    public void ToDialect(CodeWriter writer)
    {
			try {
				ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
				writer.getContext().registerDeclaration(method);
			} catch(SyntaxError /*e*/) {
				// ok
			}
			declaration.ToDialect(writer);
    }

	
	public T getDeclaration() {
		return declaration;
	}
	
	override public IType check(Context context) {
		if(declaration is ConcreteMethodDeclaration) {
            ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
			method.checkChild(context);
			context.registerDeclaration(method);
		} else
			throw new SyntaxError("Unsupported:" + declaration.GetType().Name);
		return VoidType.Instance;
	}
	
	public override IValue interpret(Context context) {
		if(declaration is ConcreteMethodDeclaration) {
            ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)(object)declaration;
			context.registerDeclaration(method);
				MethodType type = new MethodType(method);
				context.registerValue(new Variable(method.GetName(), type)); 
				context.setValue(method.GetName(), new ClosureValue(context, type));
			return null;
		} else
			throw new SyntaxError("Unsupported:" + declaration.GetType().Name);
	}
	
}


}
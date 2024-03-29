using prompto.parser;
using prompto.runtime;
using System;
using prompto.grammar;
using prompto.type;
using prompto.value;
using prompto.param;

namespace prompto.declaration
{

    public interface IMethodDeclaration : IDeclaration
    {
        IType check(Context context, bool isStart);
        String getProto();
		IType getReturnType();
        ParameterList getParameters();
        String getSignature(Dialect dialect);
        Specificity? computeSpecificity(Context context, IParameter parameter, Argument argument, bool checkInstance);
		IValue interpret(Context local);
        bool isAssignableTo(Context context, ArgumentList arguments, bool useInstance);
        bool isAssignableFrom(Context context, ArgumentList arguments);
        void registerParameters(Context local);
        bool isAbstract();
		bool isTemplate();
		bool isEligibleAsMain();
		void setMemberOf (CategoryDeclaration declaration);
		CategoryDeclaration getMemberOf ();
		IType checkChild(Context context);
        bool IsReference();
        IMethodDeclaration AsReference();
    }
}



using prompto.parser;
using prompto.runtime;
using System;
using prompto.grammar;
using prompto.type;
using prompto.value;
using prompto.argument;

namespace prompto.declaration
{

    public interface IMethodDeclaration : IDeclaration
    {

		IType getReturnType();
        ArgumentList getArguments();
        String getSignature(Dialect dialect);
        String getProto(Context context);
        Specificity? computeSpecificity(Context context, IArgument argument, ArgumentAssignment assignment, bool checkInstance);
		IValue interpret(Context local);
        bool isAssignableTo(Context context, ArgumentAssignmentList assignments, bool useInstance);
        void registerArguments(Context local);
        bool isAbstract();
		bool isTemplate();
		bool isEligibleAsMain();
		void check(CategoryDeclaration decl, Context context);
		void setMemberOf (CategoryDeclaration declaration);
		CategoryDeclaration getMemberOf ();
	}
}



using System.Collections.Generic;
using prompto.grammar;
using prompto.param;
using prompto.parser;
using prompto.runtime;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.declaration {

	public class MethodDeclarationWrapper : Section, IMethodDeclaration {

		IMethodDeclaration wrapped;

		public MethodDeclarationWrapper(IMethodDeclaration wrapped) {
			this.wrapped = wrapped;
		}

		public virtual bool IsReference() {
			return wrapped.IsReference();
		}

		public virtual IMethodDeclaration AsReference()
		{
			return new MethodDeclarationReference(wrapped);
		}

		public void register(Context context) {
			wrapped.register(context);
		}

		public void ToDialect(CodeWriter writer) {
			wrapped.ToDialect(writer);
		}

		public IMethodDeclaration ClosureOf
		{
			set
			{
				wrapped.ClosureOf = value;
			}
			get
			{
				return wrapped.ClosureOf;
			}
		}

		public IList<CommentStatement> Comments
		 {
			get {
				return wrapped.Comments;
			}
			set
            {
				wrapped.Comments = value;
			}
		}

		public IList<Annotation> Annotations
		{
			get
			{
				return wrapped.Annotations;
			}
			set
			{
				wrapped.Annotations = value;
			}
		}

		public string GetName() {
			return wrapped.GetName();
		}

		public IType GetIType(Context context) {
			return wrapped.GetIType(context);
		}

		public string getProto() {
			return wrapped.getProto();
		}

		public IType check(Context context)
		{
			return wrapped.check(context);
		}

		public IType check(Context context, bool isStart) {
			return wrapped.check(context, isStart);
		}

		public IType getReturnType() {
			return wrapped.getReturnType();
		}

		public ParameterList getParameters() {
			return wrapped.getParameters();
		}

		public string getSignature(Dialect dialect) {
			return wrapped.getSignature(dialect);
		}

		public bool isAbstract() {
			return wrapped.isAbstract();
		}

		public bool isTemplate() {
			return wrapped.isTemplate();
		}

		public bool isEligibleAsMain() {
			return wrapped.isEligibleAsMain();
		}

		public void setMemberOf(CategoryDeclaration declaration) {
			wrapped.setMemberOf(declaration);
		}

		public CategoryDeclaration getMemberOf() {
			return wrapped.getMemberOf();
		}

		public IValue interpret(Context context) {
			return wrapped.interpret(context);
		}

		public IType checkChild(Context context) {
			return wrapped.checkChild(context);
		}

		public bool isAssignableTo(Context context, ArgumentList assignments, bool checkInstance) {
			return wrapped.isAssignableTo(context, assignments, checkInstance);
		}

		public bool isAssignableFrom(Context context, ArgumentList assignments)
		{
			return wrapped.isAssignableFrom(context, assignments);
		}


		public void registerParameters(Context local) {
			wrapped.registerParameters(local);
		}

		public Specificity? computeSpecificity(Context context, IParameter parameter, Argument argument, bool useInstance) {
			return wrapped.computeSpecificity(context, parameter, argument, useInstance);
		}

	}
}
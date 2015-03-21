using presto.error;
using presto.parser;
using presto.runtime;
using System;
using presto.grammar;
using presto.statement;
using presto.utils;
using presto.type;
using presto.value;

namespace presto.declaration
{

	public abstract class BaseCategoryMethodDeclaration : BaseDeclaration, ICategoryMethodDeclaration
	{

		ArgumentList arguments;
		protected StatementList instructions;

		protected BaseCategoryMethodDeclaration (String name, ArgumentList arguments, StatementList instructions)
			: base (name)
		{
			this.arguments = arguments;
			this.instructions = instructions;
		}

		override
		public void register (Context context)
		{
			// TODO Auto-generated method stub
		
		}

		public abstract IType getReturnType ();

		public ArgumentList getArguments ()
		{
			return arguments;
		}

		public String getSignature (Dialect dialect)
		{
			// TODO Auto-generated method stub
			return null;
		}

		public String getProto (Context context)
		{
			// TODO Auto-generated method stub
			return null;
		}

		public Specificity? computeSpecificity (Context context, IArgument argument,
		                                      ArgumentAssignment assignment, bool checkInstance)
		{
			// TODO Auto-generated method stub
			return null;
		}

		public bool isAssignableTo (Context context,
		                          ArgumentAssignmentList assignments, bool checkInstance)
		{
			// TODO Auto-generated method stub
			return false;
		}

		public void registerArguments (Context local)
		{
			// TODO Auto-generated method stub
		
		}

		public bool isEligibleAsMain ()
		{
			// TODO Auto-generated method stub
			return false;
		}

		public abstract void check (ConcreteCategoryDeclaration declaration, Context context);

		public abstract IValue interpret (Context local);
	}


}
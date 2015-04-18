﻿using System;
using presto.grammar;
using presto.type;
using presto.expression;
using presto.statement;
using presto.runtime;
using presto.utils;

namespace presto.declaration
{
	public class OperatorMethodDeclaration : ConcreteMethodDeclaration, IExpression, ICategoryMethodDeclaration 
	{

		Operator oper;

		public OperatorMethodDeclaration(Operator oper, IArgument arg, IType returnType, StatementList stmts)
			: base("operator_" + oper.ToString(), new ArgumentList(arg), returnType, stmts)
		{
			this.oper = oper;
		}


		protected override void toPDialect(CodeWriter writer) {
			writer.append("def operator ");
			writer.append(Enums.OperatorToString(oper));
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(")");
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("->");
				returnType.ToDialect(writer);
			}
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		protected override void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(Enums.OperatorToString(oper));
			writer.append(" as: operator ");
			arguments.ToDialect(writer);
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("returning: ");
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("doing:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		protected override void toODialect(CodeWriter writer) {
			if(returnType!=null && returnType!=VoidType.Instance) {
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("operator ");
			writer.append(Enums.OperatorToString(oper));
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(") {\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			writer.append("}\n");
		}
			
	}

}

using prompto.error;
using prompto.runtime;
using System.Collections.Generic;
using System;
using Boolean = prompto.value.Boolean;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

    public class IfStatement : BaseStatement
    {

        IfElementList elements = new IfElementList();
 
		public IfStatement(IExpression condition, StatementList statements)
        {
			elements.Add(new IfElement(condition, statements));
        }

		public IfStatement(IExpression condition, StatementList statements, IfElementList elseIfs, StatementList elseStmts)
        {
			elements.Add(new IfElement(condition, statements));
            if (elseIfs != null)
                elements.AddRange(elseIfs);
            if (elseStmts != null)
                elements.Add(new IfElement(null, elseStmts));
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}


		private void toPDialect(CodeWriter writer) {
			bool first = true;
			foreach(IfElement elem in elements) {
				if(!first)
					writer.append("else ");
				elem.ToDialect(writer);
				first = false;
			}
			writer.newLine();
		}


		private void toODialect(CodeWriter writer) {
			bool first = true;
			bool curly = false;
			foreach(IfElement elem in elements) {
				if(!first) {
					if(curly)
						writer.append(" ");
					writer.append("else ");
				}
				curly = elem.statements.Count>1;
				elem.ToDialect(writer);
				first = false;
			}
			writer.newLine();
		}


		private void toEDialect(CodeWriter writer) {
			bool first = true;
			foreach(IfElement elem in elements) {
				if(!first)
					writer.append("else ");
				elem.ToDialect(writer);
				first = false;
			}
			writer.newLine();
		}
        public void addAdditional(IExpression condition, StatementList instructions)
        {
            elements.Add(new IfElement(condition, instructions));
        }

        public void setFinal(StatementList instructions)
        {
            elements.Add(new IfElement(null, instructions));
        }

        override
        public IType check(Context context)
        {
            return elements[0].check(context);
            // TODO check consistency with additional elements
        }

        override
		public IValue interpret(Context context) {
		foreach(IfElement element in elements) {
			IExpression condition = element.getCondition();
			IValue test = condition==null ? Boolean.TRUE : condition.interpret(context);
			if(test == Boolean.TRUE)
				return element.interpret(context);
		}
		return null;
	}

   

    }

    public class IfElement : BaseStatement
    {

        IExpression condition;
        public StatementList statements;

		public IfElement(IExpression condition, StatementList statements)
        {
            this.condition = condition;
			this.statements = statements;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}

		public void toPDialect(CodeWriter writer) {
			toEDialect(writer);
		}

		public void toEDialect(CodeWriter writer) {
			if(condition!=null) {
				writer.append("if ");
				condition.ToDialect(writer);
			}
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();	
		}

		public void toODialect(CodeWriter writer) {
			if(condition!=null)
			{
				writer.append("if (");
				condition.ToDialect(writer);
				writer.append(") ");
			}
			bool curly = statements!=null && statements.Count>1;
			if(curly) 
				writer.append("{\n");
			else 
				writer.newLine();
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			if(curly) 
				writer.append("}");
		}        
        public IExpression getCondition()
        {
            return condition;
        }

        public StatementList getInstructions()
        {
            return statements;
        }

        override
        public IType check(Context context)
        {
            IType cond = condition.check(context);
            if (cond != BooleanType.Instance)
                throw new SyntaxError("Expected a bool condition!");
			context = downCast(context, false);
			return statements.check(context, null);
        }

        override
        public IValue interpret(Context context)
        {
			context = downCast(context, true);
			return statements.interpret(context);
		}

		private Context downCast(Context context, bool setValue) {
			Context parent = context;
			if(condition is EqualsExpression)
				context = ((EqualsExpression)condition).downCast(context, setValue);
			context = parent!=context ? context : context.newChildContext();
			return context;
		}

	}

    	public class IfElementList : List<IfElement> {

		public IfElementList() {
			
		}
		
		public IfElementList(IfElement elem) {
			this.Add(elem);
		}

		
	}

}
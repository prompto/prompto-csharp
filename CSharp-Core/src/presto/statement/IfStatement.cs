using presto.error;
using presto.runtime;
using System.Collections.Generic;
using System;
using Boolean = presto.value.Boolean;
using presto.parser;
using presto.type;
using presto.expression;
using presto.utils;
using presto.value;

namespace presto.statement
{

    public class IfStatement : BaseStatement
    {

        IfElementList elements = new IfElementList();
 
        public IfStatement(IExpression condition, StatementList instructions)
        {
            elements.Add(new IfElement(condition, instructions));
        }

        public IfStatement(IExpression condition, StatementList instructions, IfElementList elseIfs, StatementList elseStmts)
        {
            elements.Add(new IfElement(condition, instructions));
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
				curly = elem.instructions.Count>1;
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
        public StatementList instructions;

        public IfElement(IExpression condition, StatementList instructions)
        {
            this.condition = condition;
            this.instructions = instructions;
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
			instructions.ToDialect(writer);
			writer.dedent();	
		}

		public void toODialect(CodeWriter writer) {
			if(condition!=null)
			{
				writer.append("if (");
				condition.ToDialect(writer);
				writer.append(") ");
			}
			bool curly = instructions!=null && instructions.Count>1;
			if(curly) 
				writer.append("{\n");
			else 
				writer.newLine();
			writer.indent();
			instructions.ToDialect(writer);
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
            return instructions;
        }

        override
        public IType check(Context context)
        {
            IType cond = condition.check(context);
            if (cond != BooleanType.Instance)
                throw new SyntaxError("Expected a bool condition!");
			context = downCast(context, false);
			return instructions.check(context);
        }

        override
        public IValue interpret(Context context)
        {
			context = downCast(context, true);
			return instructions.interpret(context);
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
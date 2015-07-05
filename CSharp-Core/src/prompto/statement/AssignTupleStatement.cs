using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.grammar;
using prompto.expression;
using prompto.utils;

namespace prompto.statement
{

	public class AssignTupleStatement : SimpleStatement
    {

        IdentifierList names = new IdentifierList();
        IExpression expression;
   
        public AssignTupleStatement(String name)
        {
            this.names.Add(name);
        }

        public AssignTupleStatement(IdentifierList names, IExpression expression)
        {
            this.names = names;
            this.expression = expression;
        }

        /* for unified grammar */
        public void add(String i1)
        {
            this.Add(i1);
        }
  
        override
		public void ToDialect(CodeWriter writer)
        {
			names.ToDialect(writer, false);
			writer.append(" = ");
			expression.ToDialect(writer);
	    }

        public void Add(String i1)
        {
            this.names.Add(i1);
        }

        public IdentifierList getNames()
        {
            return names;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression getExpression()
        {
            return expression;
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is AssignTupleStatement))
                return false;
            AssignTupleStatement other = (AssignTupleStatement)obj;
            return this.getNames().Equals(other.getNames())
                    && this.getExpression().Equals(other.getExpression());
        }

        override
        public IType check(Context context)
        {
            IType type = expression.check(context);
            if (type != TupleType.Instance)
				throw new SyntaxError("Expecting a tuple expression, got " + type.GetName());
            foreach (String name in names)
            {
                INamed actual = context.getRegistered(name);
                if (actual == null)
                {
					IType actualType = expression.check(context);
					context.registerValue(new Variable(name, actualType));
                }
                else
                {
                    // need to check type compatibility
                    IType actualType = actual.GetType(context);
                    IType newType = expression.check(context);
                    newType.checkAssignableTo(context, actualType);
                }
            }
            return VoidType.Instance;
        }


        override
        public IValue interpret(Context context)
        {
			IValue o = expression.interpret(context);
            if (!(o is TupleValue))
                throw new SyntaxError("Expecting a tuple expression, got " + o.GetType().Name);
            TupleValue tuple = (TupleValue)o;
            for (int i = 0; i < names.Count; i++)
            {
                String name = names[i];
                IValue value = tuple[i];
				if (context.getRegisteredValue<INamed>(name) == null)
					context.registerValue(new Variable(name, value.GetType(context)));
                context.setValue(name, value);
            }
            return null;
        }

    }
}

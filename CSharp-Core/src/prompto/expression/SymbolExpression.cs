using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.value;
using prompto.type;
using prompto.grammar;
using prompto.utils;

namespace prompto.expression
{

    public class SymbolExpression : BaseExpression, IExpression
    {

        String name;

        public SymbolExpression(String name)
        {
            this.name = name;
        }

  
        public String getName()
        {
            return name;
        }

        
        public override String ToString()
        {
            return name;
        }

        public override void ToDialect(CodeWriter writer)
        {
			writer.append(name);
       }
       

        public override IType check(Context context)
        {
            Symbol symbol = context.getRegisteredValue<Symbol>(name);
            if (symbol == null)
                throw new SyntaxError("Unknown symbol:" + name);
            return symbol.check(context);
        }

        public override IValue interpret(Context context)
        {
            Symbol symbol = context.getRegisteredValue<Symbol>(name);
            if (symbol == null)
                throw new SyntaxError("Unknown symbol:" + name);
            return symbol.interpret(context);
        }

    }
}

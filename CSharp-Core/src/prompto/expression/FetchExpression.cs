using prompto.parser;
using System;
using prompto.runtime;
using prompto.error;
using Boolean = prompto.value.Boolean;
using prompto.type;
using prompto.value;
using prompto.grammar;
using prompto.utils;
using System.Collections;

namespace prompto.expression
{

    public class FetchExpression : Section, IExpression
    {

        String itemName;
        IExpression source;
        IExpression filter;

        public FetchExpression(String name, IExpression source, IExpression filter)
        {
            this.itemName = name;
            this.source = source;
            this.filter = filter;
        }


        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append("fetch any ");
				writer.append(itemName);
				break;
			case Dialect.O:
				writer.append("fetch (");
				writer.append(itemName);
				writer.append(")");
				break;
			case Dialect.S:
				writer.append("fetch ");
				writer.append(itemName);
				break;
			}
			writer.append(" from ");
			source.ToDialect(writer);
			writer.append(" where ");
			filter.ToDialect(writer);        
		}

        public IType check(Context context)
        {
            IType listType = source.check(context);
			if (!(listType is ContainerType))
                throw new SyntaxError("Expecting a list type as data source !");
            Context local = context.newLocalContext();
			IType itemType = ((ContainerType)listType).GetItemType ();
			local.registerValue(new Variable(itemName, itemType));
            IType filterType = filter.check(local);
            if (filterType != BooleanType.Instance)
                throw new SyntaxError("Filtering expresion must return a bool !");
			return new ListType(itemType);
        }

        public IValue interpret(Context context)
        {
			IValue list = source.interpret(context);
            if (list == null)
                throw new NullReferenceError();
			if (!(list is IContainer))
                throw new InternalError("Illegal fetch source: " + source);
            IType listType = source.check(context);
			if (!(listType is ContainerType))
				throw new InternalError("Illegal source type: " + listType.GetName());
			IType itemType = ((ContainerType)listType).GetItemType();
			ListValue result = new ListValue(itemType);
            Context local = context.newLocalContext();
            Variable item = new Variable(itemName, itemType);
            local.registerValue(item);
			foreach (IValue o in ((IContainer)list).GetEnumerable(context))
            {
                local.setValue(itemName, o);
                Object test = filter.interpret(local);
                if (!(test is Boolean))
                    throw new InternalError("Illegal test result: " + test);
                if (((Boolean)test).Value)
                    result.Add(o);
            }
            return result;
        }
    }
}

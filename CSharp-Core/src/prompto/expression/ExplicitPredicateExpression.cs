using System;
using prompto.grammar;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{


    public class ExplicitPredicateExpression : PredicateExpression, IExpression
    {

        String itemName;
        IExpression predicate;


        public ExplicitPredicateExpression(String itemName, IExpression predicate)
        {
            this.itemName = itemName;
            this.predicate = predicate;
        }


        public override ArrowExpression ToArrowExpression()
        {
            ArrowExpression arrow = new ArrowExpression(new IdentifierList(itemName), null, null);
            arrow.Expression = predicate;
            return arrow;
        }


        public override String ToString()
        {
            return "" + itemName + " where " + predicate.ToString();
        }


        public override void FilteredToDialect(CodeWriter writer, IExpression source)
        {
            writer = writer.newChildWriter();
            IType sourceType = source.check(writer.getContext());
            IType itemType = ((IterableType)sourceType).GetItemType();
            writer.getContext().registerValue(new Variable(itemName, itemType));
            switch (writer.getDialect())
            {
                case Dialect.E:
                case Dialect.M:
                    source.ToDialect(writer);
                    writer.append(" filtered with ")
                        .append(itemName)
                        .append(" where ");
                    predicate.ToDialect(writer);
                    break;
                case Dialect.O:
                    writer.append("filtered (");
                    source.ToDialect(writer);
                    writer.append(") with (")
                        .append(itemName)
                        .append(") where (");
                    predicate.ToDialect(writer);
                    writer.append(")");
                    break;
            }
        }

        public override void ContainsToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                case Dialect.M:
                    writer.append(" ")
                        .append(itemName)
                        .append(" where ");
                    predicate.ToDialect(writer);
                    break;
                case Dialect.O:
                    writer.append(" (")
                        .append(itemName)
                        .append(") where (");
                    predicate.ToDialect(writer);
                    writer.append(")");
                    break;
            }

        }


        public override IType Check(Context context, IType itemType)
        {
            Context child = context.newChildContext();
            child.registerValue(new Variable(itemName, itemType));
            return predicate.check(child);
        }


        public override IType check(Context context)
        {
            return ToArrowExpression().check(context);
        }


        public override IValue interpret(Context context)
        {
            return ToArrowExpression().interpret(context);
        }


        public override void ToDialect(CodeWriter writer)
        {
            throw new Exception();
        }




    }
}

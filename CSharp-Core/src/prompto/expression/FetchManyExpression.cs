using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.declaration;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.store;
using prompto.grammar;
using System.Collections.Generic;

namespace prompto.expression
{

    public class FetchManyExpression : BaseExpression, IExpression
    {

        protected CategoryType type;
        IExpression predicate;
        IExpression first;
        IExpression last;
        protected List<string> include;
        OrderByClauseList orderBy;

        public FetchManyExpression(CategoryType type, IExpression predicate, IExpression first, IExpression last, List<string> include, OrderByClauseList orderBy)
        {
            this.type = type;
            this.predicate = predicate;
            this.first = first;
            this.last = last;
            this.include = include;
            this.orderBy = orderBy;
        }


        public override void ToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    ToMDialect(writer);
                    break;
            }
        }

        private void ToMDialect(CodeWriter writer)
        {
            writer.append("fetch ");
            if (first != null)
            {
                writer.append("rows ");
                first.ToDialect(writer);
                writer.append(" to ");
                last.ToDialect(writer);
                writer.append(" ");
            }
            else
                writer.append("all ");
            writer.append("( ");
            if (type != null)
            {
                if (type.Mutable)
                    writer.append("mutable ");
                writer.append(type.GetTypeName());
                writer.append(" ");
            }
            writer.append(") ");
            if (predicate != null)
            {
                writer.append("where ");
                predicate.ToDialect(writer);
                writer.append(" ");
            }
            if (include != null)
            {
                writer.append(" include ");
                foreach(string name in include)
                {
                    writer.append(name).append(", ");
                }
                writer.trimLast(", ".Length);
            }
            if (orderBy != null)
                orderBy.ToDialect(writer);
        }


        private void ToODialect(CodeWriter writer)
        {
            writer.append("fetch ");
            if (first == null)
                writer.append("all ");
            if (type != null)
            {
                writer.append("( ");
                if (type.Mutable)
                    writer.append("mutable ");
                writer.append(type.GetTypeName().ToString());
                writer.append(" ) ");
            }
            if (first != null)
            {
                writer.append("rows ( ");
                first.ToDialect(writer);
                writer.append(" to ");
                last.ToDialect(writer);
                writer.append(") ");
            }
            if (predicate != null)
            {
                writer.append(" where ( ");
                predicate.ToDialect(writer);
                writer.append(") ");
            }
            if (include != null)
            {
                writer.append(" include (");
                foreach (string name in include)
                {
                    writer.append(name).append(", ");
                }
                writer.trimLast(", ".Length);
                writer.append(")");
            }
            if (orderBy != null)
                orderBy.ToDialect(writer);
        }


        private void ToEDialect(CodeWriter writer)
        {
            writer.append("fetch");
            if (first == null)
                writer.append(" all");
            if (type != null)
            {
                writer.append(" ");
                if (type.Mutable)
                    writer.append("mutable ");
                writer.append(type.GetTypeName());
            }
            if (first != null)
            {
                writer.append(" ");
                first.ToDialect(writer);
                writer.append(" to ");
                last.ToDialect(writer);
            }
            if (predicate != null)
            {
                writer.append(" where ");
                predicate.ToDialect(writer);
            }
            if (include != null)
            {
                writer.append(" include ");
                if (include.Count == 1)
                    writer.append(include[0]);
                else { 
                    for (int i = 0; i < include.Count - 1; i++)
                    {
                        writer.append(include[i]).append(", ");
                    }
                    writer.trimLast(", ".Length);
                    writer.append(" and ").append(include[include.Count - 1]);
                }
            }
            if (orderBy != null)
            {
                writer.append(" ");
                orderBy.ToDialect(writer);
            }
        }

        public override IType check(Context context)
        {
            IType type = this.type;
            if (type == null)
                type = AnyType.Instance;
            else
            {
                CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
                if (decl == null)
                    throw new SyntaxError("Unknown category: " + type.GetTypeName().ToString());
            }
            checkPredicate(context);
            checkInclude(context);
            checkOrderBy(context);
            checkSlice(context);
            return new CursorType(type);
        }

        public void checkInclude(Context context)
        {
            // TODO
        }

        public void checkOrderBy(Context context)
        {
            // TODO
        }


        public void checkSlice(Context context)
        {
            // TODO
        }

        public void checkPredicate(Context context)
        {
            if (predicate == null)
                return;
            if (!(predicate is IPredicateExpression))
                throw new SyntaxError("Filtering expression must be a predicate !");
            IType filterType = predicate.check(context);
            if (filterType != BooleanType.Instance)
                throw new SyntaxError("Filtering expresion must return a boolean !");
        }

        public override IValue interpret(Context context)
        {
            IStore store = DataStore.Instance;
            IQuery query = buildFetchManyQuery(context, store);
            IStoredEnumerable docs = store.FetchMany(query);
            IType type = this.type != null ? (IType)this.type : AnyType.Instance;
            return new CursorValue(context, type, docs);
        }


        private IQuery buildFetchManyQuery(Context context, IStore store)
        {
            IQueryBuilder builder = store.NewQueryBuilder();
            if (type != null)
            {
                AttributeInfo info = new AttributeInfo("category", TypeFamily.TEXT, true, null);
                builder.Verify(info, MatchOp.CONTAINS, type.GetTypeName());
            }
            if (predicate != null)
            {
                if (!(predicate is IPredicateExpression))
                    throw new SyntaxError("Filtering expression must be a predicate !");
                ((IPredicateExpression)predicate).interpretQuery(context, builder);
            }
            if (type != null && predicate != null)
                builder.And();
            builder.SetFirst(InterpretLimit(context, first));
            builder.SetLast(InterpretLimit(context, last));
            if (include != null)
                builder.Project(include);
            if (orderBy != null)
                orderBy.interpretQuery(context, builder);
            return builder.Build();
        }

        private long? InterpretLimit(Context context, IExpression exp)
        {
            if (exp == null)
                return null;
            IValue value = exp.interpret(context);
            if (!(value is prompto.value.IntegerValue))
                throw new InvalidValueError("Expecting an Integer, found:" + value.GetIType().GetTypeName());
            return ((prompto.value.IntegerValue)value).LongValue;
        }



    }
}

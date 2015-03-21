using presto.parser;
using presto.expression;

namespace presto.statement
{

    public interface IStatement : IExpression, ISection
    {
    }

}
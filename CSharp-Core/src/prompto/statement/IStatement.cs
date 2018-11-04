using prompto.parser;
using prompto.expression;

namespace prompto.statement
{

    public interface IStatement : IExpression, ISection
    {
		bool CanReturn { get; }
		bool IsSimple { get; }
    }

}
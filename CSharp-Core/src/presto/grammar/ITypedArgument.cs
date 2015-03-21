using presto.type;
namespace presto.grammar
{

    public interface ITypedArgument : IArgument
    {

        IType getType();

    }
}

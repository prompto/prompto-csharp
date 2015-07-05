using prompto.type;
namespace prompto.grammar
{

    public interface ITypedArgument : IArgument
    {

        IType getType();

    }
}

using prompto.type;
using prompto.argument;


namespace prompto.argument
{

    public interface ITypedArgument : IArgument
    {

        IType getType();

    }
}

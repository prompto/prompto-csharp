using prompto.type;


namespace prompto.param
{

    public interface ITypedParameter : IParameter
    {

        IType getType();

    }
}

namespace prompto.grammar
{

    public interface IAssignableSelector : IAssignableInstance
    {
        void SetParent(IAssignableInstance parent);
    }
}
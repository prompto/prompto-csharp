namespace prompto.instance
{

    public interface IAssignableSelector : IAssignableInstance
    {
        void SetParent(IAssignableInstance parent);
    }
}
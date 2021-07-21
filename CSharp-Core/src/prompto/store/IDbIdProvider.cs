namespace prompto.store
{
    public delegate object IDbIdProvider();
    public delegate void IDbIdListener(object dbId);

    public interface IDbIdFactory
    {
        IDbIdProvider Provider { get; }
        IDbIdListener Listener { get; }
        bool IsUpdate();
    }

}

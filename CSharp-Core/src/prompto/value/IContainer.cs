using prompto.runtime;

namespace prompto.value
{
	public interface IContainer : IIterable
    {
		bool HasItem(Context context, IValue item);
   }
}

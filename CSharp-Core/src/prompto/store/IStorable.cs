using prompto.value;

namespace prompto.store
{

	public interface IStorable
	{
		bool Dirty { get; set; }
		Document asDocument ();
	}

}

namespace prompto.store
{

	public interface IStorable
	{

		object GetOrCreateDbId();
		bool Dirty { get; set; }
		void SetData(string attrName, object value);

	}

}
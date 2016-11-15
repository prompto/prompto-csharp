using System.Collections.Generic;

namespace prompto.store
{
	public interface IStoredEnumerable : IEnumerable<IStored> {

		long Length { get; }
		long TotalLength{ get; }
		new IStoredEnumerator GetEnumerator();
	}

	public interface IStoredEnumerator : IEnumerator<IStored>
	{

		long Length { get; }
		long TotalLength { get; }

	}

}

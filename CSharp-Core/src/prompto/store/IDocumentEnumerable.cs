using System.Collections.Generic;
using prompto.value;

namespace prompto.store
{

	public interface IDocumentEnumerable : IEnumerable<Document>
	{

		long Length { get; }

	}


	public interface IDocumentEnumerator : IEnumerator<Document>
	{

		long Length { get; }

	}
}
using prompto.runtime;
using prompto.declaration;

namespace prompto.expression
{

	public interface IAssertion
	{

		bool interpretAssert (Context context, TestMethodDeclaration testMethodDeclaration);

	}
}
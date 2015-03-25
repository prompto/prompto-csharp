using presto.runtime;
using presto.declaration;

namespace presto.expression
{

	public interface IAssertion
	{

		bool interpretAssert (Context context, TestMethodDeclaration testMethodDeclaration);

	}
}
using prompto.runtime;
using prompto.type;
using prompto.utils;

namespace prompto.jsx
{

	public interface IJsxValue
	{

		IType check(Context context);
		void ToDialect(CodeWriter writer);

	}
}
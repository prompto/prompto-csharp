using prompto.runtime;
using prompto.type;
using prompto.utils;

namespace prompto.jsx
{

	public interface IJsxValue
	{

		IType check(Context context);
		IType checkProto(Context context, MethodType type);
		bool IsLiteral();
		void ToDialect(CodeWriter writer);

	}
}
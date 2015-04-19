
using presto.runtime;
using System;
using presto.type;

namespace presto.grammar {

public interface INamed {
	String GetName();
	IType GetType(Context context);
}

}
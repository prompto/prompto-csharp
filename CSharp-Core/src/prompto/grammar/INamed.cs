
using prompto.runtime;
using System;
using prompto.type;

namespace prompto.grammar {

public interface INamed {
	String GetName();
	IType GetIType(Context context);
}

}
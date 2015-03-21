using presto.parser;
using presto.runtime;
using System;

namespace presto.debug {

public class StackFrame {
	
	String methodName;
	String path;
	int line;
	int charStart;
	int charEnd;
	
	public StackFrame(Context context, String methodName, ISection section) {
		this.methodName = methodName;
		this.path = section.Path;
		this.line = section.Start.Line;
		this.charStart = section.Start.Index;
		this.charEnd = section.End.Index;
	}
	
	public String getMethodName() {
		return methodName;
	}
	
	public String getPath() {
		return path;
	}
	
	public int getLine() {
		return line;
	}
	
	public int getCharEnd() {
		return charEnd;
	}
	
	public int getCharStart() {
		return charStart;
	}
	
	override public String ToString() {
		return methodName + ", line " + line;
	}
	
	override public bool Equals(Object obj) {
		if(obj==this)
			return true;
		if(!(obj is StackFrame))
			return false;
		StackFrame sf = (StackFrame)obj;
		return this.methodName.Equals(sf.methodName)
				&& this.path.Equals(sf.path)
				&& this.line==sf.line
				&& this.charStart==sf.charStart
				&& this.charEnd==sf.charEnd;
	}
}

}
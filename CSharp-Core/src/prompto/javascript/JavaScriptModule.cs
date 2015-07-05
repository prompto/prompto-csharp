using System.Collections.Generic;
using System;
using prompto.utils;

namespace prompto.javascript {

public class JavaScriptModule {

	List<String> identifiers;
	
		public JavaScriptModule(List<String> identifiers) {
		this.identifiers = identifiers;
	}

	public void toDialect(CodeWriter writer) {
		writer.append(" from module: ");
		foreach(String id in identifiers) {
			if("js"==id) {
				writer.trimLast(1);
				writer.append('.');
			}
			writer.append(id);
			writer.append('/');
		}
		writer.trimLast(1);
	}

}

}
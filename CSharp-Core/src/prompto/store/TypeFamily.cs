﻿using System;

namespace prompto.store
{
	public enum TypeFamily
	{
		// storable
		BOOLEAN,
		CHARACTER,
		INTEGER,
		DECIMAL,
		TEXT,
		UUID,
		DATE,
		TIME,
		DATETIME,
		PERIOD,
		VERSION,
		LIST,
		SET,
		TUPLE,
		RANGE,
		BLOB,
		IMAGE,
		DOCUMENT,
		CATEGORY,
		RESOURCE,
		DICTIONARY,
		ENUMERATED,
		// non storable
		VOID,
		NULL,
		DBID,
		ANY,
		METHOD,
		CURSOR,
		ITERATOR,
		PROPERTIES,
		CLASS,
		TYPE,
		CODE,
		JSX,
		CSS,
		HTML,
		// volatile
		MISSING
	}


}

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
		ANY,
		METHOD,
		CURSOR,
		ITERATOR,
		CLASS,
		TYPE,
		CODE,
		// volatile
		MISSING
	}


}
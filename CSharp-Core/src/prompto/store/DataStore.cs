using System;
using System.Collections.Generic;
using prompto.expression;
using prompto.grammar;
using prompto.runtime;
using prompto.type;

namespace prompto.store
{
	public abstract class DataStore 
	{
		static IStore instance = new MemStore();

		public static IStore Instance
		{
			get
			{
				return instance;
			}
			set
			{
				instance = value;
			}
		}

	}
}

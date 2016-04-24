﻿using System;
using prompto.runtime;


namespace prompto.type
{

	public abstract class BinaryType : NativeType
	{

		protected BinaryType (String typeName)
			: base (typeName)
		{
		}

	
		public override IType CheckMember (Context context, String name)
		{
			if ("name" == name)
				return TextType.Instance;
			else if ("format" == name)
				return TextType.Instance;
			else
				return base.CheckMember (context, name);
		}

		public override Type ToCSharpType ()
		{
			throw new NotImplementedException ();
		}
	
	}
}
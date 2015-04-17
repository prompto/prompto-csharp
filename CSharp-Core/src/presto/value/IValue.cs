using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.runtime;
using presto.type;

namespace presto.value
{
    public interface IValue
    {
		bool IsMutable();
		IType GetType(Context context);
		IValue Add(Context context, IValue value);
        IValue Subtract(Context context, IValue iValue);
        IValue Multiply(Context context, IValue iValue);
        IValue Divide(Context context, IValue iValue);
        IValue IntDivide(Context context, IValue iValue);
        IValue Modulo(Context context, IValue iValue);
		IValue GetMember(Context context, String attrName);
		void SetMember(Context context, String attrName, IValue value);
        Int32 CompareTo(Context context, IValue value);
        Object ConvertTo(Type type);
		bool Equals(Context context, IValue value);
		bool Roughly(Context context, IValue value);
   }
}

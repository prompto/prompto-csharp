using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;

namespace prompto.value
{
    public abstract class BaseValue : IValue
    {
		protected IType type;

		protected BaseValue(IType type) {
			this.type = type;
		}

		public virtual bool IsMutable()
		{
			return false;
		}

		public virtual IType GetIType() {
			return this.type;
		}

		public virtual IValue Add(Context context, IValue value)
        {
            throw new NotSupportedException("Add not supported by " + this.GetType().Name);
        }

        public virtual IValue Subtract(Context context, IValue value)
        {
            throw new NotSupportedException("Subtract not supported by " + this.GetType().Name);
        }

        public virtual IValue Multiply(Context context, IValue value)
        {
            throw new NotSupportedException("Multiply not supported by " + this.GetType().Name);
        }

        public virtual IValue Divide(Context context, IValue value)
        {
            throw new NotSupportedException("Divide not supported by " + this.GetType().Name);
        }

        public virtual IValue IntDivide(Context context, IValue value)
        {
            throw new NotSupportedException("IntegerDivide not supported by " + this.GetType().Name);
        }

        public virtual IValue Modulo(Context context, IValue value)
        {
            throw new NotSupportedException("Modulo not supported by " + this.GetType().Name);
        }

        public virtual Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }

		public virtual IValue GetMember(Context context, String name, bool autoCreate)
        {
            throw new NotSupportedException("No member support for " + this.GetType().Name);
        }

		public virtual void SetMember(Context context, String name, IValue value)
		{
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual Object ConvertTo(Type type)
        {
            return this;
        }

		public virtual bool Equals(Context context, IValue rval)
		{
			return this.Equals (rval);
		}

		public virtual bool Roughly(Context context, IValue rval)
		{
			return this.Equals (context, rval);
		}

		public virtual void ToJson (Context context, JsonWriter generator, Object instanceId, String fieldName, Dictionary<String, byte[]> binaries)
		{
			throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
		}


    }
}

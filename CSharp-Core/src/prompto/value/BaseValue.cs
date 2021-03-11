using System;
using System.Collections.Generic;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;
using prompto.store;
using Newtonsoft.Json.Linq;
using System.IO;

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

		public virtual void SetIType(IType type) {
			this.type = type;
		}


		public virtual void CollectStorables(List<IStorable> storables)
		{
			// nothing to do;
		}

		public virtual object GetStorableData()
		{
			throw new NotSupportedException("GetStorableData not supported by " + this.GetType().Name);
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

		public virtual IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
			if ("text" == name)
				return new TextValue(this.ToString());
			else if ("json" == name)
			{
				JToken token = ToJsonToken();
				JsonSerializer serializer = JsonSerializer.CreateDefault();
				StringWriter writer = new StringWriter();
				serializer.Serialize(writer, token);
				return new TextValue(writer.ToString());
			} 
			else
				throw new NotSupportedException("No member support for " + this.GetType().Name);
        }

		public virtual void SetMemberValue(Context context, String name, IValue value)
		{
			throw new NotSupportedException("No member support for " + this.GetType().Name);
		}

		public virtual IValue GetItem(Context context, IValue item)
		{
			throw new NotSupportedException("No item support for " + this.GetType().Name);
		}

		public virtual void SetItem(Context context, IValue item, IValue value)
		{
			throw new NotSupportedException("No item support for " + this.GetType().Name);
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


		public virtual bool Contains(Context context, IValue rval)
		{
			throw new NotSupportedException("No 'contains' support for " + this.GetType().Name);
		}


		public virtual JToken ToJsonToken()
        {
			throw new NotSupportedException("No ToJsonToken support for " + this.GetType().Name);
		}

		public virtual void ToJson (Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries)
		{
			throw new NotSupportedException("No ToJson support for " + this.GetType().Name);
		}

        public virtual IValue ToDocumentValue(Context context)
        {
			return this;
        }

	}
}

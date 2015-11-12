using System.Collections.Generic;
using System;
using prompto.value;
using prompto.runtime;
using prompto.type;


namespace prompto.value
{

    public class Document : BaseValue
    {
        Dictionary<String, IValue> members = new Dictionary<String, IValue>();

		public Document()
			: base (DocumentType.Instance)
		{
		}

		public override bool IsMutable ()
		{
			return true;
		}

		public bool HasMember(String name)
		{
			return members.ContainsKey (name);
		}

		public override IValue GetMember(Context context, String name, bool autoCreate)
		{
            IValue result;
			bool exists = members.TryGetValue (name, out result);
			if(autoCreate && !exists)
            {
                result = new Document();
                members[name] = result;
            }
            return result;
        }


		public override void SetMember(Context context, String name, IValue value)
        {
            members[name] = value;
        }
    }
}

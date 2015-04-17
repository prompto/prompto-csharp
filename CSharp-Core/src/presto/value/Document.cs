using System.Collections.Generic;
using System;
using presto.value;
using presto.runtime;
using presto.type;


namespace presto.value
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

        
		public override IValue GetMember(Context context, String name)
        {
            IValue result;
            if(!members.TryGetValue(name, out result))
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

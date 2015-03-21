using System.Collections.Generic;
using System;
using System.Text;
namespace presto.literal
{

    public class DictEntryList : List<DictEntry>
    {

        public DictEntryList()
        {
        }

        public DictEntryList(DictEntry entry)
        {
            this.Add(entry);
        }

        /* for unified grammar */
        public void add(DictEntry entry)
        {
            this.Add(entry);
        }
        
        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (DictEntry entry in this)
            {
                sb.Append(entry.ToString());
                sb.Append(", ");
            }
            if(sb.Length>2)
                sb.Length = sb.Length - 2;
            sb.Append("}");
            return sb.ToString();
        }

    }

}

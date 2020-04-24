using System;
namespace prompto.runtime
{
    public class ContextFlags
    {
        public static ContextFlags START = new ContextFlags(true, false);
        public static ContextFlags MEMBER = new ContextFlags(false, true);
        public static ContextFlags NONE = new ContextFlags(false, false);

        public ContextFlags(bool start, bool member)
        {
            Start = start;
            Member = member;
        }

        public bool Start { get; set; }
        public bool Member { get; set; }
    }
}

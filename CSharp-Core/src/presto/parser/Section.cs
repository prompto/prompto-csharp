using System;
using Antlr4.Runtime;
namespace presto.parser
{

    public class Section : ISection
    {

        protected Section()
        {
        }

        public void SetFrom(String path, IToken start, IToken end)
        {
            this.Path = path;
            this.Start = new Location(start);
            this.End = new Location(end, true);
        }


        public string Path { set; get; }

        public ILocation Start { set; get; }
  
        public ILocation End { set; get; }
   
        public bool Breakpoint{ set; get; }
      }
}

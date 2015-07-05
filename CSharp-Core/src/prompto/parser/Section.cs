using System;
using Antlr4.Runtime;
namespace prompto.parser
{

    public class Section : ISection
    {

        protected Section()
        {
        }

		public void SetFrom(String path, IToken start, IToken end, Dialect dialect)
        {
            this.Path = path;
            this.Start = new Location(start);
            this.End = new Location(end, true);
			this.Dialect = dialect;
        }


        public string Path { set; get; }

        public ILocation Start { set; get; }
  
        public ILocation End { set; get; }
   
		public Dialect Dialect { set; get; }

		public bool Breakpoint{ set; get; }
      }
}

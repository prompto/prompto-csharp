using System;

namespace prompto.parser
{
    public interface ISection
    {

        String Path { get; }
        ILocation Start { get; }
        ILocation End { get; }
		Dialect Dialect{ get; }
        bool Breakpoint { get; set; }
 
    }
}
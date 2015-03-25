using System;

namespace presto.parser
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
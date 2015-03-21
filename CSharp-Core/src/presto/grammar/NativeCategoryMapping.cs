using System;
using presto.parser;
using presto.utils;


namespace presto.grammar
{

    public abstract class NativeCategoryMapping : IDialectElement
    {
		public abstract void ToDialect (CodeWriter writer);
    }

}
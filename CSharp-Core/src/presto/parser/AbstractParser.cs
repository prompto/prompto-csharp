using System;
using Antlr4.Runtime;
using System.Text;
using System.Collections.Generic;

namespace presto.parser
{

    public abstract class AbstractParser : Parser
    {
        public static int EOF = Eof;

        public AbstractParser(ITokenStream input)
            : base(input)
        {
        }

        public bool isText(IToken token, string text)
        {
            return text == token.Text;
        }

        public bool was(int type)
        {
            return lastHiddenTokenType() == type;
        }

        public bool wasNot(int type)
        {
            return lastHiddenTokenType() != type;
        }

        public bool willBe(int type)
        {
            return this.InputStream.La(1) == type;
        }

        public bool willNotBe(int type)
        {
            return this.InputStream.La(1) != type;
        }

		public virtual int equalToken()
		{
			throw new Exception ("You must override equalToken()!");
		}

		public bool willBeAOrAn() 
		{
			return willBeText("a") || willBeText("an");
		}

		public bool willBeText(string text) {
			return text==this.TokenStream.Lt(1).Text;
		}

        public int nextHiddenTokenType()
        {
            BufferedTokenStream bts = (BufferedTokenStream)this.InputStream;
            IList<IToken> hidden = bts.GetHiddenTokensToRight(bts.Index - 1);
            if (hidden == null || hidden.Count == 0)
                return 0;
            else
                return hidden[0].Type;
        }

        public int lastHiddenTokenType()
        {
            BufferedTokenStream bts = (BufferedTokenStream)this.InputStream;
            IList<IToken> hidden = bts.GetHiddenTokensToLeft(bts.Index);
            if (hidden == null || hidden.Count == 0)
                return 0;
            else
                return hidden[hidden.Count - 1].Type;
        }

    }

}

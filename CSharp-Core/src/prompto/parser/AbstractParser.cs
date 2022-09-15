using System;
using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;

namespace prompto.parser
{

    public abstract class AbstractParser : Parser
    {
        public static int EOF = Eof;
		int WS_TOKEN;

        public AbstractParser(ITokenStream input)
			: this(input, null, null)
        {
        }

		public AbstractParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
            : base(input, output, errorOutput)
		{
			if(this is EParser)
				WS_TOKEN = EParser.WS;
			else if(this is MParser)
				WS_TOKEN = MParser.WS;
			else if(this is OParser)
				WS_TOKEN = OParser.WS;
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

		public bool wasNotWhiteSpace()
		{
			return lastHiddenTokenType() != WS_TOKEN;
		}


        public bool willBe(int type)
        {
            return this.InputStream.LA(1) == type;
        }

        public bool willBeIn(params int[] types)
        {
            var next = this.InputStream.LA(1);
            var iter = types.GetEnumerator();
            while(iter.MoveNext())
            {
                if (next == (int)iter.Current)
                    return true;
            }
            return false;
        }

        public bool willNotBe(int type)
        {
            return this.InputStream.LA(1) != type;
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
			return text==this.TokenStream.LT(1).Text;
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

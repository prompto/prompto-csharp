using Antlr4.Runtime;

namespace prompto.parser
{

    public class Location : ILocation
    {
        int index;
        int line;
        int column;

        public Location(IToken token)
            : this(token, false)
        {
        }

        public Location(IToken token, bool isEnd)
        {
            this.index = token.StartIndex;
            this.line = token.Line;
            this.column = token.Column;
            if (isEnd && token.Text != null)
            {
                this.index += token.Text.Length;
                this.column += token.Text.Length;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public int Line
        {
            get
            {
                return line;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
        }
    }
}

using System.Collections.Generic;
using System;
using Antlr4.Runtime;

namespace prompto.parser
{

    public class SIndentingLexer : SLexer, ILexer
    {
        List<IToken> tokens = new List<IToken>();
        Stack<int> indents = new Stack<int>();
        bool wasLF = false;

        public SIndentingLexer(ICharStream input)
            : base(input)
        {
            indents.Push(0);
            AddLF = true;
        }

        public bool AddLF { get; set; }

        public Dialect Dialect { get { return Dialect.S; } }
    
        public override IToken NextToken()
        {
            IToken t = getNextToken();
            wasLF = t.Type == LF;
            return t;
        }

        private IToken getNextToken()
        {
            if (tokens.Count > 0)
            {
                IToken result = tokens[0];
                tokens.RemoveAt(0);
                return result;
            }
            interpret(base.NextToken());
            return NextToken();
        }

        void interpret(IToken token)
        {
            switch (token.Type)
            {
                case Eof:
                    InterpretEOF(token);
                    break;
                case LF_TAB:
                    InterpretLFTAB(token);
                    break;
                default:
                    InterpretAnyToken(token);
                    break;
            }
        }

        void InterpretEOF(IToken eof)
        {
            // gracefully handle missing dedents
            while (indents.Count > 1)
            {
                tokens.Add(DeriveToken(eof, DEDENT));
                tokens.Add(DeriveToken(eof, LF));
                wasLF = true;
                indents.Pop();
            }
            // gracefully handle missing lf
            if (!wasLF && AddLF)
                tokens.Add(DeriveToken(eof, LF));
            tokens.Add(eof);

        }

        void InterpretLFTAB(IToken lftab)
        {
            // count TABs following LF
            int indentCount = CountIndents(lftab.Text);
            IToken next = base.NextToken();
            // if this was an empty line, simply skip it
            if (next.Type == Eof || next.Type == LF_TAB)
            {
                tokens.Add(DeriveToken(lftab, LF));
                interpret(next);
            }
            else if (indentCount == indents.Peek())
            {
                tokens.Add(DeriveToken(lftab, LF));
                interpret(next);
            }
            else if (indentCount > indents.Peek())
            {
                tokens.Add(DeriveToken(lftab, LF));
                tokens.Add(DeriveToken(lftab, INDENT));
                indents.Push(indentCount);
                interpret(next);
            }
            else
            {
                while (indents.Count > 1 && indentCount < indents.Peek())
                {
                    tokens.Add(DeriveToken(lftab, DEDENT));
                    tokens.Add(DeriveToken(lftab, LF));
                    indents.Pop();
                }
                // TODO, fire an error through token
                // if (indentCount > indents.Peek())
                //    ; 
                interpret(next);
            }
        }

        private int CountIndents(String text)
        {
            int count = 0;
            foreach (char c in text.ToCharArray()) switch (c)
                {
                    case ' ':
                        count += 1;
                        break;
                    case '\t':
                        count += 4;
                        break;
                }
            return count/4;
        }

         void InterpretAnyToken(IToken token)
        {
            tokens.Add(token);
        }

         private CommonToken DeriveToken(IToken token, int type)
         {
             CommonToken res = new CommonToken(token);
             res.Type = type;
             return res;
         }


    }

}

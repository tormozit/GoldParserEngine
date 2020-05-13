using System.Runtime.InteropServices;

namespace GoldParserEngine
{
    /// <summary>
    ///     While the Symbol represents a class of terminals and nonterminals, the
    ///     Token represents an individual piece of information.
    ///     Ideally, the token would inherit directly from the Symbol Class, but do to
    ///     the fact that Visual Basic 5/6 does not support this aspect of Object Oriented
    ///     Programming, a Symbol is created as a member and its methods are mimicked.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Token
    {
        public Token BeginNoise;
        public Token EndNoise;
        private readonly Position _position = new Position();
        private Symbol _parent;

        internal Token()
        {
            _parent = null;
            Data = null;
            State = 0;
        }

        public Token(Symbol parent, object data)
        {
            _parent = parent;
            Data = data;
            State = 0;
        }

        // BorderIndex = 0 - begin border
        // BorderIndex = 1 - end border
        public Token GetBorderToken(int BorderIndex = 0, bool IncludeNoise = false)
        {
            Reduction DataReduction = (Reduction)Data;
            int TokenCount = DataReduction.TokenCount;
            Token TokenCandidate;
            for (int Counter = 1; Counter <= TokenCount; Counter++)
            {
                if (BorderIndex == 1)
                {
                    TokenCandidate = (Token)DataReduction.Tokens(TokenCount - Counter);
                }
                else
                {
                    TokenCandidate = (Token)DataReduction.Tokens(Counter - 1);
                }
                if (TokenCandidate.Type == SymbolType.Terminal)
                {
                    if (IncludeNoise && BorderIndex == 1 && TokenCandidate.EndNoise != null)
                    {
                        return TokenCandidate.EndNoise;
                    }
                    else if (IncludeNoise && BorderIndex == 0 && TokenCandidate.BeginNoise != null)
                    {
                        return TokenCandidate.BeginNoise;
                    }
                    else
                        return TokenCandidate;
                }
                else //if (TokenCandidate.Type == SymbolType.Nonterminal)
                {
                    if (((Reduction)TokenCandidate.Data).TokenCount > 0)
                    {
                        Token TokenFromBottom = TokenCandidate.GetBorderToken(BorderIndex, IncludeNoise);
                        if (TokenFromBottom != null)
                        {
                            return TokenFromBottom;
                        }
                    }

                }
            }
	        return null;
        }

        public int ColumnNumber 
        {
            get { return _position.Column + 1; }
        }
        public int LineNumber
        {
            get { return _position.Line + 1; }
        }
        /// <summary>
        ///     Returns the line/column position where the token was read.
        /// </summary>
        /// <returns></returns>
        public Position Position
        {
            get { return _position; }
        }
        public string Name
        {
            get { return Parent.Name; }
        }

        public string Text
        {
            get { return "<" + Parent.Name + ">"; }
        }
        
        /// <summary>
        ///     Returns/sets the object associated with the token.
        /// </summary>
        /// <returns></returns>
        public object Data { get; set; }

        internal short State { get; set; }

        /// <summary>
        ///     Returns the parent symbol of the token.
        /// </summary>
        /// <returns></returns>
        public Symbol Parent
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        /// <summary>
        ///     "Returns the symbol type associated with this token."
        /// </summary>
        /// <returns></returns>
        public SymbolType Type
        {
            get { return _parent.Type; }
        }

        public SymbolType Kind
        {
            get { return _parent.Type; }
        }

        internal Group Group
        {
            get { return _parent.Group; }
        }
    }
}
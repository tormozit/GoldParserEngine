using System.Runtime.InteropServices;

namespace GoldParserEngine
{
    /// <summary>
    ///     This class is used by the engine to hold a reduced rule.
    ///     Rather the contain a list of Symbols, a reduction contains a list of Tokens corresponding to the the rule it
    ///     represents.
    ///     This class is important since it is used to store the actual source program parsed by the Engine.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Reduction : TokenList
    {
        internal Reduction(int size)
        {
            for (int n = 0; n < size; n++)
            {
                base.Add(null);
            }
        }

        /// <summary>
        ///     Returns the parent production.
        /// </summary>
        /// <returns></returns>
        public Production ParentRule { get; internal set; }

        /// <summary>
        ///     Returns/sets any additional user-defined data to this object.
        /// </summary>
        /// <returns></returns>
        public object Tag { get; set; }

        public object Tokens(int index)
        {
            if (Count > index)
                return this[index];
            else
                return null;
        }

        public string RuleText()
        {
            return ParentRule.RuleNonterminal.Text;
        }

        public int TokenCount
        {
            get { return this.Count; }
        }

        public void SetData(int index, object value)
        {
            this[index].Data = value;
        }
    }
}
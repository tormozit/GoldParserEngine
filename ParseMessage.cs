using System.Runtime.InteropServices;

namespace GoldParserEngine
{
    [ComVisible(true)]
    public enum ParseMessage
    {
        /// <summary>
        /// A new token is read
        /// </summary>
        TokenRead = 1,
        /// <summary>
        /// A production is reduced
        /// </summary>
        Reduction = 2,
        /// <summary>
        /// Grammar complete
        /// </summary>
        Accept = 3,
        /// <summary>
        /// The tables are not loaded
        /// </summary>
        NotLoadedError = 4,
        /// <summary>
        /// Token not recognized
        /// </summary>
        LexicalError = 5,
        /// <summary>
        /// Token is not expected
        /// </summary>
        SyntaxError = 6,
        /// <summary>
        /// Reached the end of the file inside a block
        /// </summary>
        GroupError = 7,
        /// <summary>
        /// Something is wrong, very wrong
        /// </summary>
        InternalError = 8
    }
}
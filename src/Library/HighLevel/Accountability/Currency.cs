namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a currency with which transactions can be made.
    /// </summary>
    public sealed class Currency
    {
        /// <summary>
        /// The currency's name.
        /// Because of the OCP principle we use tha attributes are
        /// readonly, that way this class is open to extension
        /// but closed to modifications.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The currency's symbol.
        /// </summary>
        public readonly string Symbol;

        private Currency(string name, string symbol)
        {
            this.Name = name;
            this.Symbol = symbol;
        }

        /// <summary>
        /// The uruguayan peso.
        /// </summary>
        public static Currency Peso = new Currency("uruguayan peso(s)", "U$");

        /// <summary>
        /// The american dollar.
        /// </summary>
        public static Currency Dollar = new Currency("american dollar(s)", "U$S");
    }
}

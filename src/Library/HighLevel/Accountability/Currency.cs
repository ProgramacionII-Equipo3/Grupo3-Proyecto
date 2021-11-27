using System.Linq;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a currency with which transactions can be made.
    /// Because of the OCP principle, the attributes are
    /// readonly, that way this class is open to extension
    /// but closed to modifications.
    /// </summary>
    public sealed class Currency
    {
        /// <summary>
        /// The uruguayan peso.
        /// </summary>
        public static Currency Peso = new Currency("peso", "U$");

        /// <summary>
        /// The american dollar.
        /// </summary>
        public static Currency Dollar = new Currency("dollar", "US$");

        /// <summary>
        /// Gets the list of available currencies.
        /// </summary>
        public static Currency[] Currencies = new Currency[]
        {
            Currency.Peso,
            Currency.Dollar
        };

        /// <summary>
        /// The currency's name.
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

        /// <inheritdoc />
        public override string ToString() => this.Symbol;

        /// <summary>
        /// Gets the currency given its name.
        /// </summary>
        /// <param name="name">The currency's name.</param>
        /// <returns>The currency, if there is.</returns>
        public static Currency? GetFromName(string name) =>
            Currencies.Where(c => c.Name.ToLowerInvariant() == name.Trim().ToLowerInvariant()).FirstOrDefault();

        /// <summary>
        /// Gets the currency given its symbol.
        /// </summary>
        /// <param name="symbol">The currency's symbol.</param>
        /// <returns>The currency, if there is.</returns>
        public static Currency? GetFromSymbol(string symbol) =>
            Currencies.Where(c => c.Symbol.ToLowerInvariant() == symbol.Trim().ToLowerInvariant()).FirstOrDefault();

    }
}

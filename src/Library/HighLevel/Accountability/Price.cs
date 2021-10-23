namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct represents a price.
    /// </summary>
    public struct Price
    {
        /// <summary>
        /// The numeric value.
        /// </summary>
        public float Quantity { get; private set; }

        /// <summary>
        /// The currency with which the price is determined.
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// The unit of material measurement with which the price is determined.
        /// </summary>
        /// <value></value>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="Price" />.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="currency">The currency with which the price is determined.</param>
        /// <param name="unit">The unit of material measurement with which the price is determined.</param>
        /// <example>
        ///     This example creates an instance of <see cref="Price" /> which represents U$S 30/kg;
        ///     <code>
        ///         new Price(30, Currency.Dollar, Unit.GetByAbbr("kg"));
        ///     </code>
        /// </example>
        public Price(float quantity, Currency currency, Unit unit)
        {
            this.Quantity = quantity;
            this.Currency = currency;
            this.Unit = unit;
        }
    }
}

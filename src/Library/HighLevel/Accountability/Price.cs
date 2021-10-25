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
        /// <param name="quantity"></param>
        /// <param name="currency"></param>
        /// <param name="unit"></param>
        public Price(float quantity, Currency currency, Unit unit)
        {
            this.Quantity = quantity;
            this.Currency = currency;
            this.Unit = unit;
        }
    }
}

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct represents a price.
    /// Created because of SRP.
    /// </summary>
    public struct Price
    {
        /// <summary>
        /// Gets the numeric value.
        /// </summary>
        public float Quantity { get; private set; }

        /// <summary>
        /// Gets the currency with which the price is determined.
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Gets the unit of material measurement with which the price is determined.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> struct.
        /// </summary>
        /// <param name="quantity">Material´s Quantity.</param>
        /// <param name="currency">Material´s publication currency.</param>
        /// <param name="unit">Material´s unit.</param>
        public Price(float quantity, Currency currency, Unit unit)
        {
            this.Quantity = quantity;
            this.Currency = currency;
            this.Unit = unit;
        }
    }
}

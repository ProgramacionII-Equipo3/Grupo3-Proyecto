namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents an amount of money.
    /// </summary>
    public struct MoneyQuantity
    {
        /// <summary>
        /// The numeric value.
        /// </summary>
        public readonly float Quantity;

        /// <summary>
        /// The currency.
        /// </summary>
        public readonly Currency Currency;

        /// <summary>
        /// Creates an instance of <see cref="MoneyQuantity" />.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="currency">The currency.</param>
        public MoneyQuantity(float quantity, Currency currency)
        {
            this.Quantity = quantity;
            this.Currency = currency;
        }
    }
}

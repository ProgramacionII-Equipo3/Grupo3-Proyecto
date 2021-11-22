namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents an amount of money.
    /// Created because of SRP and Low Coupling and High Cohesion, in that way
    /// we have this class that has the responsibility of assign an amount of 
    /// money with his respective quantity and currency (particular of a money quantity)
    /// with the currency that is controlled by another class currency (following the
    /// pattern and principle).
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
        /// Initializes a new instance of the <see cref="MoneyQuantity"/> struct.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="currency">The currency.</param>
        public MoneyQuantity(float quantity, Currency currency)
        {
            this.Quantity = quantity;
            this.Currency = currency;
        }

        public override string ToString() =>
            $"{this.Currency} {this.Quantity}";
    }
}

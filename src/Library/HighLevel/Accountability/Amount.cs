namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct represents an amount of material.
    /// </summary>
    public struct Amount
    {
        /// <summary>
        /// The numeric value in the amount.
        /// </summary>
        public readonly float Quantity;

        /// <summary>
        /// The unit used in the amount.
        /// </summary>
        /// <value></value>
        public readonly Unit Unit;

        /// <summary>
        /// Creates an instance of <see cref="Amount" />.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="unit">The unit.</param>
        public Amount(float quantity, Unit unit)
        {
            this.Quantity = quantity;
            this.Unit = unit;
        }
    }
}
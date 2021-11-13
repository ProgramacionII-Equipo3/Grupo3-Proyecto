namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct represents an amount of material.
    /// We used the OCP principle to create this class, we used "readonly"
    /// to prevent modifications, but it's still open to extension.
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
        public readonly Unit Unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Amount"/> struct.
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

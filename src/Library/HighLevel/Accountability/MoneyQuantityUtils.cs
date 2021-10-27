namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class offers functions associated with the <see cref="MoneyQuantity" /> struct.
    /// </summary>
    public static class MoneyQuantityUtils
    {
        /// <summary>
        /// Calculates the amount of money made from selling a certain amount of material at a certain price.
        /// </summary>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <returns>The resulting <see cref="MoneyQuantity" />, or null if the amount and price are invalid with each other.</returns>
        public static MoneyQuantity? Calculate(Amount amount, Price price) =>
            Unit.GetConversionFactor(amount.Unit, price.Unit) is double unitConversionFactor
                ? new MoneyQuantity(
                    (float) (amount.Quantity * price.Quantity * unitConversionFactor),
                    price.Currency
                ) : null;
    }
}

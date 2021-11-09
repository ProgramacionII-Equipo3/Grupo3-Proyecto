namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class offers functions associated with the <see cref="MoneyQuantity" /> struct.
    /// We created this class because of the Polymorphism pattern.
    /// </summary>
    public static class MoneyQuantityUtils
    {
        /// <summary>
        /// Calculates the amount of money made from selling a certain amount of material at a certain price.
        /// </summary>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <returns>The resulting <see cref="MoneyQuantity" />, or null if the amount and price are invalid with each other.</returns>
        public static Option<MoneyQuantity> Calculate(Amount amount, Price price) =>
            Unit.GetConversionFactor(amount.Unit, price.Unit).MapValue(
                unitConversionFactor => new MoneyQuantity(
                    (float) (amount.Quantity * price.Quantity * unitConversionFactor),
                    price.Currency
                )
            );
    }
}

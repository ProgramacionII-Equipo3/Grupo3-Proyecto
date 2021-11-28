using Library.HighLevel.Accountability;

namespace Library.Utils
{
    /// <summary>
    /// This class offers functions associated with the <see cref="MoneyQuantity" /> struct.
    /// We created this class because of the Polymorphism pattern, while money quantity is for
    /// an amount of money, this class is created for a particular sale, so we separated it by
    /// destination.
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
                (amount.Quantity * price.Quantity * unitConversionFactor),
                price.Currency)
            : null;
    }
}

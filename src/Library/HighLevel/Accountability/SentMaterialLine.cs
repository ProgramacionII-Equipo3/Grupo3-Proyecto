using System;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a concrete material sale.
    /// Created because of SRP and LSP, so we can use a specific line
    /// without modifying the report.
    /// </summary>
    public class MaterialSalesLine
    {
        /// <summary>
        /// The sold material.
        /// </summary>
        public readonly Material Material;

        /// <summary>
        /// The amount of sold material.
        /// </summary>
        public readonly Amount Amount;

        /// <summary>
        /// The price of the sold material.
        /// </summary>
        public readonly Price Price;

        /// <summary>
        /// The moment the sale happened.
        /// </summary>
        public readonly DateTime DateTime;

        /// <summary>
        /// The amount of money made from this sale.
        /// </summary>
        public MoneyQuantity Income => MoneyQuantityUtils.Calculate(this.Amount, this.Price).Unwrap();

        /// <summary>
        /// Creates an instance of <see cref="MaterialSalesLine" />.
        /// </summary>
        /// <param name="material">The sold material.</param>
        /// <param name="amount">The amount of sold material.</param>
        /// <param name="price">The price of the sold material.</param>
        /// <param name="dateTime">The moment the sale happened.</param>
        public MaterialSalesLine(Material material, Amount amount, Price price, DateTime dateTime)
        {
            this.Material = material;
            this.Amount = amount;
            this.Price = price;
            this.DateTime = dateTime;
        }
    }
}

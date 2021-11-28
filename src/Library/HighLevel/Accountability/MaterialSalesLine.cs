using System;
using System.Globalization;
using Library.HighLevel.Materials;
using Library.Utils;

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
        /// The name of the entrepreneur who made the purchase.
        /// </summary>
        public readonly string Buyer;

        /// <summary>
        /// Gets the ID for the sale.
        /// </summary>
        public static int StaticID = 0;

        /// <summary>
        /// The sale´s ID.
        /// </summary>
        public readonly int SaleID;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialSalesLine"/> class.
        /// </summary>
        /// <param name="material">The sold material.</param>
        /// <param name="amount">The amount of sold material.</param>
        /// <param name="price">The price of the sold material.</param>
        /// <param name="dateTime">The moment the sale happened.</param>
        /// <param name="buyer">The name of the entrepreneur who made the purchase.</param>
        public MaterialSalesLine(Material material, Amount amount, Price price, DateTime dateTime, string buyer)
        {
            this.Material = material;
            this.Amount = amount;
            this.Price = price;
            this.DateTime = dateTime;
            this.Buyer = buyer;
            this.SaleID = MaterialSalesLine.StaticID;

            MaterialSalesLine.StaticID += 1;
        }

        /// <summary>
        /// Gets the amount of money made from this sale.
        /// </summary>
        public MoneyQuantity Income => MoneyQuantityUtils.Calculate(this.Amount, this.Price).Unwrap();

        /// <inheritdoc />
        public override string? ToString() =>
            $"{this.Amount} de {this.Material.Name} vendido(s) al emprendedor {this.Buyer} a precio de {this.Price} el día {this.DateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
    }
}

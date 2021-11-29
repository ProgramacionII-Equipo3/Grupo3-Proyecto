using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This class represents a material bought by the entrepreneur
    /// We used the SRP principle, that way material, datetime,
    /// price, amount are all objects created by his respective class.
    /// That way we have classes with Low Coupling.
    /// </summary>
    public class BoughtMaterialLine
    {
        /// <summary>
        /// The company who owns the material.
        /// </summary>
        public string CompanyName { get; }

        /// <summary>
        /// The purchased material.
        /// </summary>
        public Material Material { get; }

        /// <summary>
        /// The date of when the purchase was made.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// The cost of the material.
        /// </summary>
        public Price Price { get; }

        /// <summary>
        /// The amount of the purchased material.
        /// </summary>
        public Amount Amount { get; }

        /// <summary>
        /// The purchase's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoughtMaterialLine"/> class.
        /// </summary>
        /// <param name="companyName">The company who owned the material.</param>
        /// <param name="material">The purchased material.</param>
        /// <param name="dateTime">The date of when the purchase was made.</param>
        /// <param name="price">The cost of the material.</param>
        /// <param name="amount">The amount of the purchased material.</param>
        /// <param name="id">The purchase's id.</param>
        [JsonConstructor]
        public BoughtMaterialLine(string companyName, Material material, DateTime dateTime, Price price, Amount amount, int id)
        {
            this.CompanyName = companyName;
            this.Material = material;
            this.DateTime = dateTime;
            this.Price = price;
            this.Amount = amount;
            this.Id = id;
        }

        /// <summary>
        /// Gets the amount of money spent.
        /// </summary>
        public MoneyQuantity Spent => MoneyQuantityUtils.Calculate(this.Amount, this.Price).Unwrap();

        /// <inheritdoc />
        public override string? ToString() =>
            $"{this.Amount} de {this.Material.Name} el día {this.DateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} a precio de {this.Price} ({this.Spent})";
    }
}

using System;
using System.Linq;
using Library.HighLevel.Companies;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct holds the JSON information of a <see cref="BoughtMaterialLine" />.
    /// </summary>
    public struct JsonBoughtMaterialLine : IJsonHolder<BoughtMaterialLine>
    {
        /// <summary>
        /// The name of the company from whom the material was bought.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The material.
        /// </summary>
        public JsonMaterial Material { get; set; }

        /// <summary>
        /// The moment of the purchase.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The price of the material.
        /// </summary>
        public JsonPrice Price { get; set; }

        /// <summary>
        /// The amount of material.
        /// </summary>
        public JsonAmount Amount { get; set; }

        /// <inheritdoc />
        public void FromValue(BoughtMaterialLine value)
        {
            this.CompanyName = value.CompanyName;
            this.Material = new JsonMaterial();
            this.Material.FromValue(value.Material);
            this.DateTime = value.DateTime;
            this.Price = new JsonPrice();
            this.Price.FromValue(value.Price);
            this.Amount = new JsonAmount();
            this.Amount.FromValue(value.Amount);
        }

        /// <inheritdoc />
        public BoughtMaterialLine ToValue()
        {
            var material = this.Material;
            return new BoughtMaterialLine(
                this.CompanyName,
                Singleton<CompanyManager>.Instance
                    .GetByName(this.CompanyName)
                    .Unwrap()
                    .AssignedPublications
                    .Select(p => p.Publication.Material)
                    .Where(m => m.Name == material.Name)
                    .FirstOrDefault()
                    .Unwrap(),
                this.DateTime,
                this.Price.ToValue(),
                this.Amount.ToValue());
        }
    }
}
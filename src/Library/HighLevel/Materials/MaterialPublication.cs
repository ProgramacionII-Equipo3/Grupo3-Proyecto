using System.Collections.Generic;
using System;
using Library.HighLevel.Accountability;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a publication of a material from a company.
    /// We used the principle Expert to create this class.
    /// </summary>
    public class MaterialPublication
    {
        /// <summary>
        /// The publication's material.
        /// </summary>
        public Material Material { get; private set; }

        /// <summary>
        /// The publication's amount of material.
        /// </summary>
        public Amount Amount { get; private set; }

        /// <summary>
        /// The publication's price of the material.
        /// </summary>
        public Price Price { get; private set; }

        /// <summary>
        /// The publication's pick-up location of material.
        /// </summary>
        public Location PickupLocation { get; private set; }
        
        /// <summary>
        /// List to save all the publication's.
        /// </summary>
        public static List<MaterialPublication> publications = new List<MaterialPublication>();

        /// <summary>
        /// List of the keywords of the publication material.
        /// </summary>
        public List<string> Keywords = new List<string>();
        
        private MaterialPublication(Material material, Amount amount, Price price, Location pickupLocation, List<string> keywords)
        {
            this.Material = material;
            this.Amount = amount;
            this.Price = price;
            this.PickupLocation = pickupLocation;
            this.Keywords = keywords;
        }

        /// <summary>
        /// Checks whether the given fields for building a <see cref="MaterialPublication" /> are valid with each other.
        /// That is, whether the material, amount and price are described under the same measure.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <returns>Whether the data is valid with itself.</returns>
        private static bool CheckMaterialFields(Material material, Amount amount, Price price) =>
            material.Measure == amount.Unit.Measure && material.Measure == price.Unit.Measure;

        /// <summary>
        /// Creates an instance of <see cref="MaterialPublication" />, validating the data beforehand.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <param name="pickupLocation">The pick-up location of the material.</param>
        /// <param name ="keywords">The keywords of the material.</param>
        /// <returns>A <see cref="MaterialPublication" />, or null if the data is invalid.</returns>
        public static MaterialPublication CreateInstance(Material material, Amount amount, Price price, Location pickupLocation, List<string> keywords) =>
            CheckMaterialFields(material, amount, price)
                ? new MaterialPublication(material, amount, price, pickupLocation, keywords)
                : null;

        /// <summary>
        /// This method add's all created publication's to the previously created publication's list
        /// </summary>
        public static void AddPublication(MaterialPublication publication)
        {
            publications.Add(publication);
        }

    }
}
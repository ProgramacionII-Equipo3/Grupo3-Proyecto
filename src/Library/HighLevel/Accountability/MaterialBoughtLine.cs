using System;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// this class represents a material bought by the entrepreneur
    /// We used the SRP principle, that way material, datetime, 
    /// price, amount are all objects created by his respective class.
    /// </summary>
    public class MaterialBoughtLine
    {
        /// <summary>
        /// The purchased material.
        /// </summary>
        public readonly Material Material;
        
        /// <summary>
        /// The date of when the purchase was made.
        /// </summary>
        public readonly DateTime DateTime;
        
        /// <summary>
        /// The cost of the material.
        /// </summary>
        public readonly Price Price;

        /// <summary>
        /// The amount of the purchased material.
        /// </summary>
        public readonly Amount Amount;
        /// <summary>
        /// The amount of money spent.
        /// </summary>
        /// <returns></returns>
        public MoneyQuantity Spent => MoneyQuantityUtils.Calculate(this.Amount, this.Price).Unwrap();
        
        /// <summary>
        /// Creates a MaterialBoughtLine. 
        /// </summary>
        /// <param name="material">It is the purchased material</param>
        /// <param name="dateTime">It is the date of when the purchase was made</param>
        /// <param name="price">It is the cost of the material</param>
        /// <param name="amount">It is the amount of the pruchased material</param>
        public MaterialBoughtLine (Material material, DateTime dateTime, Price price, Amount amount)
        {
            this.Material = material;
            this.DateTime = dateTime;
            this.Price = price;
            this.Amount = amount;
        }
    }
}
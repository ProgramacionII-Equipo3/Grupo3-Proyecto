using System;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// this class represents a material bought by the entrepreneur
    /// </summary>
    public class MaterialBoughtLine
    {
        /// <summary>
        /// Is the purchased material
        /// </summary>
        public readonly Material Material;
        
        /// <summary>
        /// It is the date of when the purchase was made
        /// </summary>
        public readonly DateTime DateTime;
        
        /// <summary>
        /// It is the cost of the material
        /// </summary>
        public readonly Price Price;

        /// <summary>
        /// It is the amount of the purchased material
        /// </summary>
        public readonly Amount Amount;
        /// <summary>
        /// Is the amount of money spent
        /// </summary>
        /// <returns></returns>
        public MoneyQuantity Spent => MoneyQuantityUtils.Calculate(this.Amount, this.Price).Unwrap();
        
        /// <summary>
        /// It is the constructor of MaterialBoughtLine 
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
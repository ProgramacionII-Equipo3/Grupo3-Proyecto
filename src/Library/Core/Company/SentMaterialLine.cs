namespace Library.Core.Company
{
    /// <summary>
    /// This class represents sent material lines, 
    /// it stores the information
    /// </summary>
    public class SentMaterialLine
    {
        /// <summary>
        /// The material of the material line
        /// </summary>
        public Material Material;
        /// <summary>
        /// The stock of the material line
        /// </summary>
        public Amount Stock;
        /// <summary>
        /// The price of the material line
        /// </summary>
        public Price Price;
        /// <summary>
        /// The time stamp of the material line
        /// </summary>
        public DateTime Timestamp;
        /// <summary>
        /// The income of the material line
        /// </summary>
        public MoneyQuantity Income;

        /// <summary>
        /// The SentMaterialLine's Constructor
        /// </summary>
        /// <param name="material">SentMaterialLine's material</param>
        /// <param name="stock">SentMaterialLine's stock</param>
        /// <param name="price">SentMaterialLine's price</param>
        /// <param name="timestamp">SentMaterialLine's time stamp</param>
        /// <param name="income">SentMaterialLine's income</param>
        public SentMaterialLine (Material material, Amount stock, Price price, DateTime timestamp, MoneyQuantity income)
        {
            this.Material = material;
            this.Stock = stock;
            this.Price = price;
            this.Timestamp = timestamp;
            this.Income = income;
        }
    }
}
namespace Library.Core.Company
{
    /// <summary>
    /// This class represents the money quantity with his 
    /// specific currency from an income
    /// </summary>
    public class MoneyQuantity 
    {
        /// <summary>
        /// The quantity of money
        /// </summary>
        public float Quantity;
        /// <summary>
        /// The currency of the money
        /// </summary>
        public Currency Currency;

        /// <summary>
        /// The MoneyQuantity Constructor
        /// </summary>
        /// <param name="quantity">MoneyQuantity's quantity</param>
        /// <param name="currency">MoneyQuantity's currency</param>
        public MoneyQuantity (float quantity, Currency currency)
        {
            this.Quantity = quantity;
            this.Currency = currency;
        }
    }
}
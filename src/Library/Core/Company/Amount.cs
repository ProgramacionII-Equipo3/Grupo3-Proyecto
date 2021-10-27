namespace Library.Core.Company
{
    /// <summary>
    /// This class represents the amount of material in stock
    /// </summary>
    public class Amount 
    {
        /// <summary>
        /// The quantity of the amount
        /// </summary>
        public float Quantity;
        /// <summary>
        /// The unit of the amount
        /// </summary>
        public Unit Unit;

        /// <summary>
        /// The Amount Constructor
        /// </summary>
        /// <param name="quantity">Amount's quantity</param>
        /// <param name="unit">Amount's unit</param>
        public Amount (float quantity, Unit unit)
        {
            this.Quantity = quantity;
            this.Unit = unit;
        }
    }
}
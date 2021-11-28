using System.Text.Json.Serialization;
using Library.Utils;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct represents an amount of material.
    /// We used the OCP principle to create this class, we used "readonly"
    /// to prevent modifications, but it's still open to extension.
    /// </summary>
    public struct Amount
    {
        /// <summary>
        /// The numeric value in the amount.
        /// </summary>
        public double Quantity { get; private set; }

        /// <summary>
        /// The unit used in the amount.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Amount"/> struct.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="unit">The unit.</param>
        public Amount(double quantity, Unit unit)
        {
            this.Quantity = quantity;
            this.Unit = unit;
        }

        /// <inheritdoc />
        public override string? ToString() =>
            $"{this.Quantity.ToString("0.00")} {this.Unit}";

        /// <summary>
        /// Substracts two amounts, storing the result in the first one.
        /// </summary>
        /// <param name="other">The amount to substract.</param>
        /// <returns>
        /// 0 if the two amounts can be substracted, <br />
        /// 1 if the two amounts' units are incompatible with each other, <br />
        /// 2 if the second amount is bigger than the first one.
        /// </returns>
        public byte Substract(Amount other)
        {
            if(Unit.GetConversionFactor(this.Unit, other.Unit) is double factor)
            {
                double newQuantity = this.Quantity - other.Quantity / factor;
                if(newQuantity < 0) return 2;
                this.Quantity = newQuantity;
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// Sets itself to zero.
        /// </summary>
        public void SetToZero()
        {
            this.Quantity = 0;
        }
    }
}

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
        public float Quantity { get; private set; }

        /// <summary>
        /// The unit used in the amount.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Amount"/> struct.
        /// </summary>
        /// <param name="quantity">The numeric value.</param>
        /// <param name="unit">The unit.</param>
        public Amount(float quantity, Unit unit)
        {
            this.Quantity = quantity;
            this.Unit = unit;
        }

        /// <inheritdoc />
        public override string? ToString() =>
            $"{this.Quantity} {this.Unit}";
    }
}

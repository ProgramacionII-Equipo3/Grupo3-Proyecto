using Library.HighLevel;
using Library.Utils;

namespace Library.HighLevel.Accountability
{
    /// <summary>
    /// This struct holds the JSON information of a <see cref="Price" />.
    /// </summary>
    public class JsonPrice : IJsonHolder<Price>
    {
        /// <summary>
        /// The price's quantity.
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// The price's currency.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// The price's unit.
        /// </summary>
        public string? Unit { get; set; }

        /// <inheritdoc />
        public Price ToValue() =>
            new Price(
                this.Quantity,
                Accountability.Currency.GetFromName(this.Currency.Unwrap()).Unwrap(),
                Accountability.Unit.GetByAbbr(this.Unit.Unwrap()).Unwrap());

        /// <inheritdoc />
        public void FromValue(Price value)
        {
            this.Quantity = value.Quantity;
            this.Currency = value.Currency.Name;
            this.Unit = value.Unit.Abbreviation;
        }
    }
}

using Library.HighLevel;
using Library.Utils;

namespace Library.HighLevel.Accountability.Json
{
    ///
    public struct JsonAmount
    {
        ///
        public float Quantity { get; set; }

        ///
        public string? Unit { get; set; }

        ///
        public Amount ToAmount() =>
            new Amount(this.Quantity, Accountability.Unit.GetByAbbr(this.Unit.Unwrap()).Unwrap());

        ///
        public static JsonAmount FromAmount(Amount amount) =>
            new JsonAmount()
            {
                Quantity = amount.Quantity,
                Unit = amount.Unit.Abbreviation
            };
    }
}

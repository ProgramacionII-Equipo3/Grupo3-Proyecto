using Library.HighLevel;
using Library.Utils;

namespace Library.HighLevel.Accountability.Json
{
    ///
    public class JsonPrice
    {
        ///
        public float Quantity { get; private set; }

        ///
        public string? Currency { get; private set; }

        ///
        public string? Unit { get; private set; }

        ///
        public Price ToAmount() =>
            new Price(
                this.Quantity,
                Accountability.Currency.GetFromName(this.Currency.Unwrap()).Unwrap(),
                Accountability.Unit.GetByAbbr(this.Unit.Unwrap()).Unwrap());

        ///
        public static JsonPrice FromPrice(Price price) =>
            new JsonPrice()
            {
                Quantity = price.Quantity,
                Currency = price.Currency.Name,
                Unit = price.Unit.Abbreviation
            };
    }
}

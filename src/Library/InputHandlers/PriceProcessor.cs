using System;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Accountability;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of return the price of the publication, using the input data of the user.
    /// </summary>
    public class PriceProcessor : FormProcessor<Price>
    {
        private float? quantity;
        private Currency? currency;
        private Unit? unit;

        /// <summary>
        /// Initializes an instance of <see cref="PriceProcessor" />
        /// </summary>
        public PriceProcessor(Func<Measure> measure)
        {
            this.inputHandlers = new InputHandler[]
            {
                ProcessorHandler<string>.CreateInfallibleInstance(
                    q => this.quantity = float.Parse(q),
                    new BasicStringProcessor(() => "Por favor ingresa el precio del material.")
                ),
                ProcessorHandler<Currency>.CreateInfallibleInstance(
                    c => this.currency = c,
                    new CurrencyProcessor(() => "Por favor ingresa la moneda del precio del material:\n        \"pesos\": Pesos Uruguayos\n        \"dollars\": dólares")
                ),
                ProcessorHandler<Unit>.CreateInfallibleInstance(
                    u => this.unit = u,
                    new UnitProcessor(measure, () => "Por favor ingresa la unidad del material relacionada al precio.")
                )
            };
        }

        /// <inheritdoc />
        protected override Result<Price, string> getResult() =>
            Result<Price, string>.Ok(new Price(this.quantity.Unwrap(), this.currency.Unwrap(), this.unit.Unwrap()));
    }
}

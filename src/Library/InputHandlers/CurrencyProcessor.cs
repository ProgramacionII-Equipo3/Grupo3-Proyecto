using System;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of return an currency for the publication.
    /// </summary>
    public class CurrencyProcessor : ProcessorWrapper<Currency>
    {
        /// <summary>
        /// Initializes an instance of <see cref="CurrencyProcessor" />
        /// </summary>
        /// <param name="initialResponseGetter"></param>
        public CurrencyProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<Currency>.CreateInstance<string>(
                c =>
                {
                    switch (c.Trim().ToLowerInvariant())
                    {
                        case "pesos":
                            return Result<Currency, string>.Ok(Currency.Peso);
                        case "dollars":
                            return Result<Currency, string>.Ok(Currency.Dollar);
                        default:
                            return Result<Currency, string>.Err("No reconocí esa moneda.");
                    }
                },
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        {
        }
    }
}

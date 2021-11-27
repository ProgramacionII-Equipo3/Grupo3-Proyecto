using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Accountability;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of return the price of the publication, using the input data of the user.
    /// </summary>
    public class PriceProcessor : ProcessorWrapper<Price>
    {
        private static Regex parsingRegex = new Regex(
            "\\s*(?<quantity>\\d+(?:\\.\\d+)?) *(?<currency>.+?) */ *(?<unit>[A-Za-z]+)\\s*",
            RegexOptions.Compiled);

        /// <summary>
        /// Initializes an instance of <see cref="PriceProcessor" />
        /// </summary>
        public PriceProcessor(Func<string> initialResponseGetter, Func<Measure> measure) : base (
            PipeProcessor<Price>.CreateInstance<string>(
                s =>
                {
                    Match match = parsingRegex.Match(s);
                    if(!match.Success)
                    {
                        return Result<Price, string>.Err("El texto ingresado no sigue el formato requerido.");
                    }

                    float quantity;
                    if(!float.TryParse(match.Groups["quantity"].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out quantity))
                    {
                        return Result<Price, string>.Err("El texto ingresado en la posición de la cantidad no es un número.");
                    }

                    Currency currency;
                    {
                        if(Currency.GetFromSymbol(match.Groups["currency"].Value.ToLowerInvariant()) is Currency c)
                            currency = c;
                        else
                            return Result<Price, string>.Err("La moneda ingresada no es válida (aviso: se debe utilizar la abreviación).");
                    }

                    Unit unit;
                    {
                        if(Unit.GetByAbbr(match.Groups["unit"].Value.ToLowerInvariant()) is Unit u && u.Measure == measure())
                            unit = u;
                        else
                            return Result<Price, string>.Err("La unidad ingresada no es válida (aviso: se debe utilizar la abreviación).");
                    }

                    return Result<Price, string>.Ok(new Price(quantity, currency, unit));
                },
                new BasicStringProcessor(() => $"{initialResponseGetter()}\nUnidades disponibles:{string.Join(null, measure().Units.Select(u => $"\n        {u.Abbreviation} ({u.Name})"))}\nMonedas disponibles:{string.Join(null, Currency.Currencies.Select(c => $"\n        {c.Symbol} ({c.Name})"))}")
            )
        ) {}
    }
}

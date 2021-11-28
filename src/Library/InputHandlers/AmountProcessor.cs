using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;
using Library.Utils;


namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of create an instance of amount for the publication, with the input data of the user.
    /// </summary>
    public class AmountProcessor : ProcessorWrapper<Amount>
    {
        private static Regex parsingRegex = new Regex(
            "\\s*(?<quantity>\\d+(?:\\.\\d+)?) *(?<unit>[A-Za-z]+)\\s*",
            RegexOptions.Compiled);

        /// <summary>
        /// Initializes an instance of <see cref="AmountProcessor" />.
        /// </summary>
        public AmountProcessor(Func<string> initialResponseGetter, Func<Measure> measure) : base(
            new PipeProcessor<string, Amount>(
                q =>
                {
                    Match match = parsingRegex.Match(q);
                    if (!match.Success)
                    {
                        return Result<Amount, string>.Err("El texto ingresado no sigue el formato requerido.");
                    }

                    float quantity;
                    if (!float.TryParse(match.Groups["quantity"].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out quantity))
                    {
                        return Result<Amount, string>.Err("El texto ingresado en la posición de la cantidad no es un número.");
                    }

                    if (Unit.GetByAbbr(match.Groups["unit"].Value.ToLowerInvariant()) is Unit rUnit && rUnit.Measure == measure())
                    {
                        return Result<Amount, string>.Ok(new Amount(quantity, rUnit));
                    }

                    return Result<Amount, string>.Err("La unidad ingresada no es correcta (aviso: se debe utilizar la abreviación).");
                },
                new BasicStringProcessor(() => $"{initialResponseGetter()}\nUnidades disponibles:{string.Join(null, measure().Units.Select(u => $"\n        {u.Abbreviation} ({u.Name})"))}")
            )
        ) {}
    }
}

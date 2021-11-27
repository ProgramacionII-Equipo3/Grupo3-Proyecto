using System;
using System.Linq;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has the responsibility of return a unit for the publication.
    /// </summary>
    public class UnitProcessor : ProcessorWrapper<Unit>
    {
        /// <summary>
        /// Initializes an instance of <see cref="UnitProcessor" />.
        /// </summary>
        /// <param name="measure">The measure from which to look for units.</param>
        /// <param name="initialResponseGetter">The function which determines the processor's default response.</param>
        public UnitProcessor(Func<Measure> measure, Func<string> initialResponseGetter) : base(
            new PipeProcessor<string, Unit>(
                u => Unit.GetByAbbr(u.Trim().ToLowerInvariant()) is Unit unit
                    ? Result<Unit, string>.Ok(unit)
                    : Result<Unit, string>.Err("Esa unidad no existe."),
                new BasicStringProcessor(() => $"{initialResponseGetter()}\nUnidades disponibles:{string.Join(null, measure().Units.Select(u => $"\n        {u.Abbreviation} ({u.Name})"))}")
            )
        )
        {
        }
    }
}
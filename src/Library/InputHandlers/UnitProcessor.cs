using System;
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
        /// Initializes an instance of <see cref="UnitProcessor" />
        /// </summary>
        /// <param name="initialResponseGetter"></param>
        /// <returns></returns>
        public UnitProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<Unit>.CreateInstance<string>(
                u => Unit.GetByAbbr(u.Trim().ToLowerInvariant()) is Unit unit
                    ? Result<Unit, string>.Ok(unit)
                    : Result<Unit, string>.Err("Esa unidad no existe."),
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        {
        }
    }
}
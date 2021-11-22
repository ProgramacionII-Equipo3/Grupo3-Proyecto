using System;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class has te responsibility of return a measure for the publication.
    /// </summary>
    public class MeasureProcessor : ProcessorWrapper<Measure>
    {
        /// <summary>
        /// Initializes an instance of <see cref="MeasureProcessor" />
        /// </summary>
        /// <param name="initialResponseGetter"></param>
        public MeasureProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<Measure>.CreateInstance<string>(
                s =>
                {
                    switch (s.Trim().ToLower())
                    {
                        case "length":
                            return Result<Measure, string>.Ok(Measure.Length);
                        case "weight":
                            return Result<Measure, string>.Ok(Measure.Weight);
                        default:
                            return Result<Measure, string>.Err("No reconocí esa medida.");
                    }
                },
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        {
        }
    }
}

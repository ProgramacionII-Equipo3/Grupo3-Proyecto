using System;
using Library.Core.Processing;
using Library.HighLevel.Accountability;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    public class MeasureProcessor : ProcessorWrapper<Measure>
    {
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

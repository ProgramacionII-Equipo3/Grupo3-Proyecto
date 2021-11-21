using System.Globalization;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using System;

namespace Library.InputHandlers
{
    public class DateProcessor : ProcessorWrapper<DateTime>
    {
        public DateProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<DateTime>.CreateInstance<string>(
                s =>
                {
                    DateTime dateTime;
                    if(DateTime.TryParseExact(s, "dd/MM/yyyy", NumberFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
                    {
                        return Result<DateTime, string>.Ok(dateTime);
                    } else
                    {
                        return Result<DateTime, String>.Err("La fecha es incorrecta.");
                    }
                },
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        {
        }
    }
}
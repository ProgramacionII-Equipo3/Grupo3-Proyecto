using System.Globalization;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using System;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents an <see cref="InputProcessor{T}" /> which generates objects of type <see cref="DateTime" />.
    /// </summary>
    public class DateProcessor : ProcessorWrapper<DateTime>
    {
        /// <inheritdoc />
        public DateProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<DateTime>.CreateInstance<string>(
                s =>
                {
                    DateTime dateTime;
                    if (DateTime.TryParseExact(s, "dd/MM/yyyy", NumberFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
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
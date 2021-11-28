using System;
using Library.InputHandlers.Abstractions;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents an <see cref="InputProcessor{T}" /> which processes the response to a yes/no question.
    /// </summary>
    public class YesNoProcessor : ProcessorWrapper<bool>
    {
        /// <summary>
        /// Initializes an instance of <see cref="YesNoProcessor" />.
        /// </summary>
        /// <param name="initialResponseGetter">The function to get the default response.</param>
        public YesNoProcessor(Func<string> initialResponseGetter) : base(
            new PipeProcessor<string, bool>(
                msg =>
                {
                    msg = msg.Trim().ToLowerInvariant();
                    if (msg == "sí" || msg == "si" || msg == "s" || msg == "yes" || msg == "y")
                        return Result<bool, string>.Ok(true);
                    else if (msg == "no" || msg == "n")
                        return Result<bool, string>.Ok(false);

                    return Result<bool, string>.Err($"Por favor ingresa \"sí\" o \"no\".");
                },
                new BasicStringProcessor(initialResponseGetter)
            )
        )
        { }
    }
}
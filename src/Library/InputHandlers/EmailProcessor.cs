using System;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents a processor which parses an email from input.
    /// </summary>
    public class EmailProcessor : ProcessorWrapper<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="EmailProcessor" />.
        /// </summary>
        /// <param name="initialResponseGetter">The function which determines the default response of the processor.</param>
        public EmailProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<string>.CreateInstance<string> (
                func: s => BasicUtils.IsValidEmail(s)
                    ? Result<string, string>.Ok(s)
                    : Result<string, string>.Err("The given input is not a valid email."),
                processor: new BasicStringProcessor(initialResponseGetter)
            )) {}
    }
}

 
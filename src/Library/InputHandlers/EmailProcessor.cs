using System;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents a processor who parses an email from input.
    /// </summary>
    public class EmailProcessor : ProcessorWrapper<string>
    {
        ///
        public EmailProcessor(Func<string> initialResponseGetter): base(
            PipeProcessor<string>.CreateInstance<string>(
                func: s => Utils.IsValidEmail(s) ? (s, null) : (null, "The given input is not a valid email."),
                processor: new BasicStringProcessor(initialResponseGetter)
            )
        ) {}
    }
}

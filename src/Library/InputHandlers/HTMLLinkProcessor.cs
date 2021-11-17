using System;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents a processor which generates a hypertext link.
    /// </summary>
    public class HTMLLinkProcessor : ProcessorWrapper<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="HTMLLinkProcessor" />.
        /// </summary>
        /// <param name="initialResponseGetter">The function which determines the default response of the processor.</param>
        public HTMLLinkProcessor(Func<string> initialResponseGetter) : base(
            PipeProcessor<string>.CreateInstance<string>(
                s => Utils.IsValidHyperTextLink(s)
                    ? Result<string, string>.Ok(s)
                    : Result<string, string>.Err("The given link is invalid."),
                new BasicStringProcessor(initialResponseGetter))) {}
    }
}
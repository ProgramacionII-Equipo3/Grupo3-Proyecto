using System;
using Library.InputHandlers.Abstractions;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor which processes a phone number from input.
    /// </summary>
    public class PhoneNumberProcessor : ProcessorWrapper<int>
    {
        ///
        public PhoneNumberProcessor(Func<string> initialResponseGetter): base(
            PipeProcessor<int>.CreateInstance<int>(
                func: n => BasicUtils.IsValidPhoneNumber(n)
                    ? Result<int, string>.Ok(n)
                    : Result<int, string>.Err("The given input is not a valid phone number."),
                processor: new UnsignedInt32Processor(initialResponseGetter)
            )
        ) {}
    }
}

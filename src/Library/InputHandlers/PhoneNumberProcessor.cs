using System;
using Library.InputHandlers.Abstractions;
using BoxedInt = Library.RefWrapper<int>;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor with processes a phone number from input.
    /// </summary>
    public class PhoneNumberProcessor : ProcessorWrapper<BoxedInt>
    {
        ///
        public PhoneNumberProcessor(Func<string> initialResponseGetter): base(
            PipeProcessor<BoxedInt>.CreateInstance<BoxedInt>(
                func: n => Utils.IsValidPhoneNumber(n.value) ? (n, null) : (null, "The given input is not a valid phone number."),
                processor: new UnsignedInt32Processor(initialResponseGetter)
            )
        ) {}
    }
}
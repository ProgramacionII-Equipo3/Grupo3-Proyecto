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
            new PipeProcessor<int, int>(
                func: n => BasicUtils.IsValidPhoneNumber(n)
                    ? Result<int, string>.Ok(n)
                    : Result<int, string>.Err("El dato ingresado no es un número de teléfono válido."),
                processor: new UnsignedInt32Processor(initialResponseGetter)
            )
        ) {}
    }
}

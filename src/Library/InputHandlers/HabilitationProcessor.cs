using System;
using Library.InputHandlers.Abstractions;
using Library.HighLevel.Materials;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents a processor which generates habilitations.
    /// </summary>
    public class HabilitationProcessor : ProcessorWrapper<Habilitation>
    {
        ///
        public HabilitationProcessor(Func<string> initialResponseGetter): base(
            PipeProcessor<Habilitation>.CreateInstance<string>(
                link => Utils.IsValidHyperTextLink(link)
                    ? Result<Habilitation, string>.Ok(new Habilitation(link))
                    : Result<Habilitation, string>.Err("The given link is invalid"),
                new BasicStringProcessor(initialResponseGetter)
            )
        ) {}
    }
}

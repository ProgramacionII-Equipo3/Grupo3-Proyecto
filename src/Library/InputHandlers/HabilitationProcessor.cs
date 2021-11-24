using System;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.InputHandlers.Abstractions;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents a processor which generates habilitations.
    /// </summary>
    public class HabilitationProcessor : FormProcessor<Habilitation>
    {
        private string? docLink;
        private string? description;

        /// <summary>
        /// Initializes an instance of <see cref="HabilitationProcessor" />.
        /// </summary>
        public HabilitationProcessor()
        {
            this.inputHandlers = new InputHandler[]
            {
                ProcessorHandler.CreateInfallibleInstance<string>(
                    s => this.docLink = s,
                    new HTMLLinkProcessor(() => "Por favor ingrese el link de la habilitación.")
                ),
                ProcessorHandler.CreateInfallibleInstance<string>(
                    s => this.description = s,
                    new BasicStringProcessor(() => "Por favor ingrese una descripción de la habilitación.")
                )
            };
        }

        /// <inheritdoc />
        protected override Result<Habilitation, string> getResult() =>
            Result<Habilitation, string>.Ok(new Habilitation(docLink.Unwrap(), description.Unwrap()));
    }
}
using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Entrepreneurs;
using Library.Utils;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// 
    /// </summary>
    public class NewEntrepreneurState : InputHandlerState
    {
        ///
        public NewEntrepreneurState(string userId): base(
            ProcessorHandler.CreateInfallibleInstance<Entrepreneur>(
                e => Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(e),
                new NewEntrepreneurForm(userId)
            ),
            () => null,
            () => new EntrepreneurInitialMenuState(userId)
        ) {}

        private class NewEntrepreneurForm : FormProcessor<Entrepreneur>
        {
            private string userId;

            private string? name;
            private int? age;
            private Location? location;
            private string? heading;
            private IList<Habilitation>? habilitations;
            private IList<string>? specializations;

            public NewEntrepreneurForm(string userId)
            {
                this.userId = userId;

                this.inputHandlers = new InputHandler[]
                {
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        name => this.name = name,
                        new BasicStringProcessor(() => "Por favor ingrese su nombre.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<int>(
                        age => this.age = age,
                        new UnsignedInt32Processor(() => "Por favor ingrese su edad.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Location>(
                        location => this.location = location,
                        new LocationProcessor(() => "Por favor ingrese su ubicación (<dirección>, <ciudad>, <departamento>, <país>).")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        heading => this.heading = heading,
                        new BasicStringProcessor(() => "Por favor ingrese su rubro.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Habilitation[]>(
                        habs => this.habilitations = habs.ToList(),
                        new ListProcessor<Habilitation>(
                            () => "Por favor ingrese sus habilitaciones.",
                            new HabilitationProcessor()
                        )
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string[]>(
                        specializations => this.specializations = specializations.ToList(),
                        new ListProcessor<string>(
                            () => "Por favor ingrese sus especializaciones.",
                            new BasicStringProcessor(() => "Por favor ingrese sus especializaciones, escriba /finish para finalizar.")
                        )
                    )
                };
            }

            protected override Result<Entrepreneur, string> getResult() =>
                Result<Entrepreneur, string>.Ok(new Entrepreneur(userId, name.Unwrap(), age.Unwrap(), location.Unwrap(), heading.Unwrap(), habilitations.Unwrap(), specializations.Unwrap()));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Library.Core.Distribution;
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
    /// This class represents the state of an entrepreneur in the middle of being registered.
    /// </summary>
    public class NewEntrepreneurState : InputHandlerState
    {
        /// <summary>
        /// Initializes an instance of <see cref="NewEntrepreneurState" />.
        /// </summary>
        /// <param name="userId">The user id of the entrepreneur.</param>
        public NewEntrepreneurState(string userId): base(
            ProcessorHandler.CreateInstance<Entrepreneur>(
                e => Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(e)
                    ? null
                    : "Ya hay un emprendedor con ese nombre.",
                new NewEntrepreneurForm(userId)
            ),
            () => null,
            () =>
            {
                Singleton<SessionManager>.Instance.GetById(userId).Unwrap().UserData.IsComplete = true;
                return new EntrepreneurInitialMenuState(userId);
            }
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
                    ProcessorHandler.CreateInstance<string>(
                        name =>
                        {
                            if(Singleton<EntrepreneurManager>.Instance.GetByName(name) is Entrepreneur)
                            {
                                return "Ya hay un emprendedor con ese nombre.";
                            }

                            this.name = name;
                            return null;
                        },
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

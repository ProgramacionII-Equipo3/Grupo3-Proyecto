using System.Collections.Generic;
using System.Linq;
using Library.Core.Distribution;
using Library.HighLevel.Entrepreneurs;
using Library.Utils;
using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
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
        public NewEntrepreneurState(string userId) : base(
            ProcessorHandler.CreateInstance<(UserData, Entrepreneur)>(
                e =>
                {
                    if(Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(e.Item2))
                    {
                        UserSession session = Singleton<SessionManager>.Instance.GetById(userId) !;
                        session.UserData = e.Item1;
                        return null;
                    }
                    else
                    {
                        return "Ya hay un emprendedor con ese nombre.";
                    }
                },
                new NewEntrepreneurForm(userId)
            ),
            () => null,
            () =>
            {
                Singleton<SessionManager>.Instance.GetById(userId).Unwrap().UserData.IsComplete = true;
                return new EntrepreneurInitialMenuState(userId);
            }
        ) {}

        private class NewEntrepreneurForm : FormProcessor<(UserData, Entrepreneur)>
        {
            private string userId;

            private UserData? userData;
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
                    ProcessorHandler.CreateInfallibleInstance<UserData>(
                        data => this.userData = data,
                        new UserDataProcessor(true, UserData.Type.ENTREPRENEUR)),
                    ProcessorHandler.CreateInfallibleInstance<int>(
                        age => this.age = age,
                        new UnsignedInt32Processor(() => "Por favor ingrese su edad.")),
                    ProcessorHandler.CreateInfallibleInstance<Location>(
                        location => this.location = location,
                        new LocationProcessor(() => "Por favor ingrese su ubicación (<dirección>, <ciudad>, <departamento>, <país>).")),
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        heading => this.heading = heading,
                        new BasicStringProcessor(() => "Por favor ingrese su rubro.")),
                    ProcessorHandler.CreateInfallibleInstance<Habilitation[]>(
                        habs => this.habilitations = habs.ToList(),
                        new ListProcessor<Habilitation>(
                            () => "Por favor ingrese sus habilitaciones.",
                            new HabilitationProcessor())),
                    ProcessorHandler.CreateInfallibleInstance<string[]>(
                        specializations => this.specializations = specializations.ToList(),
                        new ListProcessor<string>(
                            () => "Por favor ingrese sus especializaciones.",
                            new BasicStringProcessor(() => "Por favor ingrese una especialización.")))
                };
            }

            protected override Result<(UserData, Entrepreneur), string> getResult() =>
                Result<(UserData, Entrepreneur), string>.Ok((
                    this.userData.Unwrap(),
                    new Entrepreneur(
                        userId,
                        this.userData.Unwrap().Name,
                        this.age.Unwrap(),
                        this.location.Unwrap(),
                        this.heading.Unwrap(),
                        this.habilitations.Unwrap(),
                        this.specializations.Unwrap())));
        }
    }
}

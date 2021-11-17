using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Entrepreneurs;
using Library.Core;
using Library.InputHandlers;
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
            ProcessorHandler.CreateInstance<Entrepreneur>(
                e => Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(e),
                new NewEntrepreneurForm(userId)
            ),
            () => null,

            // TODO: implement next state
            () => null
        ) {}

        /// <inheritdoc />
        public override bool IsComplete => false;

        private class NewEntrepreneurForm : FormProcessor<Entrepreneur>
        {
            private string userId;

            private string name;
            private int age;
            private Location location;
            private string heading;
            private IList<Habilitation> habilitations;
            private IList<string> specializations;

            public NewEntrepreneurForm(string userId)
            {
                this.userId = userId;

                this.inputHandlers = new IInputHandler[]
                {
                    ProcessorHandler.CreateInstance<string>(
                        name => this.name = name,
                        new BasicStringProcessor(() => "Please insert your name.")
                    ),
                    ProcessorHandler.CreateInstance<int>(
                        age => this.age = age,
                        new UnsignedInt32Processor(() => "Please insert your age (in years).")
                    ),
                    ProcessorHandler.CreateInstance<Location>(
                        location => this.location = location,
                        new LocationProcessor(() => "Please insert your location (<address>, <city>, <department>, <country>).")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        heading => this.heading = heading,
                        new BasicStringProcessor(() => "Please insert your heading.")
                    ),
                    ProcessorHandler.CreateInstance<Habilitation[]>(
                        habs => this.habilitations = habs.ToList(),
                        new ListProcessor<Habilitation>(
                            new HabilitationProcessor(() => "Please insert your habilitations (they are represented as links to documents which act as evidence).\nInsert them one on one and insert /finish to finish."),
                            s => s.Trim() == "/finish",
                            () => "Insert the next habilitation (or /finish to finish)"
                        )
                    ),
                    ProcessorHandler.CreateInstance<string[]>(
                        specializations => this.specializations = specializations.ToList(),
                        new ListProcessor<string>(
                            new BasicStringProcessor(() => "Please insert your specializations, insert /finish to finish."),
                            s => s.Trim() == "/finish",
                            () => "Insert the next specialization (or /finish to finish)"
                        )
                    )
                };
            }

            protected override Result<Entrepreneur, string> getResult() =>
                Result<Entrepreneur, string>.Ok(new Entrepreneur(userId, name, age.ToString(), location, heading, habilitations, specializations));
        }
    }
}

using System.Collections.Generic;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.InputHandlers;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of get the habilitations of a concrete entrepreneur.
    /// </summary>
    public class CompanyCheckEntrepreneurHabilitationsState : WrapperState
    {
        /// <summary>
        /// Initializes an instance of <see cref="CompanyCheckEntrepreneurHabilitationsState" /> class.
        /// </summary>
        /// <param name="id">The company representative's id.</param>
        public CompanyCheckEntrepreneurHabilitationsState(string id) : base(
            new InputProcessorState<string>(
                new BasicStringProcessor(() => "Inserte el nombre del emprendedor que desea chequear."),
                entrepreneur =>
                {
                    if (Singleton<EntrepreneurManager>.Instance.GetByName(entrepreneur) is Entrepreneur entrepreneurResult)
                    {
                        IList<Habilitation> habilitationsResult = entrepreneurResult.Habilitations;
                        var newState = new CompanyInitialMenuState(id);
                        return (newState, $"{string.Join("\n", habilitationsResult)}\n{newState.GetDefaultResponse()}");
                    }
                    else
                    {
                        var newState = new CompanyInitialMenuState(id);
                        return (newState, $"Lo siento, no encontré ese emprendedor.\n{newState.GetDefaultResponse()}");
                    }
                },
                () => (new CompanyInitialMenuState(id), null)
            )
        )
        {
        }
    }
}

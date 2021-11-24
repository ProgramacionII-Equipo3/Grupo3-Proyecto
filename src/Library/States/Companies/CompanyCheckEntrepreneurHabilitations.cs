using System;
using System.Collections.Generic;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.InputHandlers;
using Library.States;

namespace Library.States.Companies
{
    public class CompanyCheckEntrepreneurHabilitations : WrapperState
    {
        public CompanyCheckEntrepreneurHabilitations(string id) : base(
            InputProcessorState.CreateInstance<string>(
                new BasicStringProcessor(() => "Inserte el nombre del emprendedor que desea chequear."),
                entrepreneur =>
                {
                    if (Singleton<EntrepreneurManager>.Instance.GetByName(entrepreneur) is Entrepreneur entrepreneurResult)
                    {
                        IList<Habilitation> habilitationsResult = entrepreneurResult.Habilitations;
                        return (new CompanyInitialMenuState(string.Join("\n", habilitationsResult)), null);
                    }
                },
                () => (new CompanyInitialMenuState(id), null)
            )
        )
        {
        }
    }
}

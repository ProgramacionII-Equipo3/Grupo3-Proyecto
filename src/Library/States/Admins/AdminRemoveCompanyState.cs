using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;

namespace Library.States.Admins
{
    /// <summary>
    /// This class represents the state of an admin who is removing a company.
    /// </summary>
    public class AdminRemoveCompanyState : InputHandlerState
    {
        /// <summary>
        /// Initializes an instance of <see cref="AdminRemoveCompanyState" />.
        /// </summary>
        public AdminRemoveCompanyState(): base(
            inputHandler: new ProcessorHandler<string>(
                companyName => Singleton<CompanyManager>.Instance.RemoveCompany(companyName)
                    ? null
                    : "No existe una compañía con ese nombre.",
                new BasicStringProcessor(() => "Por favor ingrese el nombre de la compañía que quieres eliminar.")
            ),
            exitState: () => new AdminInitialMenuState(),
            nextState: () => new AdminInitialMenuState()
        ) {}
    }
}

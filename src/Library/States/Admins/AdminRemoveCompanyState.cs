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
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ADMIN;

        /// <summary>
        /// Initializes an instance of <see cref="AdminRemoveCompanyState" />.
        /// </summary>
        public AdminRemoveCompanyState(): base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                companyName => Singleton<CompanyManager>.Instance.RemoveCompany(companyName)
                    ? null
                    : "There's no company with that name.",
                new BasicStringProcessor(() => "Please insert the name of the company you want to remove.")
            ),
            exitState: () => new AdminInitialMenuState(),
            nextState: () => new AdminInitialMenuState()
        ) {}
    }
}

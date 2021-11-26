using Library.Core;
using Library.HighLevel.Companies;
using Library.InputHandlers;


namespace Library.States.Companies
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyRemovePublicationState : WrapperState
    {
        /// <summary>
        /// This state has the responsibility of remove a company publication given its name.
        /// </summary>
        /// <param name="id">The user´s id.</param>
        public CompanyRemovePublicationState(string id) : base(
            InputProcessorState.CreateInstance<string>(
                new BasicStringProcessor(() => "Ingrese el nombre del material o residuo que desea borrar."),
                name =>
                {
                    State newState = new CompanyInitialMenuState(id);
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        if (company.RemovePublication(name))
                        {
                            return (newState, newState.GetDefaultResponse());
                        }
                        else
                        {
                            return (newState, $"Lo siento, no encontré la publicación.\n{newState.GetDefaultResponse()}");
                        }
                    }
                    else
                    {
                        return (newState, $"Lo siento, no te reconozco como un representante de una compañía.\n{newState.GetDefaultResponse()}");
                    }
                },
                exitState: () => (new CompanyInitialMenuState(id), null)
            )
        )
        {
        }
    }
}
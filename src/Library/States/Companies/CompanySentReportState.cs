using System;
using Library.Core;
using Library.HighLevel.Companies;
using Library.HighLevel.Accountability;
using Library.InputHandlers;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of return the material's sent report.
    /// </summary>
    public class CompanySentReportState : WrapperState
    {
        /// <summary>
        /// Initializes an intance of <see cref="CompanySentReportState" />
        /// </summary>
        /// <param name="id">User's id.</param>
        public CompanySentReportState(string id) : base(
            new InputProcessorState<DateTime>(
                new DateProcessor(() => "Ingresa la fecha para realizar el reporte (formato: dd/mm/aaaa)."),
                nextState: time =>
                {
                    State newState = new CompanyInitialMenuState(id);
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        SentMaterialReport report = company.GetMaterialReport(time);
                        return (newState, $"{report}\n{newState.GetDefaultResponse()}");
                    }
                    else
                    {
                        return (newState, $"Lo siento, no te reconozco como un representante de una compañía.\n{newState.GetDefaultResponse()}");
                    }
                },
                () => (new CompanyInitialMenuState(id), null)
            )
        )
        {
        }
    }
}

using System;
using Library.Core;
using Library.HighLevel.Companies;
using Library.HighLevel.Accountability;
using Library.InputHandlers;

namespace Library.States.Companies
{
    /// <summary>
    /// This class has the responsibility of return the material´s sent report.
    /// </summary>
    public class CompanySentReportState : WrapperState
    {
        /// <summary>
        /// Initializes an intance of <see cref="CompanySentReportState" />
        /// </summary>
        /// <param name="id">User´s id.</param>
        /// <returns></returns>
        public CompanySentReportState(string id) : base(
            InputProcessorState.CreateInstance<DateTime>(
                new DateProcessor(() => "Ingresa la fecha para realizar el reporte."),
                nextState: time =>
                {
                    State newState = new CompanyInitialMenuState(id);
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        SentMaterialReport report = (company as ISentMaterialReportCreator).GetMaterialReport(time);
                        return (newState, $"{report}\n{newState.GetDefaultResponse()}");
                    }
                    else
                    {
                        return (newState, $"The user is not a company representative\n{newState.GetDefaultResponse()}");
                    }
                },
                () => (new CompanyInitialMenuState(id), null)
            )
        )
        {
        }
    }
}

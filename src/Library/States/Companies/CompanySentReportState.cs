using System;
using System.Collections.Generic;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.States;
using Library.

namespace Library.States.Companies
{
    public class CompanySentReportState : WrapperState
    {
        public CompanySentReportState(string id) : base(
            exitState: () => new CompanyInitialMenuState(),
            nextState: () => new CompanyInitialMenuState(),
            inputHandler: ProcessorHandler.CreateInstance<DateTime>(
                time =>
                {
                    if (Singleton<CompanyManager>.Instance.GetCompanyOf(id) is Company company)
                    {
                        SentMaterialReport report = (company as ISentMaterialReportCreator).GetMaterialReport(time);
                        
                    }
                    else
                    {
                        return "This user is not a company representative.";
                    }
                }
            )
        )
        {
        }
    }
}
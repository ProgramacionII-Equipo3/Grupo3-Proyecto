using System;
using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    public class EntrepreneurCreateReportState : WrapperState
    {

        public EntrepreneurCreateReportState(Entrepreneur entrepreneur) : base(
            InputProcessorState.CreateInstance<DateTime>(
                new DateProcessor(() => "Ingresa la fecha para ver el reporte correspondiente."),
                dateTime =>
                {
                    ReceivedMaterialReport report = (entrepreneur as IReceivedMaterialReportCreator).GetMaterialReport(dateTime);
                    State newState = new EntrepreneurInitialMenuState(entrepreneur.Id);
                    return (newState, $"{report}\n{newState.GetDefaultResponse()}");
                },
                () => (new EntrepreneurInitialMenuState(entrepreneur.Id), null)
            )
        )
        {

        }
    }
}
using System;
using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.HighLevel.Accountability;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents a <see cref="State" /> for an entrepreneur who is creating a report.
    /// </summary>
    public class EntrepreneurCreateReportState : WrapperState
    {
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurCreateReportState" />.
        /// </summary>
        /// <param name="entrepreneur">The data of the entrepreneur.</param>
        public EntrepreneurCreateReportState(Entrepreneur entrepreneur) : base(
            new InputProcessorState<DateTime>(
                new DateProcessor(() => "Ingresa la fecha para ver el reporte correspondiente."),
                dateTime =>
                {
                    ReceivedMaterialReport report = entrepreneur.GetMaterialReport(dateTime);
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
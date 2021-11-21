using System;
using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    public class EntrepreneurCreateReportState : WrapperState
    {

        public EntrepreneurCreateReportState(Entrepreneur entrepreneur) : base(
            InputProcessorState.CreateInstance<DateTime>(
                new DateProcessor(() => ""),
                dateTime =>
                {
                    (entrepreneur as IReceivedMaterialReportCreator).
                }
            )
        )
        {

        }
    }
}
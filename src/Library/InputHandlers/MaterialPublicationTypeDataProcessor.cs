using System;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.InputHandlers
{
    public class MaterialPublicationTypeDataProcessor : InputProcessor<MaterialPublicationTypeData>
    {
        private Func<string> initialResponseGetter;
        private bool askForDate = false;
        private MaterialPublicationTypeData? result = null;
        private DateProcessor dateProcessor = new DateProcessor(() => "Which is the scheduled date?");

        public MaterialPublicationTypeDataProcessor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        public override string GetDefaultResponse() =>
            askForDate
                ? dateProcessor.GetDefaultResponse()
                : (this.initialResponseGetter)();

        public override Result<bool, string> ProcessInput(string msg)
        {
            if(this.askForDate)
            {
                if(dateProcessor.GenerateFromInput(msg) is Result<DateTime, string> result)
                {
                    return result.SwitchOk(
                        dateTime =>
                        {
                            this.result = MaterialPublicationTypeData.Scheduled(dateTime);
                            return true;
                        }
                    );
                } else
                {
                    this.askForDate = false;
                    return Result<bool, string>.Err(this.GetDefaultResponse());
                }
            } else
            {
                switch(msg.Trim().ToLower())
                {
                    case "/normal":
                        this.result = MaterialPublicationTypeData.Normal();
                        return Result<bool, string>.Ok(true);
                    case "/scheduled":
                        this.askForDate = true;
                        return Result<bool, string>.Err(this.GetDefaultResponse());
                    case "/continuous":
                        this.result = MaterialPublicationTypeData.Continuous();
                        return Result<bool, string>.Ok(true);
                    default:
                        return Result<bool, string>.Err($"Invalid option.\n{this.GetDefaultResponse()}");
                }
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            this.askForDate = false;
            this.result = null;
        }

        /// <inheritdoc />
        protected override Result<MaterialPublicationTypeData, string> getResult() => Result<MaterialPublicationTypeData, string>.Ok(this.result.Unwrap());
    }
}

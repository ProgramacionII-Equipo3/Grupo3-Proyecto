using System;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents an <see cref="InputProcessor{T}" /> which generates objects of type <see cref="MaterialPublicationTypeData" />.
    /// </summary>
    public class MaterialPublicationTypeDataProcessor : InputProcessor<MaterialPublicationTypeData>
    {
        private Func<string> initialResponseGetter;
        private bool askForDate = false;
        private MaterialPublicationTypeData? result = null;
        private DateProcessor dateProcessor = new DateProcessor(() => "¿Cuál es la fecha agendada?");

        /// <summary>
        /// Initializes an instance of <see cref="MaterialPublicationTypeDataProcessor" />.
        /// </summary>
        /// <param name="initialResponseGetter">The function which determines the processor's default response.</param>
        public MaterialPublicationTypeDataProcessor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() =>
            askForDate
                ? dateProcessor.GetDefaultResponse()
                : (this.initialResponseGetter)();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            if (this.askForDate)
            {
                if (dateProcessor.GenerateFromInput(msg) is Result<DateTime, string> result)
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
                switch(msg.Trim().ToLowerInvariant())
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
                        return Result<bool, string>.Err($"Opción inválida.\n{this.GetDefaultResponse()}");
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

using System;
using Library.Core;
using Library.Core.Distribution;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;
using Library.Utils;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class acts as a processor to get a <see cref="UserData" />.
    /// </summary>
    public class UserDataProcessor : FormProcessor<UserData>
    {
        private bool isComplete;
        private UserData.Type userType;
        private string? name;
        private int? phoneNumber;
        private string? email;

        /// <summary>
        /// Initialize an instance of <see cref="UserData" />.
        /// </summary>
        /// <param name="isComplete">The <see cref="UserData.IsComplete" /> attribute/property.</param>
        /// <param name="userType">The <see cref="UserData.UserType" /> attribute/property.</param>
        public UserDataProcessor(bool isComplete, UserData.Type userType)
        {
            this.isComplete = isComplete;
            this.userType = userType;

            this.inputHandlers = new InputHandler[]
            {
                new ProcessorHandler<string>(
                    s =>
                    {
                        if(Singleton<SessionManager>.Instance.GetByName(s) is not null)
                        {
                            return "Ya hay un usuario con ese nombre.";
                        }
                        else
                        {
                            this.name = s;
                            return null;
                        }
                    },
                    new BasicStringProcessor(() => "Por favor, inserte su nombre completo.")),
                ProcessorHandler<ClassWrapper<int>?>.CreateInfallibleInstance(
                    n =>
                    {
                        this.phoneNumber = n?.Value;
                    },
                    new OptionalProcessor<ClassWrapper<int>>(
                        PipeProcessor<int, ClassWrapper<int>>.CreateInfallibleInstance(
                            n => (ClassWrapper<int>)n,
                            new PhoneNumberProcessor(() => "Por favor, ingrese su número de teléfono (opcional).")))),
                ProcessorHandler<string?>.CreateInfallibleInstance(
                    s => this.email = s,
                    new OptionalProcessor<string>(
                        new EmailProcessor(() => "Por favor, ingrese su email (opcional).")))
            };
        }

        /// <inheritdoc />
        protected override Result<UserData, string> getResult() =>
            Result<UserData, string>.Ok(
                new UserData(this.name.Unwrap(), this.isComplete, this.userType, this.email, this.phoneNumber));
    }
}

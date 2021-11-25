using Library.Core;
using Library.Core.Processing;
using Library.InputHandlers.Abstractions;

namespace Library.InputHandlers
{
    public class UserDataProcessor : FormProcessor<UserData>
    {
        private bool isComplete;
        private UserData.Type userType;
        private string name;
        private int? phoneNumber;
        private string? email;

        public UserDataProcessor(bool isComplete, UserData.Type userType)
        {
            this.inputHandlers = new InputHandler[]
            {
                ProcessorHandler.CreateInstance<string>()
            };
        }

        protected override Result<UserData, string> getResult()
        {
            throw new System.NotImplementedException();
        }
    }
}

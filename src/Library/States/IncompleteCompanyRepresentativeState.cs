using System.Collections.Generic;
using Library.Core;
using Library.HighLevel.Companies;

namespace Library.States
{
    /// <summary>
    /// This class represents the state of a company representative who is yet to be fully logged in to the platform.
    /// </summary>
    public class IncompleteCompanyRepresentativeState : State
    {
        private State step = null;
        private string name;
        private string title;
        private string location;
        private int phoneNumber;
        private string email;

        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            if(this.step == null)
            {
                this.name = msg.Trim();
                this.step = new AssignExistingCompanyAmongQueueState(this.name);
                return (this, "");
            }

            return (null, null);
        }

        private State NextStateGivenCompanyName(string name)
        {
            
        }

        private class AssignExistingCompanyAmongQueueState : State
        {
            private Queue<Company> companies;

            public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
            {
                return (null, null);
            }
        }

    }
}

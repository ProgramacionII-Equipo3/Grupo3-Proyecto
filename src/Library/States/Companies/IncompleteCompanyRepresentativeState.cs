using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;

namespace Library.States.Companies
{
    /// <summary>
    /// This class represents the state of a company representative who is yet to be fully logged in to the platform.
    /// </summary>
    public partial class IncompleteCompanyRepresentativeState : State
    {
        private InputProcessor<Company>? companyGetter = null;
        private string name = string.Empty;

        /// <inheritdoc />
        public override (State?, string?, UserData?) ProcessMessage(string id, UserData data, string msg)
        {
            if(this.companyGetter == null)
            {
                this.name = msg.Trim();
                var (newStep, response) = this.nextStateGivenCompanyName(this.name);
                this.companyGetter = newStep;
                return (this, response, null);
            }

            if(companyGetter.GenerateFromInput(msg) is Result<Company, string> result)
            {
                return result.Map<(State?, string?, UserData?)>(
                    company =>
                    {
                        company.AddUser(id);
                        return (
                            // TODO: Implement next state
                            null,
                            "Welcome to the platform. What do you want to do?",
                            new UserData(data.Name, true, data.UserType, data.ContactInfo.Email, data.ContactInfo.PhoneNumber));
                    },
                    e => (this, e, null)
                );
            } else
            {
                this.companyGetter = null;
                return (this, null, null);
            }
        }

        /// <inheritdoc />
        public override string GetDefaultResponse()
        {
            if(this.companyGetter == null)
            {
                return "Please insert the company's name.";
            } else
            {
                return this.companyGetter.GetDefaultResponse();
            }
        }

        private (InputProcessor<Company>, string) nextStateGivenCompanyName(string name)
        {
            InputProcessor<Company> getter;
            if(Singleton<CompanyManager>.Instance.GetByName(name) is Company perfectMatch)
            {
                getter = new AssignExistingCompanyState(perfectMatch);
            } else
            {
                getter = new CreateNewCompanyState(this);
            }
                return (getter, getter.GetDefaultResponse());
        }
    }
}

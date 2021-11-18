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
        private IInputProcessor<Company> companyGetter = null;
        private string name;

        /// <inheritdoc />
        public override (State, string) ProcessMessage(string id, UserData data, string msg)
        {
            if(this.companyGetter == null)
            {
                this.name = msg.Trim();
                var (newStep, response) = this.nextStateGivenCompanyName(this.name);
                this.companyGetter = newStep;
                return (this, response);
            }

            return companyGetter.GenerateFromInput(msg).Map(
                result => result.Map(
                    company =>
                    {
                        company.AddUser(id);
                        return (null, "Welcome to the platform. What do you want to do?");
                    },
                    e => (this, e)
                ),
                () =>
                {
                    this.companyGetter = null;
                    return (this, this.GetDefaultResponse());
                }
            );
        }

        /// <inheritdoc />
        public override bool IsComplete => false;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.COMPANY;

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

        private (IInputProcessor<Company>, string) nextStateGivenCompanyName(string name)
        {
            IInputProcessor<Company> getter;
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

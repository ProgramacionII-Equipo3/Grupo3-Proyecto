using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;

namespace Library.States
{
    /// <summary>
    /// This class represents the state of a company representative who is yet to be fully logged in to the platform.
    /// </summary>
    public partial class IncompleteCompanyRepresentativeState : State
    {
        private IInputProcessor<Company> companyGetter = null;
        private string name;

        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            if(this.companyGetter == null)
            {
                this.name = msg.Trim();
                var (newStep, response) = this.nextStateGivenCompanyName(this.name);
                this.companyGetter = newStep;
                return (this, response);
            }

            if(companyGetter.GenerateFromInput(msg) is Result<Company, string> result)
            {
                return result.Map(
                    company =>
                    {
                        company.AddUser(id);
                        return (null, "Welcome to the platform. What do you want to do?");
                    },
                    e => (this, e)
                );
            } else
            {
                this.companyGetter = null;
                return (this, this.getDefaultResponse());
            }
        }

        private string getDefaultResponse()
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
            if(CompanyManager.GetByName(name) is Company perfectMatch)
            {
                getter = new AssignExistingCompanyState(perfectMatch);
            } else
            {
                List<Company> companies = CompanyManager.GetCompaniesWithNamesSimilarTo(name).ToList();
                if(companies.Count > 0)
                    getter = new AssignExistingCompanyInListState(companies);
                else
                    getter = new CreateNewCompanyState(this);
            }
                return (getter, getter.GetDefaultResponse());
        }
    }
}

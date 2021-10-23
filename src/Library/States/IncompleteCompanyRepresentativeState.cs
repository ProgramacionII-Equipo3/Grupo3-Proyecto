using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.HighLevel.Companies;

namespace Library.States
{
    /// <summary>
    /// This class represents the state of a company representative who is yet to be fully logged in to the platform.
    /// </summary>
    public partial class IncompleteCompanyRepresentativeState : State
    {
        private MiddleState step = null;
        private string name;

        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            if(this.step == null)
            {
                this.name = msg.Trim();
                var (newStep, response) = this.nextStateGivenCompanyName(this.name);
                this.step = newStep;
                return (this, response);
            }

            var (company, response2) = step.processMessage(msg);
            if(company == null)
            {
                if(response2 == null)
                {
                    this.step = null;
                    return (this, "Please insert the name of the company you represent.");
                }
                return (this, response2);
            }

            company.AddUser(id);
            return (null, "Welcome to the platform. What do you want to do?");
        }

        private (MiddleState, string) nextStateGivenCompanyName(string name)
        {
            if(CompanyManager.GetByName(name) is Company perfectMatch)
            {
                return (new AssignExistingCompanyState(this, perfectMatch), $"There's already a company called {perfectMatch.Name}. Is this the company you want to assign to?");
            } else
            {
                List<Company> companies = CompanyManager.GetCompaniesWithNamesSimilarTo(name).ToList();
                if(companies.Count > 0)
                    return (new AssignExistingCompanyInListState(this, companies), "The following are a list of companies with a similar name. Which is the one you represent?" + companies.Select(company => "\n" + company.Name));
                else
                    return (new CreateNewCompanyState(this), "");
            }
        }
    }
}

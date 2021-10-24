using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class AssignExistingCompanyInListState : IInputProcessor<Company>
        {
            private List<Company> companies;
            private Company chosenCompany = null;

            public AssignExistingCompanyInListState(List<Company> companies)
            {
                this.companies = companies;
            }

            public string GetDefaultResponse() =>
                "The following are a list of companies with a similar name. Which is the one you represent?"
                + this.companies.Select(company => "\n" + company.Name);

            (Company, string) IInputProcessor<Company>.getResult() => (this.chosenCompany, null);

            (bool, string) IInputProcessor<Company>.getInput(string msg)
            {
                if(msg == "/esc")
                    return (false, null);

                msg = msg.Trim();

                if(companies.Where(company => company.Name == msg).FirstOrDefault() is Company result)
                {
                    this.chosenCompany = result;
                    return (true, null);
                }
                
                return (default, "That name is not on the list.");
            }

            void IInputProcessor<Company>.Reset()
            {
                this.chosenCompany = null;
            }
        }
    }
}

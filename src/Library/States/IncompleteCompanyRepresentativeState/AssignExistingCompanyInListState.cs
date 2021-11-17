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
            private IList<Company> companies;
            private Company chosenCompany = null;

            public AssignExistingCompanyInListState(IList<Company> companies)
            {
                this.companies = companies;
            }

            public string GetDefaultResponse() =>
                "The following are a list of companies with a similar name. Which is the one you represent?"
                + this.companies.Select(company => "\n" + company.Name);

            Result<Company, string> IInputProcessor<Company>.getResult() => Result<Company, string>.Ok(this.chosenCompany);

            Result<bool, string> IInputHandler.ProcessInput(string msg)
            {
                if(msg == "/esc")
                    return Result<bool, string>.Ok(false);

                msg = msg.Trim();

                if(companies.Where(company => company.Name == msg).FirstOrDefault() is Company result)
                {
                    this.chosenCompany = result;
                    return Result<bool, string>.Ok(true);
                }
                
                return Result<bool, string>.Err("That name is not on the list.");
            }

            void IInputHandler.Reset()
            {
                this.chosenCompany = null;
            }
        }
    }
}

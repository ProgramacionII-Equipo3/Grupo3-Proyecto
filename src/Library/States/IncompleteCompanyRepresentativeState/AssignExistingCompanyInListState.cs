using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class AssignExistingCompanyInListState : MiddleState
        {
            private List<Company> companies;

            public AssignExistingCompanyInListState(IncompleteCompanyRepresentativeState parent, List<Company> companies): base(parent)
            {
                this.companies = companies;
            }

            public override (Company, string) processMessage(string msg)
            {
                if(msg == "/esc")
                    return (null, null);

                msg = msg.Trim();

                if(companies.Where(company => company.Name == msg).FirstOrDefault() is Company result)
                    return (result, null);
                
                return (null, "That name is not on the list.");
            }
        }
    }
}

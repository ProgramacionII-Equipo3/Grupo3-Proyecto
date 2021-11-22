using System;

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
        public override (State?, string?) ProcessMessage(string id, ref UserData data, string msg)
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
                var (state, s, f) = result.Map<(State?, string?, Func<UserData, UserData>?)>(
                    company =>
                    {
                        company.AddUser(id);
                        return (
                            new CompanyInitialMenuState(id),
                            null,
                            (data) =>
                            {
                                data.IsComplete = true;
                                return data;
                            }
                        );
                    },
                    e => (this, e, null)
                );
                if(f != null) data = f(data);
                return (state, s);
            } else
            {
                this.companyGetter = null;
                return (this, null);
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
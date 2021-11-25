using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Distribution;
using Library.Core.Processing;
using Library.InputHandlers;
using Library.HighLevel.Companies;

namespace Library.States.Companies
{
    /// <summary>
    /// This class represents the state of a company representative who is yet to be fully logged in to the platform.
    /// </summary>
    public partial class IncompleteCompanyRepresentativeState : State
    {
        private InputProcessor<Company>? companyGetter = null;
        private InputProcessor<UserData> userDataGetter = new UserDataProcessor(false, UserData.Type.COMPANY);
        private UserData? userData = null;
        private string name = string.Empty;

        /// <inheritdoc />
        public override (State?, string?) ProcessMessage(string id, string msg)
        {
            if(userData is null)
            {
                if (userDataGetter.GenerateFromInput(msg) is Result<UserData, string> userDataResult)
                {
                    return (
                        this,
                        userDataResult.Map(
                            v =>
                            {
                                this.userData = v;
                                return this.GetDefaultResponse();
                            },
                            e => e));
                }
                else
                {
                    return (null, null);
                }
            }

            if (this.companyGetter is null)
            {
                if(msg == "\\") return (null, null);
                this.name = msg.Trim();
                var (newStep, response) = this.nextStateGivenCompanyName(this.name);
                this.companyGetter = newStep;
                return (this, response);
            }

            if (companyGetter.GenerateFromInput(msg) is Result<Company, string> result)
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
                                this.userData.IsComplete = true;
                                return this.userData;
                            }
                        );
                    },
                    e => (this, e, null)
                );
                if (f is not null)
                {
                    UserSession session = Singleton<SessionManager>.Instance.GetById(id) !;
                    session.UserData = f(session.UserData);
                }
                return (state, s);
            }
            else
            {
                this.companyGetter = null;
                return (this, null);
            }
        }

        /// <inheritdoc />
        public override string GetDefaultResponse()
        {
            if (this.userData is null)
            {
                return this.userDataGetter.GetDefaultResponse();
            }
            else if (this.companyGetter is null)
            {
                return "Por favor ingresa el nombre de la empresa.";
            }
            else
            {
                return this.companyGetter.GetDefaultResponse();
            }
        }

        private (InputProcessor<Company>, string) nextStateGivenCompanyName(string name)
        {
            InputProcessor<Company> getter;
            if (Singleton<CompanyManager>.Instance.GetByName(name) is Company perfectMatch)
            {
                getter = new AssignExistingCompanyState(perfectMatch);
            }
            else
            {
                getter = new CreateNewCompanyState(this);
            }
                return (getter, getter.GetDefaultResponse());
        }
    }
}
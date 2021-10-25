using System;
using System.Globalization;
using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class CreateNewCompanyState : MiddleState
        {

            private byte step = 0;
            private string heading;
            private string location;
            private int phoneNumber;
            private string email;

            public CreateNewCompanyState(IncompleteCompanyRepresentativeState parent): base(parent) {}

            public override (Company, string) processMessage(string msg)
            {
                if(msg == "/esc")
                    return (null, null);
                
                msg = msg.Trim();
                switch(step)
                {
                    case 0:
                    return getInput(
                        msg,
                        s => string.IsNullOrWhiteSpace(msg)
                             ? (null, "Please insert the company's heading.")
                             : (s, null),
                        ref this.heading,
                        () => (null, "Please insert the company's location.")
                    );
                    case 1:
                    return getInput(
                        msg,
                        s => string.IsNullOrWhiteSpace(msg)
                             ? (null, "Please insert the company's location.")
                             : (s, null),
                        ref this.location,
                        () => (null, "Please insert the company's phone number.")
                    );
                    case 2:
                    return getInput(
                        msg,
                        s => 
                        {
                            int phoneNumber;
                            if(int.TryParse(msg, NumberStyles.AllowThousands, CultureInfo.InvariantCulture.NumberFormat, out phoneNumber))
                            {
                                return (phoneNumber, null);
                            }
                            return (-1, "Insert a valid phone number.");
                        },
                        ref this.phoneNumber,
                        () => (null, "Please insert the company's email")
                    );
                    case 3:
                    return getInput(
                        msg,
                        s => Utils.IsValidEmail(s) ? (s, null) : (null, "Please insert a valid email."),
                        ref this.email,
                        () =>
                        {
                            Company result = CompanyManager.CreateCompany(
                                name: this.parent.name,
                                contactInfo: new Library.Core.ContactInfo
                                {
                                    Email = this.email,
                                    PhoneNumber = this.phoneNumber
                                },
                                heading: this.heading
                            );
                            return (result, null);
                        }
                    );
                    default: throw new Exception();
                }
            }

            private (Company, string) getInput<T>(string input, Func<string, (T, string)> convert, ref T reference, Func<(Company, string)> func)
            {
                var (val, errMsg) = convert(input);
                if(errMsg == null)
                {
                    reference = val;
                    this.step++;
                    return func();
                }
                return (null, errMsg);
            }
        }
    }
}

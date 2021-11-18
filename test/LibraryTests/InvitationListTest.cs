using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Library;
using Library.Core.Invitations;

namespace UnitTests
{
    /// <summary>
    /// This class represents tests concerning the <see cref="InvitationList{T}" /> class.
    /// </summary>
    public class InvitationListTest
    {
        /// <summary>
        /// Setup function.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// Tests whether adding invitations via the <see cref="InvitationManager" /> adds them in the <see cref="InvitationList{T}" />,<br />
        /// and whether validating them removes them.
        /// </summary>
        [Test]
        public void TestInvitationList()
        {
            Singleton<InvitationManager>.Instance.CreateInvitation("abc", CustomInvitation.CreateInstance);
            Singleton<InvitationManager>.Instance.CreateInvitation("123", CustomInvitation.CreateInstance);
            Singleton<InvitationManager>.Instance.CreateInvitation("___", CustomInvitation.CreateInstance);

            foreach(string i in new string[]
            {
                "abc",
                "123",
                "___"
            })
            {
                Assert.Contains(
                    new CustomInvitation(i),
                    Singleton<InvitationList<CustomInvitation>>.Instance.GetInvitations().ToArray()
                );
                Assert.AreEqual(
                    $"Invitation accepted: {i}, AAA",
                    Singleton<InvitationManager>.Instance.ValidateInvitation(i, "AAA")
                );
            }
        }

        private class CustomInvitation : Invitation, IEquatable<CustomInvitation>
        {
            public static IList<string> validated = new List<string>();

            public CustomInvitation(string code): base(code) {}

            public static CustomInvitation CreateInstance(string code) => new CustomInvitation(code);

            /// <inheritdoc />
            public override string Validate(string userId)
            {
                validated.Add(this.Code);
                return $"Invitation accepted: {this.Code}, {userId}";
            }

            public bool Equals(CustomInvitation other) =>
                this.Code == other.Code;

            public override bool Equals(object obj) =>
                obj is CustomInvitation inv ? this.Code == inv.Code : false;
            
            public override int GetHashCode() =>
                this.Code.GetHashCode();

            public static bool operator ==(CustomInvitation a, CustomInvitation b) =>
                a.Code == b.Code;

            public static bool operator !=(CustomInvitation a, CustomInvitation b) =>
                a.Code != b.Code;
        }
    }
}

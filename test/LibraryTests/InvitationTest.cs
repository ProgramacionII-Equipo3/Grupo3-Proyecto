using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Library;
using Library.Core.Invitations;

namespace ProgramTests
{
    /// <summary>
    /// This class represents tests concerning invitations.
    /// </summary>
    [TestFixture]
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
                    Singleton<InvitationList<CustomInvitation>>.Instance.Invitations.ToArray());
                Assert.AreEqual(
                    $"Invitation accepted: {i}, AAA",
                    Singleton<InvitationManager>.Instance.ValidateInvitation(i, "AAA"));
            }

            Assert.Zero(Singleton<InvitationList<CustomInvitation>>.Instance.Invitations.Count);
        }

        private class CustomInvitation : Invitation, IEquatable<CustomInvitation>
        {
            public static IList<string> validated = new List<string>();

            public CustomInvitation(string code) : base(code)
            {
            }

            public static CustomInvitation CreateInstance(string code) => new CustomInvitation(code);

            /// <inheritdoc />
            public override string Validate(string userId)
            {
                validated.Add(this.Code);
                return $"Invitation accepted: {this.Code}, {userId}";
            }

            public bool Equals(CustomInvitation? other) =>
                other is not null && this.Code == other.Code;

            public override bool Equals(object? obj) =>
                obj is CustomInvitation inv && this.Code == inv.Code;
            
            public override int GetHashCode() =>
                this.Code.GetHashCode(StringComparison.InvariantCulture);

            public static bool operator ==(CustomInvitation a, CustomInvitation b) =>
                a.Code == b.Code;

            public static bool operator !=(CustomInvitation a, CustomInvitation b) =>
                a.Code != b.Code;
        }

        /// <summary>
        /// Tests the user story of creating invitations (admin).
        /// </summary>
        [Test]
        public void CreateInvitationTest()
        {
            RuntimeTest.BasicRuntimeTest("create-invitations", () =>
            {
                // Create a message invitation for "Company1"
                string? invitationCode;
                {
                    List<(string, string)> responses = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode, Is.Not.Null);
                }
                Assert.AreEqual(1, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Create a message invitation for "Company2"
                string? invitationCode2;
                {
                    List<(string, string)> responses = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode2 = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode2, Is.Not.Null);
                }
                Assert.AreEqual(2, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Create a message invitation for "Company3"
                string? invitationCode3;
                {
                    List<(string, string)> responses = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode3 = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2);
                    Assert.That(invitationCode3, Is.Not.Null);
                }
                Assert.AreEqual(3, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);
            });
        }
    }
}

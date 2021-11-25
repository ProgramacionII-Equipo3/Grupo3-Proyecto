using System;
using System.Collections.Generic;
using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.States.Admins;
using Library.Utils;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// This class holds a single test which executes a long runtime code into a ConsolePlatform-like platform.
    /// </summary>
    [TestFixture]
    public class RuntimeTest
    {
        /// <summary>
        /// Realizes a test from the point of view of the users.
        /// </summary>
        [Test]
        public void RuntimeBasicTest()
        {
            try
            {
                runtimeBasicTestInner();
            }
            finally
            {
                SerializationUtils.SerializeAllIntoJson("../../Memory-test");
            }
        }

        private void runtimeBasicTestInner()
        {
            ProgramaticMultipleUserPlatform platform = new ProgramaticMultipleUserPlatform();

            Singleton<SessionManager>.Instance.NewUser(
                "Admin1",
                new UserData("Martín", true, UserData.Type.ADMIN, null, null),
                new AdminInitialMenuState());

            // Sign up an user of id "Entrepreneur1" as entrepreneur
            platform.ReceiveMessages(
                "Entrepreneur1",
                "/help",
                "/start -e",
                "Santiago",
                "/esc",
                "/esc",
                "19",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "Maderas",
                "/add",
                "https://www.wikipedia.org",
                "Description1",
                "/finish",
                "/add",
                "E1",
                "/add",
                "E2",
                "/finish");

            Assert.That(Singleton<SessionManager>.Instance.GetById("Entrepreneur1"), Is.Not.Null);
            Assert.That(Singleton<SessionManager>.Instance.GetByName("Santiago"), Is.Not.Null);
            Assert.AreEqual(
                Singleton<SessionManager>.Instance.GetById("Entrepreneur1"),
                Singleton<SessionManager>.Instance.GetByName("Santiago"));
            
            {
                // Create a message invitation for "Company1"
                string? invitationCode;
                {
                    List<(string, string)> responses = platform.ReceiveMessages(
                        "Admin1",
                        "/invitecompany");
                    invitationCode = AdminStatesTest.IsCreateInvitationResponseRegex(responses[0].Item2); ;
                    Assert.That(invitationCode, Is.Not.Null);
                }
                Assert.AreEqual(1, Singleton<Library.Core.Invitations.InvitationManager>.Instance.InvitationCount);

                // Sign up an user of id "Company1" as company representative
                platform.ReceiveMessages(
                    "Company1",
                   $"/start {invitationCode}",
                    "Roberto",
                    "/esc",
                    "/esc",
                    "Teogal",
                    "Maderas",
                    "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                    "098140124",
                    "teogal@gmail.com");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.States.Admins;
using ProgramTests.Utils;

namespace ProgramTests
{
    /// <summary>
    /// This class represents unit tests related to administers.
    /// </summary>
    [TestFixture]
    public class AdminStatesTest
    {

        private static Regex createInvitationResponseRegex = new Regex(
                "El nuevo código de invitación es: (?<invitationcode>\\w+).\\n"
              + "¿Qué quieres hacer\\?\\n"
              + "        /invitecompany: Crea una invitación para un representante de una companía y obtiene su respectivo código.\\n"
              + "        /removecompany: Elimina una compañía y sus respectivos usuarios.\\n"
              + "        /removeuser: Elimina un usuario.",
                RegexOptions.Compiled);

        /// <summary>
        /// Checks whether a message is a response from creating an invitation,
        /// returning the invitation's code.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <returns>The invitation's code, if there is.</returns>
        public static string? IsCreateInvitationResponseRegex(string msg)
        {
            Match match = createInvitationResponseRegex.Match(msg);
            return match.Success
                ? match.Groups["invitationcode"].Value
                : null;
        }

        /// <summary>
        /// Tests the class <see cref="AdminInitialMenuState" />'s /createcompany option.
        /// </summary>
        [Test]
        public void TestAdminCreateInvitation()
        {
            Singleton<SessionManager>.Instance.RemoveUser("___");
            Console.WriteLine();
            BasicUtils.CreateUser(new AdminInitialMenuState());
            ProgramaticPlatform platform = new ProgramaticPlatform(
                "___",
                new string[]
                {
                    "/invitecompany"
                }
            );

            platform.Run();

            foreach(string msg in platform.ReceivedMessages)
            {
                Console.WriteLine("\t--------");
                Console.WriteLine(msg);
                Console.WriteLine("\t--------");
            }

            Match match = createInvitationResponseRegex.Match(platform.ReceivedMessages[0]);

            Singleton<SessionManager>.Instance.RemoveUser("___");

            Assert.That(match.Success, Is.True);

            string invitationCode = match.Groups["invitationcode"].Value;
            Console.WriteLine($"Invitation code: {invitationCode}");
       }

       /// <summary>
        /// Tests whether an admin can be successfully created from code.
        /// </summary>
        [Test]
        public void CreateAdminTest()
        {
            RuntimeTest.BasicRuntimeTest("create-admin", () =>
            {
                Singleton<SessionManager>.Instance.NewUser(
                    "Admin1",
                    new UserData("Martín", true, UserData.Type.ADMIN, null, null),
                    new AdminInitialMenuState());
                CheckUtils.CheckUserAndEquality(new UserData("Martín", true, UserData.Type.ADMIN, null, null), "Admin1");
            });
        }

        /// <summary>
        /// Tests the user story of removing an admin as admin.
        /// </summary>
        [Test]
        public void RemoveAdminAsAdminTest()
        {
            Singleton<SessionManager>.Instance.NewUser(
                "Admin2",
                new UserData("Bianca", true, UserData.Type.ADMIN, null, null),
                new AdminInitialMenuState()); 

            RuntimeTest.BasicRuntimeTest("remove-admin-as-admin", () =>
            {
                List<(string, string)> responses = Singleton<ProgramaticMultipleUserPlatform>.Instance.ReceiveMessages(
                    "Admin1",
                    "/removeuser",
                    "Bianca");
                
                string finalMessage = responses[responses.Count - 1].Item2;

                string expected = "No se puede eliminar un administrador.";
                string actual = finalMessage.Split('\n')[0];  

                Assert.AreEqual(expected, actual);
            });
        }
    }
}
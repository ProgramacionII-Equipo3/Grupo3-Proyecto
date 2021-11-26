using System;
using System.Collections.Generic;
using System.Globalization;
using Library;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.Platforms.Telegram;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace ProgramTests
{
    /// <summary>
    /// Test if an Entrepreneur can register into the platform.
    /// </summary>
    [TestFixture]
    public class EntrepreneurRegisterTest
    {
        private string? juanId;
        private Message nameMessage;
        private Message ageMessage;
        private LocationApiClient? provider;
        private Location? location;
        private Message headingMessage;
        private Message habilitationsMessage;
        private Message specializationsMessage;

        /// <summary>
        /// Creates a message with the correspondent information.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.juanId = "Telegram_2567104974";
            this.nameMessage = new Message("Juan", this.juanId);
            this.ageMessage = new Message("23", this.juanId);
            this.headingMessage = new Message("carpintero", this.juanId);
            this.habilitationsMessage = new Message("/command link1 link2", this.juanId);
            this.specializationsMessage = new Message("/command specialization1, specialization2", this.juanId);
            this.provider = new LocationApiClient();
            this.location = this.provider.GetLocation("Av. 8 de Octubre 2738");
        }

        /// <summary>
        /// This test evaluate if the entrepreneur is registered with their correct information.
        /// </summary>
        [Test]
        public void EntrepreneurRegister()
        {
            string[] habilitationsMessageSplitted = this.habilitationsMessage.Text.Trim().Split();
            IList<Habilitation> habilitations = new List<Habilitation>();

            for (int i = 1; i < habilitationsMessageSplitted.Length; i++)
            {
                Habilitation habilitation = new Habilitation(habilitationsMessageSplitted[i], "description");
                habilitations.Add(habilitation);
            }

            string[] specializationMessageSplitted = this.habilitationsMessage.Text.Trim().Split();
            IList<string> specializations = new List<string>();

            for (int i = 1; i < specializationMessageSplitted.Length; i++)
            {
                string specialization = new string(specializationMessageSplitted[i]);
                specializations.Add(specialization);
            }

            Entrepreneur juan = new Entrepreneur(this.juanId!, this.nameMessage.Text, int.Parse(this.ageMessage.Text, CultureInfo.InvariantCulture), this.location!, this.headingMessage.Text, habilitations, specializations);
            Singleton<EntrepreneurManager>.Instance.NewEntrepreneur(juan);

            // The user must be in the list of entrepreneurs to be registered.
            string idExpected = this.nameMessage.Id;

            int indexnameUser = Singleton<EntrepreneurManager>.Instance.Entrepreneurs.IndexOf(juan);
            Assert.AreEqual(Singleton<EntrepreneurManager>.Instance.Entrepreneurs[indexnameUser].Id, idExpected);

            // Evaluate if the habilitations, specializations and name are registered correctly.
            string nameExpected = this.nameMessage.Text;
            Assert.AreEqual(habilitations, juan.Habilitations);
            Assert.AreEqual(specializations, juan.Specializations);
            Assert.AreEqual(nameExpected, juan.Name);
            Singleton<EntrepreneurManager>.Instance.RemoveUserAsEntrepreneurByName(this.nameMessage.Text);
        }

        /// <summary>
        /// Tests the course of registering an entrepreneur from user input.
        /// </summary>
        [Test]
        public void EntrepreneurRegisterFromUserInput()
        {
            UnitTests.ProgramaticPlatform platform = new UnitTests.ProgramaticPlatform(
                "___",
                "/start -e",
                "Santiago",
                "18",
                "Av. 8 de Octubre, Montevideo, Montevideo, Uruguay",
                "Maderas",
                "/add",
                "https://web.telegram.org/k/",
                "Description1",
                "/finish",
                "/finish");
            platform.Run();
            Console.WriteLine();
            Console.WriteLine(String.Join("\n\t--------\n", platform.ReceivedMessages));
            Singleton<EntrepreneurManager>.Instance.RemoveUserAsEntrepreneurByName("Santiago");
        }
    }
}
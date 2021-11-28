using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using NUnit.Framework;

namespace UnitTests.Utils
{
    /// <summary>
    /// This class contains methods for checking the state
    /// of several types of objects related to the bot.
    /// </summary>
    public static class CheckUtils
    {
        /// <summary>
        /// Checks whether the user with a concrete id and a concrete name exists.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="name">The user's name.</param>
        public static void CheckUser(string id, string name)
        {
            UserSession? idUser = Singleton<SessionManager>.Instance.GetById(id);
            UserSession? nameUser = Singleton<SessionManager>.Instance.GetByName(name);
            Assert.That(idUser, Is.Not.Null);
            Assert.That(nameUser, Is.Not.Null);
            Assert.AreEqual(idUser, nameUser);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="ContactInfo"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckContactInfoEquality(ContactInfo expected, ContactInfo actual)
        {
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="UserData"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckUserDataEquality(UserData expected, UserData actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsComplete, actual.IsComplete);
            Assert.AreEqual(expected.UserType, actual.UserType);
            CheckContactInfoEquality(expected.ContactInfo, actual.ContactInfo);
        }

        /// <summary>
        /// Checks the existence of a <see cref="UserSession" /> with a concrete id,
        /// and then compares it with a <see cref="UserData" />.
        /// </summary>
        /// <param name="expected">The expected user.</param>
        /// <param name="id">The actual user's id.</param>
        public static void CheckUserAndEquality(UserData expected, string id)
        {
            UserSession actual;
            {
                UserSession? user = Singleton<SessionManager>.Instance.GetById(id);
                Assert.That(user, Is.Not.Null);
                actual = user!;
            }

            CheckUserDataEquality(expected, actual.UserData);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="Material"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckMaterialEquality(Material expected, Material actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Measure, actual.Measure);
            Assert.AreEqual(expected.Category, actual.Category);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="Amount"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckAmountEquality(Amount expected, Amount actual)
        {
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Unit, actual.Unit);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="Price"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckPriceEquality(Price expected, Price actual)
        {
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.Unit, actual.Unit);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="MaterialPublication"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckMaterialPublicationEquality(MaterialPublication expected, MaterialPublication actual)
        {
            CheckMaterialEquality(expected.Material, actual.Material);
            CheckAmountEquality(expected.Amount, actual.Amount);
            CheckPriceEquality(expected.Price, actual.Price);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="BoughtMaterialLine"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckBoughtMaterialLineEquality(BoughtMaterialLine expected, BoughtMaterialLine actual)
        {
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.DateTime, actual.DateTime);
            CheckAmountEquality(expected.Amount, actual.Amount);
            CheckMaterialEquality(expected.Material, actual.Material);
            CheckPriceEquality(expected.Price, actual.Price);
        }

        /// <summary>
        /// Asserts in a field-wise way the equality of two instances of <see cref="MaterialSalesLine"/>.
        /// </summary>
        /// <param name="expected">The expected instance.</param>
        /// <param name="actual">The actual instance.</param>
        public static void CheckMaterialSalesLineEquality(MaterialSalesLine expected, MaterialSalesLine actual)
        {
            Assert.AreEqual(expected.DateTime, actual.DateTime);
            Assert.AreEqual(expected.Buyer, actual.Buyer);
            CheckMaterialEquality(expected.Material, actual.Material);
            CheckPriceEquality(expected.Price, actual.Price);
            CheckAmountEquality(expected.Amount, actual.Amount);
        }
    }
}

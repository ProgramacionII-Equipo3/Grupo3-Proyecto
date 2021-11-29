using System;
using NUnit.Framework;
using Library;
using Library.Core.Distribution;
using Library.Core.Processing;
using Library.States;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using ProgramTests.Utils;

namespace ProgramTests
{
    /// <summary>
    /// This class represents unit tests related to the class <see cref="ListProcessor{T}" /> as a subclass of <see cref="InputProcessor{T}" /> of a list of elements.
    /// </summary>
    [TestFixture]
    public class ListProcessorTest
    {
        private static void listProcessorBaseTest<T>(T[]? expected, string initialResponse, InputProcessor<T> processor, params string[] messages)
        {
            Console.WriteLine();
            T[]? list = null;
            BasicUtils.CreateUser(new InputHandlerState(
                ProcessorHandler<T[]>.CreateInfallibleInstance(
                    l => list = l,
                    new ListProcessor<T>(() => initialResponse, processor)),
                () => (null, null),
                () => (null, null)));
            ProgramaticPlatform platform = new ProgramaticPlatform("___", messages);
            platform.Run();

            foreach (string msg in platform.ReceivedMessages)
            {
                Console.WriteLine(msg);
            }

            Assert.AreEqual(expected, list);
        }

        /// <summary>
        /// Tests the basic workings of the list processor: adding elements and submitting them.
        /// </summary>
        [Test]
        public void ListProcessorBasicTest()
        {
            listProcessorBaseTest<int>(
                new int[] { 32, 51 },
                "Insert the numbers.",
                new UnsignedInt32Processor(() => "Insert a number."),
                "/add",
                "32",
                "/add",
                "51",
                "/finish");
        }

        /// <summary>
        /// Tests the functionality of removing elements.
        /// </summary>
        [Test]
        public void ListProcessorRemoveElementsTest()
        {
            listProcessorBaseTest<int>(
                new int[] { 40 },
                "Insert the numbers.",
                new UnsignedInt32Processor(() => "Insert a number."),
                "/add",
                "32",
                "/add",
                "51",
                "/remove",
                "1",
                "/add",
                "40",
                "/remove",
                "2",
                "/remove",
                "0",
                "/finish");
        }

        /// <summary>
        /// Tests the functionality of giving an interrupt signal.
        /// </summary>
        [Test]
        public void ListProcessorGoBackTest()
        {
            listProcessorBaseTest<int>(
                null,
                "Insert the numbers.",
                new UnsignedInt32Processor(() => "Insert a number."),
                "/add",
                "32",
                "/add",
                "51",
                "/esc");
        }
    }
}

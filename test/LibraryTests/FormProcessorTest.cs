using System;
using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.Core.Processing;
using Library.States;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// This class represents unit tests concerning the class <see cref="FormProcessor{T, U}" />.
    /// </summary>
    public class FormProcessorTest
    {
        /// <summary>
        /// Tests the functionality itself of the <see cref="FormProcessor{T, U}" />.
        /// </summary>
        [Test]
        public void FormProcessorBasicTest()
        {
            Console.WriteLine();
            int value = default;
            Singleton<SessionManager>.Instance.NewUser("___", new Library.Core.UserData(), new FormProcessorTestState(v => value = v));
            ProgramaticPlatform platform = new ProgramaticPlatform(
                "___",
                new string[]
                {
                    "Hola",
                    "35",
                    "24"
                }
            );
            platform.Run();
            foreach(string i in platform.ReceivedMessages)
            {
                Console.WriteLine($"\t--------\n{i}");
            }
            Singleton<SessionManager>.Instance.RemoveUser("___");
            Assert.AreEqual(3524, value);
        }

        private class FormProcessorTestState : InputHandlerState
        {
            public override bool IsComplete => false;
            public override State.Type UserType => State.Type.ADMIN;

            public FormProcessorTestState(Action<int> f): base(
                exitState: () => new BasicState(),
                nextState: () => new BasicState(),
                inputHandler: ProcessorHandler.CreateInstance<int>(
                    (result) =>
                    {
                        f(result);
                        Console.WriteLine($"\tIN-CODE: Result: {result}");
                        return null;
                    },
                    new FormProcessor<int, (int, int)>(
                        initialStateGetter: () => default,
                        conversionFunction: state =>
                        {
                            Console.WriteLine($"\tIN-CODE: State: {state}");
                            return Result<int, string>.Ok(joinNumbers(state.Item1, state.Item2));
                        },
                        inputHandlers: new Func<Func<(int, int)>, IInputProcessor<(int, int)>>[]
                        {
                            ProcessorModifier<(int, int)>.CreateInfallibleInstanceGetter<int>(
                                (state, v) => (v, default),
                                new UnsignedInt32Processor(() => "Item1: ")
                            ),
                            ProcessorModifier<(int, int)>.CreateInfallibleInstanceGetter<int>(
                                (state, v) => (state.Item1, v),
                                new UnsignedInt32Processor(() => "Item2: ")
                            )
                        }
                    )
                )
            ) {}
        }

        private static int joinNumbers(int a, int b) =>
            a * (int)System.Math.Pow(10, ((int)System.Math.Log10(b) + 1)) + b;

        private class BasicState : State
        {
            public override bool IsComplete => false;

            public override Type UserType => Type.ADMIN;

            public override string GetDefaultResponse() => "What do you want to do?";

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override (State, string) ProcessMessage(string id, UserData data, string msg) =>
                (this, $"Message sent: {msg}");
        }
    }
}

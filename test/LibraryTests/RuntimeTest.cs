using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library;
using Library.Core;
using Library.Core.Distribution;
using Library.Core.Invitations;
using Library.States.Admins;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;
using Library.HighLevel.Materials;
using Library.Utils;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;
using ProgramTests.Utils;

// #warning TODO: Add test of creating an entrepreneur report before buying materials.
// #warning TODO: Add test of creating an entrepreneur report after buying materials.
// #warning TODO: Add test of removing company.
// #warning TODO: Add test of removing normal user.
// #warning TODO: Add test of removing admin.

namespace ProgramTests
{
    /// <summary>
    /// This class holds a single test which executes a long runtime code into a ConsolePlatform-like platform.
    /// </summary>
    [TestFixture]
    public class RuntimeTest
    {
        private static Regex companyReportRegex = new Regex(
            "(?<amount>\\d+(?:\\.\\d+)? \\w+) de (?<material>[\\w ]+) vendido(s) al emprendedor (?<entrepreneur>[\\w ]+) a un precio de (?<price>.+?) el día (?<date>.+)",
            RegexOptions.Compiled);

        /// <summary>
        /// Performs a generic runtime test.
        /// </summary>
        /// <param name="startFolderName">The name of the folder which holds the memory to deserialize.</param>
        /// <param name="action">The function to run.</param>
        /// <param name="endFolderName">The name of the folder which will hold the serialized memory.</param>
        public static void BasicRuntimeTest(string startFolderName, Action action, string? endFolderName = null)
        {
            endFolderName ??= startFolderName;
            SerializationUtils.DeserializeAllFromJSON($"../../../Memories/-Memory-start/{startFolderName}");
            try
            {
                action();
            }
            finally
            {
                SerializationUtils.SerializeAllIntoJson($"../../../Memories/Memory-end/{endFolderName}");
                SerializationUtils.DeserializeAllFromJSON("../../../Memories/-Memory-void");
            }
        }
    }
}

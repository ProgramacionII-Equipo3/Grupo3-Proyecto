using System.Collections.Generic;
using System.Linq;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class represents a requirement to manipulate certain materials.
    /// </summary>
    public abstract class Requirement
    {
        /// <summary>
        /// Checks if the requirement is satisfied by a concrete <see cref="Habilitation" />.
        /// </summary>
        /// <param name="habilitation">The habilitation.</param>
        /// <returns>Whether the requirement is satisfied by the habilitation.</returns>
        public abstract bool IsSatisfiedBy(Habilitation habilitation);

        /// <summary>
        /// Checks if all the given requirements are satisfied by the given habilitations.
        /// </summary>
        /// <param name="requirements">The requirements to check.</param>
        /// <param name="habilitations">The habilitations to compare with.</param>
        /// <returns>Whether all the given requirements are satisfied by the given habilitations.</returns>
        public static bool FullCheck(IEnumerable<Requirement> requirements, IEnumerable<Habilitation> habilitations) =>
            requirements.All(
                req => habilitations.Any(
                    hab => req.IsSatisfiedBy(hab)
                )
            );
    }
}

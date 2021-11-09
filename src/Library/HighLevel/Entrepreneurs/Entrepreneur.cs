using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;
using Library.Core;


namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents an entrepreneur.
    /// We used the principle Expert to create this
    /// class.
    /// </summary>
    public class Entrepreneur 
    {

        /// <summary>
        /// The entrepreneur's id.
        /// </summary>
        public UserId Id { get; set; }

        /// <summary>
        /// The entrepeneur's name.
        /// </summary>
        public string Name { get; private set; }
       
        /// <summary>
        /// The entrepreneur's age.
        /// </summary>
        public string Age { get; private set; }

        /// <summary>
        /// The entrepreneur's location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// The entrepreneur's heading.
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// The entrepreneur's habilitation needed to buy certain materials.
        /// </summary>
        public  List<Habilitation> Habilitation = new List<Habilitation>();

        /// <summary>
        /// The entrepreneur's specialization.
        /// </summary>
        public List<Specialization> Specialization = new List<Specialization>();

        /// <summary>
        /// The entrepreneur's users in the platform.
        /// </summary>
        public static List<UserId> EntrepeneurList = new List<UserId>();

        /// <summary>
        /// Entrepreneur's Constructor.
        /// </summary>
        public Entrepreneur(UserId id, string name, string age, Location location, string heading, List<Habilitation> habilitations, List<Specialization> specializations)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Location = Location;
            this.Heading = heading;
            this.Habilitation= habilitations;
            this.Specialization = specializations;
        }
    }
}
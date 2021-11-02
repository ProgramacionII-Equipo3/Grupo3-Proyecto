using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using Library.Core;


namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents a entrepreneur
    /// </summary>
    public class Entrepreneur 
    {

        /// <summary>
        /// The Id to identify the entrepreneur
        /// </summary>
        /// <value></value>
        public string Id { get; private set; }
        
        /// <summary>
        /// The entrepeneur's name
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }
       
        /// <summary>
        /// The entrepreneur's age
        /// </summary>
        /// <value></value>
        public byte Age { get; private set; }

        /// <summary>
        /// The entrepreneur's location
        /// </summary>
        /// <value></value>
        public Location Location { get; private set; }

        /// <summary>
        /// The entrepreneur's heading
        /// </summary>
        /// <value></value>
        public string Heading { get; private set; }

        /// <summary>
        /// The entrepreneur's habilitation needed to buy certain materials
        /// </summary>
        private List<Habilitation> entrepreneurHabilitation = new List<Habilitation>();

        /// <summary>
        /// The entrepreneur's specialization
        /// </summary>
        private List<EntrepreneurSpecialization> specialization = new List<EntrepreneurSpecialization>();

        /// <summary>
        /// The entrepreneur's users in the platform
        /// </summary>
        private List<UserId> entrepeneurList = new List<UserId>();

        /// <summary>
        /// Entrepreneur's Constructor
        /// </summary>
        public Entrepreneur(string id, string name, byte age, Location location, string heading)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Location = location;
            this.Heading = heading;
        }
    }
}
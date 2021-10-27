using System.Collections.Generic;
namespace Library.HighLevel.Materials
{
    public class Unit
    {
        public string Name {get; private set ;}
        
        public string Abreviation {get; private set;}

        public Measure Measure { get; private set; }

        public double Weight {get; private set;}

        private List <Unit> values = new List<Unit>();

        private void GetByAbby (string abbr) {}
        
        

    }
}
using System.Collections.Generic;

namespace Heroes.Entity
{
    public class Hero
    {
        public int HeroId { get; set; }
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
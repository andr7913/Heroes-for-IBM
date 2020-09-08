namespace Heroes.Entity
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Race { get; set; }
        public Hero Hero { get; set; }
    }
}
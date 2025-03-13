namespace Runner
{
    public enum Diet
    {
        Carnivore,
        Herbivore,
        Omnivore,
    }

    public class Dinosaur
    {
        public string Name { get; set; } = string.Empty;

        public double Weight { get; set; }

        public Diet Diet { get; set; }

        public int Extinction { get; set; }

        public Dinosaur(string name, double weight, Diet diet, int extinction)
        {
            Name = name;
            Weight = weight;
            Diet = diet;
            Extinction = extinction;
        }
    }
}

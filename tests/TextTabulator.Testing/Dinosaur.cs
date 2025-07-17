namespace TextTabulator.Testing
{
    public interface IDinosaur
    {
        string Name { get; }

        double Weight { get; }

        string Diet { get; }

        int ExtinctionMya { get; }
    }

    public class Dinosaur : IDinosaur
    {
        public string Name { get; }

        public double Weight { get; }

        public string Diet { get; }

        public int ExtinctionMya { get; }

        public Dinosaur(string name, double weight, string diet, int extinctionMya)
        {
            Name = name;
            Weight = weight;
            Diet = diet;
            ExtinctionMya = extinctionMya;
        }
    }
}



namespace yummyCook.Firebase
{
    public class Diets
    {
        public string Diet { get; set; }
        public bool Have { get; set; }
        public int Index { get; set; }
        public bool InNewRecipe { get; set; }

    }

    public class Alergies
    {
        public string Alergy { get; set; }
        public bool Have { get; set; }
        public int Index { get; set; }
    }

    public class Tools
    {
        public string Tool { get; set; }
        public bool Have { get; set; }
        public int Index { get; set; }
        public bool InNewRecipe { get; set; }

    }
    public class ProfilModel
    {
        public string ProfilName { get; set; }
        public string ProfilImage { get; set; }
        public Diets[] Diets { get; set; }
        public Alergies[] Alergy { get; set; }
        public Tools[] Tools { get; set; }
        public string Language { get; set; }
    }
}

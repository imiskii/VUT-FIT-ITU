using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{

    public class Ingredients
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Index { get; set; }
    }
    public class Steps
    {
        public string Step { get; set; }
        public int Index { get; set; }
    }
    public class RecipeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public int Time { get; set; }
        public string Type { get; set; }
        public string Kitchen { get; set; }
        public bool Favourite { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public List<Steps> Steps { get; set; }
        public List<Tools> Tools { get; set; }
        public List<Diets> Diets { get; set; }
        public List<Alergies> Allergies { get; set; }
        public string Photo { get; set; }
        public ImageSource PhotoSource { get; set; }
    }
}

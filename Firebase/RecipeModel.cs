using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{
    public class RecipeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public int Time { get; set; }
        public string Type { get; set; }
        public string Kitchen { get; set; }
        public bool Favourite { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string Appliances { get; set; }
        public string Diets { get; set; }
        public string Allergies { get; set; }
        public string Photos { get; set; }
    }
}

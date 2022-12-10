using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{
    public class IngredientModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public float Fat { get; set; }
        public float Sugar { get; set; }
        public float Proteins { get; set; }
        public float Calories { get; set; }
        public bool Have { get; set; }
        public bool Buy { get; set; }
        public bool InCart { get; set; }
        public string ToBuy { get; set; }
        public bool InNewRecipe { get; set; }
    }
}

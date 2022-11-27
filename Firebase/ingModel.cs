using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{
    public class ingModel
    {
        public Dictionary<string, IngredientModel> Ingredients { get; set;}
        public string key { get; set;}
    }
}

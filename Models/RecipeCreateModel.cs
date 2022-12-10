/* RecipeCreateModel.cs */
/* Autor: Michal Ľaš */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.Models
{
    internal class RecipeCreateModel
    {
        public List<Alergies> FigureOutAlregies(Alergies[] listOfAlergies, List<Ingredients> ingredients)
        {
            List<Alergies> alergies = new List<Alergies>();

            foreach (var ing in ingredients)
            {

            }

            return alergies;
        }
    }
}

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
        public List<Alergies> FigureOutAlregies(Alergies[] Alergies, List<Ingredients> ingredients)
        {
            /* Make it List */
            List<Alergies> listOfAlergies = new List<Alergies>();
            foreach (var alergy in Alergies)
            {
                listOfAlergies.Add(alergy);
            }

            List<Alergies> alergies = new List<Alergies>();

            foreach (var ing in ingredients)
            {
                switch (ing.Category)
                {
                    case "pastry":
                        if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Lepok").FirstOrDefault()))
                        {
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Lepok").FirstOrDefault());
                        }
                        break;
                    case "dairyproducts":
                        if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Mléčné produkty").FirstOrDefault())){
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Mléčné produkty").FirstOrDefault());
                        }
                        break;
                    case "meat":
                        if (ing.Name == "Vejce")
                        {
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Vajíčka").FirstOrDefault());
                        }
                        break;
                    case "fish":
                        if (ing.Name != "Krevety" && ing.Name != "Hřebenatka") // Mäkkýše, Morské plody
                        {
                            if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Ryby").FirstOrDefault()))
                            {
                                alergies.Add(listOfAlergies.Where(x => x.Alergy == "Ryby").FirstOrDefault());
                            }
                        }
                        else
                        {
                            if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Mäkkýše").FirstOrDefault()) && (ing.Name == "Hřebenatka")) // Mäkkýše
                            {
                                alergies.Add(listOfAlergies.Where(x => x.Alergy == "Mäkkýše").FirstOrDefault());
                            }
                            else if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Morské plody").FirstOrDefault()) && (ing.Name == "Krevety")) // Morské plody
                            {
                                alergies.Add(listOfAlergies.Where(x => x.Alergy == "Morské plody").FirstOrDefault());
                            }
                        }
                        break;
                    case "nuts":
                        if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Ořechy").FirstOrDefault()))
                        {
                            if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Arašídy").FirstOrDefault()) && ing.Name == "Arašídy")
                            {
                                alergies.Add(listOfAlergies.Where(x => x.Alergy == "Arašídy").FirstOrDefault());
                            }
                            else
                            {
                                alergies.Add(listOfAlergies.Where(x => x.Alergy == "Ořechy").FirstOrDefault());
                            }
                        }
                        break;
                    case "vegetables":
                        if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Celer").FirstOrDefault()) && ing.Name == "Celer") 
                        {
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Celer").FirstOrDefault());
                        }
                        break;
                    case "sauces":
                        if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Hořčice").FirstOrDefault()) && ing.Name == "Hořčice")
                        {
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Hořčice").FirstOrDefault());
                        }
                        else if (!alergies.Contains(listOfAlergies.Where(x => x.Alergy == "Sója").FirstOrDefault()) && ing.Name == "Sójová omáčka")
                        {
                            alergies.Add(listOfAlergies.Where(x => x.Alergy == "Sója").FirstOrDefault());
                        }
                        break;
                    default:
                        break;
                }
            }

            return alergies;
        }
    }
}

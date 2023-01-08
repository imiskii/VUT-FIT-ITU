using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using yummyCook.ViewModels;

namespace yummyCook.Firebase
{

    public class Ingredients : BaseClass
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Index { get; set; }
        public int Weight { get; set; }
        public string Unit { get; set; }
        public ICommand AddToShoppingListCommand => new Command<string>(AddToShoppingList);

        /// <summary>
        /// Pridanie ingrediencie do nákupného košíku z detailu receptu
        /// </summary>
        /// <param name="ingredientName"></param>
        private void AddToShoppingList(string ingredientName)
        {
            try
            {
                if (!ShoppingListData.Any(x => x.Name.Equals(ingredientName)))
                {
                    var ingredient = JoinedIngredients.Where(x => x.Name.Equals(ingredientName)).FirstOrDefault();
                    ShoppingListData.Add(ingredient);
                }
            }catch(Exception) 
            {
                return;
            }
        }
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
        public bool Public { get; set; }
    }
}

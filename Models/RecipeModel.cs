using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Models.Enums;

namespace yummyCook.Models
{
    public class RecipeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RecipeProcedure { get; set; }
        public string Rating { get; set; }
        public TimeSpan CookingTime { get; set; }
        public FoodType FoodType { get; set; }
        public CuisineType CuisineType { get; set; }
        public ObservableCollection<string> KitchenAppliances { get; set; }
        public ObservableCollection<Diet> Diets { get; set; }
        public ObservableCollection<Allergen> Allergens { get; set; }
        public ObservableCollection<string> SelectedIngredients { get; set; }
        public ObservableCollection<string> Pictures { get; set; }
        public bool Favorite { get; set; }

    }
}

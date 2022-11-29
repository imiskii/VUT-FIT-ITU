using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ObservableCollection<RecipeModel> Recipes { get; } = new();

        public Command GetRecipesCommand { get; }
        public RecipeViewModel(FirebaseHelper firebaseHelper)
        {
            GetRecipesCommand = new Command(async () => await GetRecipesAsync());
            GetRecipesCommand.Execute(this);
        }

        async Task GetRecipesAsync()
        {
            int counter = 0;
            var recipes = await firebaseHelper.GetRecipes();

            if (Recipes.Count != 0)
            {
                Recipes.Clear();
            }

            foreach (var recipe in recipes.OrderByDescending(o => o.Rating))
            {
                if (counter < 4)
                {
                    Recipes.Add(recipe);
                    counter++;
                }
            }
        }     
    }
}

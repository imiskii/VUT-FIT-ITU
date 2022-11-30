/**
 * RecipeViewModel.cs
 * Autor: Ondřej Janečka (xjanec33)
 *
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeViewModel : BaseClass
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ObservableCollection<RecipeModel> Recipes { get; } = new();

        public ICommand ShowShoppingList {  get; set; }

        public Command GetRecipesCommand { get; }
        public RecipeViewModel()
        {
            GetRecipesCommand = new Command(async () => await GetRecipesAsync());
            GetRecipesCommand.Execute(this);
            
            ShowShoppingList = new Command(async () => await ShowShoppingListAsync());
        }

        bool topDone = false;

        async Task GetRecipesAsync()
        {
            IsBusy = true;

            var recipes = await firebaseHelper.GetRecipes();

            if (Recipes.Count != 0)
            {
                Recipes.Clear();
            }

            if (!topDone)
                await GetTopRecipes(recipes);

            IsBusy = false;
        }

        async Task GetTopRecipes(ObservableCollection<RecipeModel> recipes)
        {
            int counter = 0;

            foreach (var recipe in recipes.OrderByDescending(o => o.Rating))
            {
                if (counter < 4)
                {
                    Recipes.Add(recipe);
                    counter++;
                }
            }

            topDone = true;
            IsBusy = false;
        }

        async Task ShowShoppingListAsync()
        {
            await Shell.Current.GoToAsync("shoppingList");
        }
    }
}

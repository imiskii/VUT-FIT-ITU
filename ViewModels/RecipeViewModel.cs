/**
 * RecipeViewModel.cs
 * Autor: Ondřej Janečka (xjanec33)
 *
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using yummyCook.Firebase;
using yummyCook.Views.Main;

namespace yummyCook.ViewModels
{
    public partial class RecipeViewModel : BaseClass
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ObservableCollection<RecipeModel> Recipes { get; set; } = new();

        public ICommand ShowShoppingList => new Command(async () => await ShowShoppingListAsync());
        public ICommand GoToDetailCommand => new Command<RecipeModel>(GoToDetailAsync);
        public ICommand FavoriteRecipesCommand => new Command(GetFavoriteRecipes);


        public Command GetRecipesCommand { get; }
        public RecipeViewModel()
        {
            GetGreeting();
            GetProfilData = new Command(async () => await GetLocalProfileAsync());
            GetProfilData.Execute(this);
            GetKitchenCommand = new Command(async () => await GetKitchenData());
            GetKitchenCommand.Execute(this);
            GetFoodTypeCommand = new Command(async () => await GetFoodTypes());
            GetFoodTypeCommand.Execute(this);
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());
            GetIngredientCommand.Execute(this);

            /// Načtení uloženého tématu aplikace
            switch (Preferences.Default.Get("AppTheme", 0))
            {
                case 0: 
                    Application.Current!.UserAppTheme = AppTheme.Unspecified; break;

                case 1:
                    Application.Current!.UserAppTheme = AppTheme.Light; break;

                case 2:
                    Application.Current!.UserAppTheme = AppTheme.Dark; break;
            }

            int a = Preferences.Default.Get("ShoppingListCount", 0);

            if (shoppingListCount == 0 && Preferences.Default.Get("ShoppingListCount", 0) == 0)
            {
                IsEmpty = true;
            }
            else
            {
                shoppingListCount = Preferences.Default.Get("ShoppingListCount", 0);
                IsEmpty = false;
            }

            GetRecipesCommand = new Command(async () => await GetRecipesAsync());
            GetRecipesCommand.Execute(this);
        }

        bool topDone = false;

        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                if (searchText.Length == 0 || searchText == " ")
                {
                    topDone = false;
                    Task.Run(async () => { await GetRecipesAsync(); }).Wait();
                }
                else if(searchText.Length > 4)
                {
                    Task.Run(async () => { await GetRecipeBySearchAsync(searchText); }).Wait();
                }
            }
        }


        async Task GetRecipesAsync()
        {
            IsBusy = true;

            var recipes = await firebaseHelper.GetRecipes();

            if (Recipes.Count != 0)
            {
                Recipes.Clear();
            }

            if (!topDone)
                GetTopRecipes(recipes);

            IsBusy = false;
        }

        /// <summary>
        /// Funkce pro získání čtyř nejlépe hjodnocených receptů
        /// </summary>
        /// <param name="recipes"></param>
        void GetTopRecipes(ObservableCollection<RecipeModel> recipes)
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

        /// <summary>
        /// Funkce generuje pozdrav na hlavní stránce
        /// </summary>
        void GetGreeting()
        {
            var hour = DateTime.Now.Hour;

            if (hour >= 4 && hour < 10)
            {
                Greeting = "Dobré ráno 🥣 🧇";
            }
            else if (hour >= 10 && hour < 12)
            {
                Greeting = "Dobré dopoledne 🥪";
            }
            else if (hour == 12)
            {
                Greeting = "Dobré poledne 🍗 🍔";
            }
            else if (hour > 12 && hour < 19)
            {
                Greeting = "Dobré odpoledne 🍕 🥗";
            }
            else
            {
                Greeting = "Dobrý večer 🌙 🌯";
            }
        }

       
      
        async Task GetRecipeBySearchAsync(string query)
        {
            IsBusy = true;

            if (query != null)
            {
                ObservableCollection<RecipeModel> recipes = new();
                

                foreach (var recipe in Recipes)
                {
                    recipes.Add(recipe);
                }

                Recipes.Clear();

                foreach (var recipe in recipes)
                {
                    if (recipe.Name.ToLower().Contains(query.ToLower()))
                        Recipes.Add(recipe);
                }
            }
            else
            {
                 await GetRecipesAsync();
            }

            IsBusy = false;
        }

        private void GetFavoriteRecipes()
        {
            IsBusy = true;

            ObservableCollection<RecipeModel> recipes = new();

            foreach (var recipe in Recipes)
            {
                recipes.Add(recipe);
            }

            Recipes.Clear();

            foreach(var recipe in recipes)
            {
                if (recipe.Favourite)
                    Recipes.Add(recipe);
            }

            IsBusy = false;
        }

        async Task ShowShoppingListAsync()
        {
            await Shell.Current.GoToAsync("shoppingList");
        }

        async void GoToDetailAsync(RecipeModel recipe)
        {
            DetailRecipe = recipe;

            await Shell.Current.GoToAsync("recipeDetail");
        }

    }
}

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
using yummyCook.Views.Main;

namespace yummyCook.ViewModels
{
    public partial class RecipeViewModel : BaseClass
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseStorageHelper firestorageHelper = new FirebaseStorageHelper();

        public ObservableCollection<RecipeModel> Recipes { get; } = new();

        public ICommand ShowShoppingList => new Command(async () => await ShowShoppingListAsync());
        public ICommand GoToDetailCommand => new Command<RecipeModel>(GoToDetailAsync);
        public ICommand UploadCommand => new Command(UploadAsync);

        public Command GetRecipesCommand { get; }
        public RecipeViewModel()
        {
            GetProfilData = new Command(async () => await GetLocalProfileAsync());
            GetProfilData.Execute(this);
            GetKitchenCommand = new Command(async () => await GetKitchenData());
            GetKitchenCommand.Execute(this);
            GetFoodTypeCommand = new Command(async () => await GetFoodTypes());
            GetFoodTypeCommand.Execute(this);
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());
            GetIngredientCommand.Execute(this);
            PreparationTimeInit();

            switch (Preferences.Default.Get("AppTheme", 0))
            {
                case 0: 
                    Application.Current.UserAppTheme = AppTheme.Unspecified; break;

                case 1:
                    Application.Current.UserAppTheme = AppTheme.Light; break;

                case 2:
                    Application.Current.UserAppTheme = AppTheme.Dark; break;
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

        async void GoToDetailAsync(RecipeModel recipe)
        {
            DetailRecipe = recipe;

            await Shell.Current.GoToAsync("recipeDetail");
        }

        async void UploadAsync()
        {
            var fileResult = await FilePicker.PickAsync();
            if (fileResult != null)
            {
                Stream fileToUpload = await fileResult.OpenReadAsync();

                var url = await firestorageHelper.UploadFile(fileToUpload, fileResult.FileName);
            }
        }
    }
}

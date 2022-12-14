using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeNutritionViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Task<ObservableCollection<IngredientModel>> Ingredients { get; set; } 

        public ICommand NavigateBackCommand => new Command(NavigateToRecipesAsync);
        public ICommand GoToRecipeGuideCommand => new Command<RecipeModel>(GoToGuideAsync);
        public ICommand GoToRecipeDetailCommand => new Command<RecipeModel>(GoToRecipeDetailAsync);

        public RecipeNutritionViewModel()
        {
            recipeModel = DetailRecipe;
            Ingredients = GetIngredientsAsync(recipeModel.Ingredients);
        }

        public async void NavigateToRecipesAsync()
        {
            await Shell.Current.GoToAsync("recipesList");
        }

        public async void GoToRecipeDetailAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("recipeDetail");
        }

        public async void GoToGuideAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("guide");
        }

        private async Task<ObservableCollection<IngredientModel>> GetIngredientsAsync(List<Ingredients> ingredients)
        {
            ObservableCollection<IngredientModel> result = new();

            // Ziskanie ingrediencii podla kategorii
            foreach(var ingredient in ingredients)
            {
               foreach(var item in (await firebaseHelper.GetIngredients(ingredient.Category)))
                {
                    if (item.Name == ingredient.Name) result.Add(item);
                }
            }
            
            return result;
        }

    }
}

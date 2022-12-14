/* RecipeNutritionViewModel.cs */
/* Autor: Peter Čellár */
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

        // Commandy na presmerovanie
        public ICommand NavigateBackCommand => new Command(NavigateToRecipesAsync);
        public ICommand GoToRecipeGuideCommand => new Command<RecipeModel>(GoToGuideAsync);
        public ICommand GoToRecipeDetailCommand => new Command<RecipeModel>(GoToRecipeDetailAsync);

        public RecipeNutritionViewModel()
        {
            recipeModel = DetailRecipe;
            Ingredients = GetIngredientsAsync(recipeModel.Ingredients);
        }

        /// <summary>
        /// Presmerovanie na zoznam receptov
        /// </summary>
        public async void NavigateToRecipesAsync()
        {
            await Shell.Current.GoToAsync("recipesList");
        }

        /// <summary>
        /// Presmerovanie na stránku so surovinami a postupom
        /// </summary>
        /// <param name="recipeModel">Model receptu</param>
        public async void GoToRecipeDetailAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("recipeDetail");
        }

        /// <summary>
        /// Presmerovanie na stránku postupu receptu
        /// </summary>
        /// <param name="recipeModel"></param>
        public async void GoToGuideAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("guide");
        }

        /// <summary>
        /// Získanie ingrediencíí podľa kategórie a ich vyfiltrovanie
        /// Prevod na kolekciu
        /// </summary>
        /// <param name="ingredients"></param>
        /// <returns></returns>
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

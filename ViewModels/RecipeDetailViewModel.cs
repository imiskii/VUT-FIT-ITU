/* RecipeDetailViewModel.cs */
/* Autor: Peter Čellár */
using System.Collections.ObjectModel;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeDetailViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ObservableCollection<Ingredients> Ingredients { get; } = new();
        public ObservableCollection<Steps> Steps { get; } = new();

        // Commandy na presmerovanie
        public ICommand NavigateBackCommand => new Command(NavigateBackFromDetail);
        public ICommand ShowRecipeGuideCommand => new Command(ShowRecipeGuideDetail);
        public ICommand ShowRecipeNutritionsCommand => new Command(ShowRecipeNutritionsDetail);
        public ICommand ShowRecipeDescriptionCommand => new Command(ShowRecipeDescriptionDetail);


        public RecipeDetailViewModel() 
        {
            recipeModel = DetailRecipe;
            ShowRecipeNutritions = false;
            ShowRecipeGuide = false;
            ShowRecipeDescription = true;
            Ingredients = LoadListFromRecipe(DetailRecipe.Ingredients);
            Steps = LoadListFromRecipe(DetailRecipe.Steps);
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
            foreach (var ingredient in ingredients)
            {
                foreach (var item in (await firebaseHelper.GetIngredients(ingredient.Category)))
                {
                    if (item.Name == ingredient.Name) result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Generická metóda na prevedenie listu na kolekciu
        /// </summary>
        /// <typeparam name="T">Model itemu</typeparam>
        /// <param name="itemsList">Zoznam itemov</param>
        /// <returns></returns>
        ObservableCollection<T> LoadListFromRecipe<T>(List<T> itemsList)
        {
            ObservableCollection<T> itemsCollection = new();

            foreach (T step in itemsList)
            {
                itemsCollection.Add(step);
            }

            return itemsCollection;
        }

        #region Navigation Commands
        /// <summary>
        /// Nastavenie viditeľnosti na postup receptu
        /// </summary>
        private void ShowRecipeGuideDetail()
        {
            ShowRecipeDescription = false;
            ShowRecipeNutritions = false;
            ShowRecipeGuide = true;
        }

        /// <summary>
        /// Nastavenie viditeľnosti na nutričné hodnoty receptu
        /// </summary>
        private void ShowRecipeNutritionsDetail()
        {
            ShowRecipeDescription = false;
            ShowRecipeGuide = false;
            ShowRecipeNutritions = true;
        }

        /// <summary>
        /// Nastavenie viditeľnosti na popis receptu
        /// </summary>
        private void ShowRecipeDescriptionDetail()
        {
            ShowRecipeGuide = false;
            ShowRecipeNutritions = false;
            ShowRecipeDescription = true;
        }

        /// <summary>
        /// Vrátenie sa na zoznam receptov
        /// </summary>
        public async void NavigateBackFromDetail()
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}

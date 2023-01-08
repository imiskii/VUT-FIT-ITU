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
        public string Calories { get; set; }
        public string Fat { get; set; }
        public string Proteins { get; set; }
        public string Sugar { get; set; }

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
        /// Generická metóda na prevedenie listu na kolekciu
        /// </summary>
        /// <typeparam name="T">Model itemu</typeparam>
        /// <param name="itemsList">Zoznam itemov</param>
        /// <returns></returns>
        ObservableCollection<T> LoadListFromRecipe<T>(List<T> itemsList)
        {
            ObservableCollection<T> itemsCollection = new();

            try
            {
                foreach (T step in itemsList)
                {
                    itemsCollection.Add(step);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return itemsCollection;
        }

        /// <summary>
        /// Získanie ingrediencií podľa unikátnych kategórii ingrediencií z modelu
        /// </summary>
        /// <param name="ingredients">Kolekcia ingrediencií z modelu</param>
        /// <returns></returns>
        private void SetRecipeNutritions(ObservableCollection<Ingredients> recipeIngredients)
        {
            float calories = 0;
            float fat = 0;
            float proteins = 0;
            float sugar = 0;
            try
            {
                foreach(var recipeIngredient in recipeIngredients)
                {
                    foreach(var ingredient in ShoppingListData)
                    {
                        if (ingredient.Name.Equals(recipeIngredient.Name))
                        {
                            calories += ingredient.Calories;
                            fat += ingredient.Fat;
                            proteins += ingredient.Proteins;
                            sugar += ingredient.Sugar;
                        }
                    }
                }
                Calories = calories.ToString() + " kcal";
                Fat = fat.ToString() + " g";
                Proteins = proteins.ToString() + " g";
                Sugar = sugar.ToString() + " g";
            }
            catch(Exception)
            {
                Calories = "0 kcal";
                Fat = "0 g";
                Proteins = "0 g";
                Sugar = "0 g";
            }
        }

        #region Command Methods
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
            SetRecipeNutritions(Ingredients);
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
        #endregion

        #region Navigation Commands
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

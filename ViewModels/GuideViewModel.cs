/* GuideViewModel.cs */
/* Autor: Peter Čellár */
using System.Collections.ObjectModel;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class GuideViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        public ObservableCollection<Steps> Steps { get; } = new();

        // Commandy na presmerovanie
        public ICommand GoToRecipeDetailCommand => new Command<RecipeModel>(GoToDetailAsync);
        public ICommand GoToRecipesCommand => new Command(GoToRecipesAsync);
        public ICommand GoToRecipeNutritionCommand => new Command<RecipeModel>(GoToNutritionAsync);

        public GuideViewModel()
        {
            recipeModel = DetailRecipe;
            Steps = LoadStepsFromRecipe(DetailRecipe.Steps);
        }

        /// <summary>
        /// Presmerovanie na stránku so surovinami a popisom receptu
        /// </summary>
        /// <param name="recipeModel"></param>
        public async void GoToDetailAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("recipeDetail");
        }

        /// <summary>
        /// Presmerovanie na stránku s nutričnými hodnotami
        /// </summary>
        /// <param name="recipeModel"></param>
        public async void GoToNutritionAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("/recipeNutrition");
        }

        /// <summary>
        /// Presmerovanie na zoznam receptov
        /// </summary>
        public async void GoToRecipesAsync()
        {
            await Shell.Current.GoToAsync("recipesList");
        }


        /// <summary>
        /// Načíta zoznam do observable collection
        /// </summary>
        /// <param name="StepsList"></param>
        /// <returns></returns>
        ObservableCollection<Steps> LoadStepsFromRecipe(List<Steps> StepsList)
        {
            ObservableCollection<Steps> Steps = new();

            foreach (var step in StepsList)
            {
                Steps.Add(step);
            }

            return Steps;
        }

        /// <summary>
        /// Naformátujú sa kroky receptu
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        ObservableCollection<Steps> FormatSteps(ObservableCollection<Steps> steps)
        {
            string stepFormat = "Krok ";
            string colon = ": \n";
            foreach (var step in steps)
            {
                step.Step = stepFormat + step.Index.ToString() + colon + step.Step;
            }

            return steps;
        }

    }
}

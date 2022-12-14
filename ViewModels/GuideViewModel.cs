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
    public partial class GuideViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        public ObservableCollection<Steps> Steps { get; } = new();

        public ICommand GoToRecipeDetailCommand => new Command<RecipeModel>(GoToDetailAsync);
        public ICommand GoToRecipesCommand => new Command(GoToRecipesAsync);
        public ICommand GoToRecipeNutritionCommand => new Command<RecipeModel>(GoToNutritionAsync);

        public GuideViewModel()
        {
            recipeModel = DetailRecipe;
            Steps = LoadStepsFromRecipe(DetailRecipe.Steps);
        }

        public async void GoToDetailAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("recipeDetail");
        }

        public async void GoToNutritionAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("/recipeNutrition");
        }

        public async void GoToRecipesAsync()
        {
            await Shell.Current.GoToAsync("recipesList");
        }



        ObservableCollection<Steps> LoadStepsFromRecipe(List<Steps> StepsList)
        {
            ObservableCollection<Steps> Steps = new();

            foreach (var step in StepsList)
            {
                Steps.Add(step);
            }

            return Steps;
        }

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

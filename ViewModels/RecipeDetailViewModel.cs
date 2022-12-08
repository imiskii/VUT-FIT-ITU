using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeDetailViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        public ObservableCollection<Steps> Steps { get; } = new();
        public ObservableCollection<Ingredients> Ingredients { get; } = new();

        public Command LoadRecipeDetailCommand { get; }
        public RecipeDetailViewModel() 
        {
            //LoadRecipeDetailCommand = new Command(async () => await LoadRecipeDetailAsync());
            //LoadRecipeDetailCommand.Execute(this);

            recipeModel = DetailRecipe;
            Steps = LoadListFromRecipe<Steps>(DetailRecipe.Steps);
            Steps = FormatSteps(Steps);
            Ingredients = LoadListFromRecipe<Ingredients>(DetailRecipe.Ingredients);
            
        }

        ObservableCollection<Steps> FormatSteps(ObservableCollection<Steps> steps)
        {
            string stepFormat = "Krok ";
            string colon = ": \n";
            foreach(var step in steps)
            {
                step.Step = stepFormat + step.Index.ToString() + colon + step.Step;
            }

            return steps;
        }

        ObservableCollection<T> LoadListFromRecipe<T>(List<T> itemsList)
        {
            ObservableCollection<T> itemsCollection = new();
            
            foreach(T step in itemsList)
            {
                itemsCollection.Add(step);
            }

            return itemsCollection;
        }
        
        async void GoToGuideAsync(RecipeModel recipe)
        {
            DetailRecipe = recipe;
            await Shell.Current.GoToAsync("guide");
        }
    }
}

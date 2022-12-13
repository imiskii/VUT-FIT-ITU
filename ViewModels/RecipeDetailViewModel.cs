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
    public partial class RecipeDetailViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        public ObservableCollection<Steps> Steps { get; } = new();
        public ObservableCollection<Ingredients> Ingredients { get; } = new();

        public ICommand NavigateBackCommand => new Command(NavigateBackFromDetail);
        public ICommand GoToRecipeGuideCommand => new Command<RecipeModel>(GoToGuideAsync);
        
        public RecipeDetailViewModel() 
        {
            recipeModel = DetailRecipe;
            Steps = LoadListFromRecipe(DetailRecipe.Steps);
            Steps = FormatSteps(Steps);
            Ingredients = LoadListFromRecipe(DetailRecipe.Ingredients);
            
        }

        public async void NavigateBackFromDetail()
        {
            await Shell.Current.GoToAsync("..");
        }

        async void GoToGuideAsync(RecipeModel recipe)
        {
            DetailRecipe = recipe;
            await Shell.Current.GoToAsync("guide");
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
    }
}

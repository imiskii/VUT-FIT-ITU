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

        public Command LoadRecipeDetailCommand { get; }
        public RecipeDetailViewModel() 
        {
            //LoadRecipeDetailCommand = new Command(async () => await LoadRecipeDetailAsync());
            //LoadRecipeDetailCommand.Execute(this);

            recipeModel = DetailRecipe;
            Steps = LoadStepsFromRecipe(DetailRecipe.Steps);
        }

        ObservableCollection<Steps> LoadStepsFromRecipe(List<Steps> StepsList)
        {
            ObservableCollection<Steps> Steps = new();
            
            foreach(var step in StepsList)
            {
                Steps.Add(step);
            }

            return Steps;
        }
        
        async Task LoadRecipeDetailAsync()
        {
            IsBusy = true;
            IsEmpty = true;

            

            IsBusy = false;
            IsEmpty = false;
        }
    }
}

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

        public Command LoadRecipeDetailCommand { get; }
        public RecipeDetailViewModel() 
        {
            //LoadRecipeDetailCommand = new Command(async () => await LoadRecipeDetailAsync());
            //LoadRecipeDetailCommand.Execute(this);

            recipeModel = DetailRecipe;
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

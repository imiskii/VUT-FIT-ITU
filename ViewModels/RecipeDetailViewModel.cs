using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class RecipeDetailViewModel : BaseClass
    {
        public RecipeModel recipeModel { get; } = new();
        public ObservableCollection<Ingredients> Ingredients { get; } = new();

        public ICommand NavigateBackCommand => new Command(NavigateBackFromDetail);
        public ICommand GoToRecipeGuideCommand => new Command<RecipeModel>(GoToGuideAsync);
        public ICommand GoToRecipeNutritionCommand => new Command<RecipeModel>(GoToNutritionAsync);
        
        public RecipeDetailViewModel() 
        {
            recipeModel = DetailRecipe;
            Ingredients = LoadListFromRecipe(DetailRecipe.Ingredients);
            
        }

        public async void NavigateBackFromDetail()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void GoToGuideAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("guide");
        }

        public async void GoToNutritionAsync(RecipeModel recipeModel)
        {
            DetailRecipe = recipeModel;
            await Shell.Current.GoToAsync("recipeNutrition");
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

using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://yummycook-itu-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<ObservableCollection<RecipeModel>> GetRecipes()
        {
            return (new ObservableCollection<RecipeModel>(await GetRecipesList()));
        }
        public async Task<ObservableCollection<IngredientModel>> GetIngredients(string category)
        {
            return (new ObservableCollection<IngredientModel>(await GetIngredientsList(category)));
        }

        public async Task<List<RecipeModel>> GetRecipesList()
        {
            return (await firebase
              .Child("Recipe")
              .OnceAsync<RecipeModel>()).Select(item => new RecipeModel
              {
                  Name = item.Object.Name,
                  Description = item.Object.Description,
                  Rating = item.Object.Rating,
                  Time = item.Object.Time,
                  Type = item.Object.Type,
                  Kitchen = item.Object.Kitchen,
                  Favourite = item.Object.Favourite,
                  Ingredients = item.Object.Ingredients,
                  Steps = item.Object.Steps,
                  Appliances = item.Object.Appliances,
                  Diets = item.Object.Diets,
                  Allergies = item.Object.Allergies,
                  Photos = item.Object.Photos

              }).ToList();
        }
        public async Task<List<IngredientModel>> GetIngredientsList(string category)
        {
            category = category.ToLower();
            return (await firebase
              .Child("Ingredients")
              .Child(category)
              .OnceAsync<IngredientModel>()).Select(item => new IngredientModel
              {
                  Name = item.Object.Name,
                  Category = item.Object.Category,
                  Fat = item.Object.Fat,
                  Sugar = item.Object.Sugar,
                  Proteins = item.Object.Proteins,
                  Calories = item.Object.Calories,
                  Have = item.Object.Have,
                  Buy = item.Object.Buy    

              }).ToList();
        }
    }
}

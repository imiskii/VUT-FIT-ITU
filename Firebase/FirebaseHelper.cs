using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.Firebase
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://yummycook-itu-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<List<RecipeModel>> GetRecipes()
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
    }
}

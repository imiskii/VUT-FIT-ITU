using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

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

        public async Task<ObservableCollection<ProfilModel>> GetProfil()
        {
            return (new ObservableCollection<ProfilModel>(await GetProfilList()));
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

        /// <summary>
        /// Fukce vyhledá objekt v databázi podle name a category, poté na zakládě property aktualizuje, buďto Have nebo Buy na hodnotu value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task UpdateIngredience(string property, string category, string name, bool value)
        {
            var ingItem = (await firebase
                .Child("Ingredients")
                .Child(category)
                .OnceAsync<IngredientModel>())
                .Where(x => x.Object.Name == name).FirstOrDefault();

            if (property == "have")
            {
                await firebase.Child("Ingredients")
                    .Child(category)
                    .Child(ingItem.Key)
                    .PutAsync(new IngredientModel { 
                        Name = ingItem.Object.Name, 
                        Category = ingItem.Object.Category, 
                        Fat = ingItem.Object.Fat, 
                        Sugar = ingItem.Object.Sugar,
                        Proteins = ingItem.Object.Proteins, 
                        Calories = ingItem.Object.Calories, 
                        Have = value, 
                        Buy = ingItem.Object.Buy });
            }
            else if (property == "buy")
            {
                await firebase.Child("Ingredients")
                    .Child(category)
                    .Child(ingItem.Key)
                    .PutAsync(new IngredientModel { 
                        Name = ingItem.Object.Name, 
                        Category = ingItem.Object.Category, 
                        Fat = ingItem.Object.Fat, 
                        Sugar = ingItem.Object.Sugar, 
                        Proteins = ingItem.Object.Proteins, 
                        Calories = ingItem.Object.Calories, 
                        Have = ingItem.Object.Have, 
                        Buy = value });
            }
        }

        public async Task<List<ProfilModel>> GetProfilList()
        {
            return (await firebase
              .Child("Profil")
              .OnceAsync<ProfilModel>()).Select(item => new ProfilModel
              {
                  ProfilName = item.Object.ProfilName,
                  ProfilImage = item.Object.ProfilImage,
                  Diets = item.Object.Diets,
                  Alergy = item.Object.Alergy,
                  Language = item.Object.Language,

              }).ToList();
        }
    }
}

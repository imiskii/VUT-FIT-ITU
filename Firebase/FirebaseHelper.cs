using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace yummyCook.Firebase
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://yummycook-itu-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<ObservableCollection<RecipeModel>> GetRecipes()
        {
            return (new ObservableCollection<RecipeModel>(await GetRecipesList()));
        }
        public async Task<ObservableCollection<RecipeModel>> GetPrivateRecipes()
        {
            return (new ObservableCollection<RecipeModel>(await GetPrivateRecipesList()));
        }
        public async Task<ObservableCollection<IngredientModel>> GetIngredients(string category)
        {
            return (new ObservableCollection<IngredientModel>(await GetIngredientsList(category)));
        }

        public async Task<ObservableCollection<ProfilModel>> GetProfil()
        {
            return (new ObservableCollection<ProfilModel>(await GetProfilList()));
        }
        public async Task<ObservableCollection<KitchenModel>> GetKitchen()
        {
            return (new ObservableCollection<KitchenModel>(await GetKitchenTypes()));
        }
        public async Task<ObservableCollection<FoodTypeModel>> GetFoodTypes()
        {
            return (new ObservableCollection<FoodTypeModel>(await GetFoodTypeList()));
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
                  Tools = item.Object.Tools,
                  Diets = item.Object.Diets,
                  Allergies = item.Object.Allergies,
                  Photo = item.Object.Photo

              }).ToList();
        }

        public async Task<List<RecipeModel>> GetPrivateRecipesList()
        {
            return (await firebase
              .Child("LocalRecipe")
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
                  Tools = item.Object.Tools,
                  Diets = item.Object.Diets,
                  Allergies = item.Object.Allergies,
                  Photo = item.Object.Photo,
                  Public = item.Object.Public

              }).ToList();
        }

        /* Funkcia vygeneruje náhodný reťazec */
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /* Funkcia prídá do databázi nový lokálny recept */
        public async Task PushNewRecipe(RecipeModel newRecipe)
        {
            await firebase.Child("LocalRecipe").Child(RandomString(10)).PutAsync(new RecipeModel
            {
                Name = newRecipe.Name,
                Description = newRecipe.Description,
                Rating = newRecipe.Rating,
                Time = newRecipe.Time,
                Type = newRecipe.Type,
                Kitchen= newRecipe.Kitchen,
                Favourite = newRecipe.Favourite,
                Ingredients = newRecipe.Ingredients,
                Steps = newRecipe.Steps,
                Tools = newRecipe.Tools,
                Diets = newRecipe.Diets,
                Allergies = newRecipe.Allergies,
                Photo = newRecipe.Photo,
                Public = newRecipe.Public
            });
        }

        /* Funkcia aktualizuje existujúci lokálny recept */
        public async Task UpdateLocalRecipe(RecipeModel newRecipe, string name)
        {
            var item = (await firebase.Child("LocalRecipe").OnceAsync<RecipeModel>()).Where(x => x.Object.Name == name).FirstOrDefault();
            await firebase.Child("LocalRecipe").Child(item!.Key).PutAsync(new RecipeModel
            {
                Name = newRecipe.Name,
                Description = newRecipe.Description,
                Rating = newRecipe.Rating,
                Time = newRecipe.Time,
                Type = newRecipe.Type,
                Kitchen = newRecipe.Kitchen,
                Favourite = newRecipe.Favourite,
                Ingredients = newRecipe.Ingredients,
                Steps = newRecipe.Steps,
                Tools = newRecipe.Tools,
                Diets = newRecipe.Diets,
                Allergies = newRecipe.Allergies,
                Photo = newRecipe.Photo,
                Public = item.Object.Public,
            });
        }

        /* Funkcia aktualizuje existujúci globálny recept  */
        public async Task UpdateGlobalRecipe(RecipeModel newRecipe, string name)
        {
            var item = (await firebase.Child("Recipe").OnceAsync<RecipeModel>()).Where(x => x.Object.Name == name).FirstOrDefault();
            await firebase.Child("Recipe").Child(item!.Key).PutAsync(new RecipeModel
            {
                Name = newRecipe.Name,
                Description = newRecipe.Description,
                Rating = newRecipe.Rating,
                Time = newRecipe.Time,
                Type = newRecipe.Type,
                Kitchen = newRecipe.Kitchen,
                Favourite = newRecipe.Favourite,
                Ingredients = newRecipe.Ingredients,
                Steps = newRecipe.Steps,
                Tools = newRecipe.Tools,
                Diets = newRecipe.Diets,
                Allergies = newRecipe.Allergies,
                Photo = newRecipe.Photo,
            });
        }

        public async Task RemoveLocalRecipe(string name)
        {
            var item = (await firebase.Child("LocalRecipe").OnceAsync<RecipeModel>()).Where(x => x.Object.Name == name).FirstOrDefault();
            await firebase.Child("LocalRecipe").Child(item!.Key).DeleteAsync();
        }

        public async Task RemoveGlobalRecipe(string name)
        {
            var item = (await firebase.Child("Recipe").OnceAsync<RecipeModel>()).Where(x => x.Object.Name == name).FirstOrDefault();
            await firebase.Child("LocalRecipe").Child(item!.Key).PutAsync(new RecipeModel
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
                Tools = item.Object.Tools,
                Diets = item.Object.Diets,
                Allergies = item.Object.Allergies,
                Photo = item.Object.Photo,
                Public = false,
            });
            await firebase.Child("Recipe").Child(item!.Key).DeleteAsync();
        }

        public async Task LocalRecipeToGlobal(string name)
        {
            var item = (await firebase.Child("LocalRecipe").OnceAsync<RecipeModel>()).Where(x => x.Object.Name == name).FirstOrDefault();
            await firebase.Child("LocalRecipe").Child(item!.Key).PutAsync(new RecipeModel
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
                Tools = item.Object.Tools,
                Diets = item.Object.Diets,
                Allergies = item.Object.Allergies,
                Photo = item.Object.Photo,
                Public = true,
            });
            await firebase.Child("Recipe").Child(item!.Key).PutAsync(new RecipeModel
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
                Tools = item.Object.Tools,
                Diets = item.Object.Diets,
                Allergies = item.Object.Allergies,
                Photo = item.Object.Photo,
            });
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
                  Buy = item.Object.Buy,
                  InCart = item.Object.InCart,
                  ToBuy = item.Object.ToBuy

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
                    .Child(ingItem!.Key)
                    .PutAsync(new IngredientModel { 
                        Name = ingItem.Object.Name, 
                        Category = ingItem.Object.Category, 
                        Fat = ingItem.Object.Fat, 
                        Sugar = ingItem.Object.Sugar,
                        Proteins = ingItem.Object.Proteins, 
                        Calories = ingItem.Object.Calories, 
                        Have = value,
                        Buy = ingItem.Object.Buy,
                        InCart = ingItem.Object.InCart,
                        ToBuy = ingItem.Object.ToBuy
                    });
            }
            else if (property == "buy")
            {
                await firebase.Child("Ingredients")
                    .Child(category)
                    .Child(ingItem!.Key)
                    .PutAsync(new IngredientModel { 
                        Name = ingItem.Object.Name, 
                        Category = ingItem.Object.Category, 
                        Fat = ingItem.Object.Fat, 
                        Sugar = ingItem.Object.Sugar, 
                        Proteins = ingItem.Object.Proteins, 
                        Calories = ingItem.Object.Calories, 
                        Have = ingItem.Object.Have, 
                        Buy = value,
                        InCart = false,
                        ToBuy = ingItem.Object.ToBuy
                    });
            }
            else if (property == "inCart")
            {
                await firebase.Child("Ingredients")
                    .Child(category)
                    .Child(ingItem!.Key)
                    .PutAsync(new IngredientModel
                    {
                        Name = ingItem.Object.Name,
                        Category = ingItem.Object.Category,
                        Fat = ingItem.Object.Fat,
                        Sugar = ingItem.Object.Sugar,
                        Proteins = ingItem.Object.Proteins,
                        Calories = ingItem.Object.Calories,
                        Have = ingItem.Object.Have,
                        Buy = ingItem.Object.Buy,
                        InCart = value,
                        ToBuy = ingItem.Object.ToBuy
                    });
            }
        }

        public async Task UpdateIngredienceAmount(string category, string name, string value)
        {
            var ingItem = (await firebase
                .Child("Ingredients")
                .Child(category)
                .OnceAsync<IngredientModel>())
                .Where(x => x.Object.Name == name).FirstOrDefault();

            await firebase.Child("Ingredients")
                .Child(category)
                .Child(ingItem!.Key)
                .PutAsync(new IngredientModel
                {
                    Name = ingItem.Object.Name,
                    Category = ingItem.Object.Category,
                    Fat = ingItem.Object.Fat,
                    Sugar = ingItem.Object.Sugar,
                    Proteins = ingItem.Object.Proteins,
                    Calories = ingItem.Object.Calories,
                    Have = ingItem.Object.Have,
                    Buy = ingItem.Object.Buy,
                    InCart = ingItem.Object.InCart,
                    ToBuy = value
                });
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
                  Tools = item.Object.Tools,
                  Language = item.Object.Language,

              }).ToList();
        }

        /* Funkcia pre profil vyhľadá zadanú alergiu (alergyName) ktorá je na indexe (index) a nastaví jej "Have" na hodnotu value */
        public async Task UpdateAlergyHave(string alergyName, bool value, int index)
        {
            var profil = (await firebase
                .Child("Profil")
                .OnceAsync<ProfilModel>()).FirstOrDefault();


            profil!.Object.Alergy[index].Have = value;

            await firebase
                .Child("Profil")
                .Child(profil.Key)
                .PutAsync(new ProfilModel
                {
                    ProfilName = profil!.Object.ProfilName,
                    ProfilImage = profil!.Object.ProfilImage,
                    Alergy = profil!.Object.Alergy,
                    Diets = profil!.Object.Diets,
                    Tools = profil!.Object.Tools,
                    Language = profil!.Object.Language,
                });
        }

        /* Funkcia pre profil vyhľadá zadanú dietu (dietName) ktorá je na indexe (index) a nastaví jej "Have" na hodnotu value */
        public async Task UpdateDietHave(string dietName, bool value, int index)
        {
            var profil = (await firebase
                .Child("Profil")
                .OnceAsync<ProfilModel>()).FirstOrDefault();


            profil!.Object.Diets[index].Have = value;

            await firebase
                .Child("Profil")
                .Child(profil.Key)
                .PutAsync(new ProfilModel
                {
                    ProfilName = profil.Object.ProfilName,
                    ProfilImage = profil.Object.ProfilImage,
                    Alergy = profil.Object.Alergy,
                    Diets = profil.Object.Diets,
                    Tools = profil.Object.Tools,
                    Language = profil.Object.Language,
                });
        }

        public async Task UpdateProfilName(string newName)
        {
            var profil = (await firebase
                .Child("Profil")
                .OnceAsync<ProfilModel>()).FirstOrDefault();


            profil!.Object.ProfilName = newName;

            await firebase
                .Child("Profil")
                .Child(profil.Key)
                .PutAsync(new ProfilModel
                {
                    ProfilName = profil.Object.ProfilName,
                    ProfilImage = profil.Object.ProfilImage,
                    Alergy = profil.Object.Alergy,
                    Diets = profil.Object.Diets,
                    Tools = profil.Object.Tools,
                    Language = profil.Object.Language,
                });
        }

        public async Task UpdateProfilImage(string path)
        {
            var profil = (await firebase
                .Child("Profil")
                .OnceAsync<ProfilModel>()).FirstOrDefault();


            profil!.Object.ProfilImage = path;

            await firebase
                .Child("Profil")
                .Child(profil.Key)
                .PutAsync(new ProfilModel
                {
                    ProfilName = profil.Object.ProfilName,
                    ProfilImage = profil.Object.ProfilImage,
                    Alergy = profil.Object.Alergy,
                    Diets = profil.Object.Diets,
                    Tools = profil.Object.Tools,
                    Language = profil.Object.Language,
                });
        }

        public async Task UpdateToolHave(string toolName, bool value, int index)
        {
            var profil = (await firebase
                .Child("Profil")
                .OnceAsync<ProfilModel>()).FirstOrDefault();


            profil!.Object.Tools[index].Have = value;

            await firebase
                .Child("Profil")
                .Child(profil.Key)
                .PutAsync(new ProfilModel
                {
                    ProfilName = profil.Object.ProfilName,
                    ProfilImage = profil.Object.ProfilImage,
                    Alergy = profil.Object.Alergy,
                    Diets = profil.Object.Diets,
                    Tools = profil.Object.Tools,
                    Language = profil.Object.Language,
                });
        }

        public async Task<List<KitchenModel>> GetKitchenTypes()
        {
            return (await firebase
              .Child("Kitchen")
              .OnceAsync<KitchenModel>()).Select(item => new KitchenModel
              {
                  Kitchen = item.Object.Kitchen
              }).ToList();
        }

        public async Task<List<FoodTypeModel>> GetFoodTypeList()
        {
            return (await firebase
              .Child("FoodType")
              .OnceAsync<FoodTypeModel>()).Select(item => new FoodTypeModel
              {
                  Type = item.Object.Type
              }).ToList();
        }
    }
}

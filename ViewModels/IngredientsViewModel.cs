/**
 * IngredientViewModel.cs
 * Autor: Michal Ľaš (xlasmi00)
 *
*/

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class IngredientsViewModel : BaseClass
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        /* Observable Collections for ingredients categories */

        public ObservableCollection<IngredientModel> Fruits { get; } = new();
        public ObservableCollection<IngredientModel> Vegetables { get; } = new();
        public ObservableCollection<IngredientModel> Meat { get; } = new();
        public ObservableCollection<IngredientModel> Fish { get; } = new();
        public ObservableCollection<IngredientModel> Pasta { get; } = new();
        public ObservableCollection<IngredientModel> Pastry { get; } = new();
        public ObservableCollection<IngredientModel> Dairyproducts { get; } = new();
        public ObservableCollection<IngredientModel> Mushrooms { get; } = new();
        public ObservableCollection<IngredientModel> Oils { get; } = new();
        public ObservableCollection<IngredientModel> Nuts { get; } = new();
        public ObservableCollection<IngredientModel> Spices { get; } = new();
        public ObservableCollection<IngredientModel> Sweeteners { get; } = new();
        public ObservableCollection<IngredientModel> Sauces { get; } = new();
        public ObservableCollection<IngredientModel> ShoppingList { get; } = new();



        /* Commands */

        public ICommand SetFruitCommand => new Command<IngredientModel>(SetIngredientHave);
        public ICommand SetInCartCommand => new Command<IngredientModel>(SetInCartFirebase);
        public ICommand NavigateBackCommand => new Command(NavigateBack);
        public ICommand ClearShoppingListCommand => new Command(ClearShoppingList);
        public ICommand RemoveCommand => new Command<IngredientModel>(Remove);
        public ICommand ChangeToBuyCommand => new Command<IngredientModel>(ChangeToBuy);
        
        

        /* VIEWMODEL */
        public IngredientsViewModel(FirebaseHelper firebaseHelper)
        {
            Fruits = FruitsData;
            Vegetables = VegetablesData;
            Meat = MeatData;
            Fish = FishData;
            Pasta = PastaData;
            Pastry = PastryData;
            Dairyproducts = DairyproductsData;
            Mushrooms = MushroomsData;
            Oils = OilsData;
            Nuts = NutsData;
            Spices = SpicesData;
            Sweeteners = SweetenersData;
            Sauces = SaucesData;
            ShoppingList = ShoppingListData;
        }
        
        /* Set Ingredient "Have" property */
        /* If "Have" is true set to false */
        /* if "Have" is false set to true */
        public async void SetIngredientHave(IngredientModel ing)
        {
            await firebaseHelper.UpdateIngredience("have", ing.Category, ing.Name, !ing.Have);
            ing.Have = !ing.Have;
        }

        public async void SetInCartFirebase(IngredientModel ing)
        {
            if (ing.InCart)
            {
                await firebaseHelper.UpdateIngredience("inCart", ing.Category, ing.Name, false);
                ing.InCart = false;
                shoppingListCount++;
                Preferences.Default.Set("ShoppingListCount", shoppingListCount);

            }
            else
            {
                await firebaseHelper.UpdateIngredience("inCart", ing.Category, ing.Name, true);
                ing.InCart = true;
                shoppingListCount--;
                Preferences.Default.Set("ShoppingListCount", shoppingListCount);

            }
        }

        public async void NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async void ClearShoppingList()
        {
            bool answer = await Shell.Current.DisplayAlert("Pozor", "Opravdu si přejete vymazat nákupní seznam?", "Ano", "Ne");

            if (answer)
            {
                foreach (var item in ShoppingList)
                {
                    await firebaseHelper.UpdateIngredience("buy", item.Category, item.Name, false);
                    await firebaseHelper.UpdateIngredience("inCart", item.Category, item.Name, false);
                }

                ShoppingList.Clear();
                shoppingListCount = 0;
                IsEmpty = true;
            }
        }

        public async void Remove(IngredientModel obj)
        {
            await firebaseHelper.UpdateIngredience("buy", obj.Category, obj.Name, false);
            await firebaseHelper.UpdateIngredience("inCart", obj.Category, obj.Name, false);
            Remove(obj);
        }

        public async void ChangeToBuy(IngredientModel obj)
        {
            string result = await App.Current.MainPage!.DisplayPromptAsync("Množství", "Upravit množství k zakoupení", "OK", "Zrušit");

            if (result == null)
            {
                return;
            }

            await firebaseHelper.UpdateIngredienceAmount(obj.Category, obj.Name, result);
        }

    }
}

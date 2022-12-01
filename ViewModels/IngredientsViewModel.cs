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
        public Command GetIngredientCommand { get; }

        public ICommand SetInCartCommand => new Command<IngredientModel>(SetInCartFirebase);
        public ICommand NavigateBackCommand => new Command(NavigateBack);
        public ICommand ClearShoppingListCommand => new Command(ClearShoppingList);
        public ICommand RemoveCommand => new Command<IngredientModel>(Remove);

        /* VIEWMODEL */
        public IngredientsViewModel(FirebaseHelper firebaseHelper)
        {
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());
            GetIngredientCommand.Execute(this);
        }
        
        /* Set Ingredient "Have" property */
        /* If "Have" is true set to false */
        /* if "Have" is false set to true */
        public async void SetIngredientHave(IngredientModel ing)
        {
            if (ing.Have)
            {
                await firebaseHelper.UpdateIngredience("have", ing.Category, ing.Name, false);
                ing.Have = false;
            }
            else
            {
                await firebaseHelper.UpdateIngredience("have", ing.Category, ing.Name, true);
                ing.Have = true;
            }
        }

        public async void SetInCartFirebase(IngredientModel ing)
        {
            if (ing.InCart)
            {
                await firebaseHelper.UpdateIngredience("inCart", ing.Category, ing.Name, false);
                ing.InCart = false;
                shoppingListCount++;
            }
            else
            {
                await firebaseHelper.UpdateIngredience("inCart", ing.Category, ing.Name, true);
                ing.InCart = true;
                shoppingListCount--;
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


        /* Load Ingredients from database */
        async Task GetIngredietsAsync()
        {
            IsBusy = true;
            IsEmpty = true;
            try
            {
                var fruits = await firebaseHelper.GetIngredients("fruits");
                var vagetables = await firebaseHelper.GetIngredients("vegetables");
                var meat = await firebaseHelper.GetIngredients("meat");
                var fish = await firebaseHelper.GetIngredients("fish");
                var pasta = await firebaseHelper.GetIngredients("pasta");
                var pastry = await firebaseHelper.GetIngredients("pastry");
                var dairyproducts = await firebaseHelper.GetIngredients("dairyproducts");
                var mushrooms = await firebaseHelper.GetIngredients("mushrooms");
                var oils = await firebaseHelper.GetIngredients("oils");
                var nuts = await firebaseHelper.GetIngredients("nuts");
                var spices = await firebaseHelper.GetIngredients("spices");
                var sweeteners = await firebaseHelper.GetIngredients("sweeteners");
                var sauces = await firebaseHelper.GetIngredients("sauces");

                if (Fruits.Count != 0 || Vegetables.Count != 0 || Meat.Count != 0 || Fish.Count != 0 ||
                    Pasta.Count != 0 || Pastry.Count != 0 || Dairyproducts.Count != 0 || Mushrooms.Count != 0 ||
                    Oils.Count != 0 || Nuts.Count != 0 || Spices.Count != 0 || Sweeteners.Count != 0 || Sauces.Count != 0)
                {
                    Fruits.Clear();
                    Vegetables.Clear();
                    Meat.Clear();
                    Fish.Clear();
                    Pasta.Clear();
                    Pastry.Clear();
                    Dairyproducts.Clear();
                    Mushrooms.Clear();
                    Oils.Clear();
                    Nuts.Clear();
                    Spices.Clear();
                    Sweeteners.Clear();
                    Sauces.Clear();
                }

                foreach (var item in fruits)
                {
                    Fruits.Add(item);
                }
                foreach (var item in vagetables)
                {
                    Vegetables.Add(item);
                }
                foreach (var item in meat)
                {
                    Meat.Add(item);
                }
                foreach (var item in fish)
                {
                    Fish.Add(item);
                }
                foreach (var item in pasta)
                {
                    Pasta.Add(item);
                }
                foreach (var item in pastry)
                {
                    Pastry.Add(item);
                }
                foreach (var item in dairyproducts)
                {
                    Dairyproducts.Add(item);
                }
                foreach (var item in mushrooms)
                {
                    Mushrooms.Add(item);
                }
                foreach (var item in oils)
                {
                    Oils.Add(item);
                }
                foreach (var item in nuts)
                {
                    Nuts.Add(item);
                }
                foreach (var item in spices)
                {
                    Spices.Add(item);
                }
                foreach (var item in sweeteners)
                {
                    Sweeteners.Add(item);
                }
                foreach (var item in sauces)
                {
                    Sauces.Add(item);
                }

                var joined = new ObservableCollection<IngredientModel>(Fruits.Concat(Vegetables.Concat(Meat.Concat(Fish.Concat(Pasta.Concat(Pastry.Concat(Dairyproducts.Concat(Mushrooms.Concat(Oils.Concat(Nuts.Concat(Spices.Concat(Sweeteners))))))))))));

                ShoppingList.Clear();
                foreach (var item in joined.Where(x => x.Buy.Equals(true)))
                {
                    ShoppingList.Add(item);
                }

                shoppingListCount = ShoppingList.Where(x => x.InCart.Equals(false)).Count();

                if (shoppingListCount != 0)
                {
                    IsEmpty = false;
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Nelze načíst ingredience: {ex.Message}", "OK");
            }
        }
    }
}

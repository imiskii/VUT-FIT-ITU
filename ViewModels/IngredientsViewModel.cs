/**
 * IngredientViewModel.cs
 * Autor: Michal Ľaš (xlasmi00)
 * Autor: Ondřej Janečka (xjanec33)
 *
*/

using System.Collections.ObjectModel;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class IngredientsViewModel : BaseClass
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        /* ************************************************************************************************ */
        /* ****************************************  ZOBRAZOVANÉ DÁTA ************************************* */
        /* ************************************************************************************************ */

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



        /* ************************************************************************************************ */
        /* ********************************************** COMMANDS **************************************** */
        /* ************************************************************************************************ */

        public ICommand SetFruitCommand => new Command<IngredientModel>(SetIngredientHave);
        public ICommand SetInCartCommand => new Command<IngredientModel>(SetInCartFirebase);
        public ICommand NavigateBackCommand => new Command(NavigateBack);
        public ICommand ClearShoppingListCommand => new Command(ClearShoppingList);
        public ICommand RemoveCommand => new Command<IngredientModel>(Remove);
        public ICommand ChangeToBuyCommand => new Command<IngredientModel>(ChangeToBuy);
        public ICommand GetSearchCommand => new Command<string>(GetSearch);
        public ICommand AddToShoppingListCommand => new Command<IngredientModel>(AddToShoppingList);
        public ICommand ExpandSearchCommand => new Command(ExpandSearch);


        /* ************************************************************************************************ */
        /* ******************************************* VIEWMODEL ****************************************** */
        /* ************************************************************************************************ */

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

            IsCollapsed = true;
        }

        /* ************************************************************************************************ */
        /* ********************************************* FUNKCIE ****************************************** */
        /* ************************************************************************************************ */

        /* Funkcia nastaví vlastnosť ingredience "have" (vlastnenú) na true/false */
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

        /// <summary>
        /// Funkce vymaže nákpní seznam
        /// </summary>
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

        /// <summary>
        /// Funkce odebere vybranou položku z nákupního seznamu
        /// </summary>
        /// <param name="obj"></param>
        public async void Remove(IngredientModel obj)
        {
            await firebaseHelper.UpdateIngredience("buy", obj.Category, obj.Name, false);
            await firebaseHelper.UpdateIngredience("inCart", obj.Category, obj.Name, false);
            obj.InCart = false;
            obj.Buy = false;

            ShoppingListData.Remove(obj);
            if (obj.InCart == false)
                shoppingListCount--;
            if (ShoppingListData.Where(x => x.Buy == true).Count() == 0)
            {
                shoppingListCount = 0;
                IsEmpty = true;
            }
        }

        /// <summary>
        /// Funkce změní stav položky, zda je či není vložena do košíku
        /// </summary>
        /// <param name="obj"></param>
        public async void ChangeToBuy(IngredientModel obj)
        {
            string result = await Application.Current!.MainPage!.DisplayPromptAsync("Množství", "Upravit množství k zakoupení", "OK", "Zrušit");

            if (result == null)
            {
                return;
            }

            int oldIndex = ShoppingListData.IndexOf(obj);
            ShoppingListData.Remove(obj);
            obj.ToBuy = result;
            ShoppingListData.Add(obj);

            ShoppingListData.Move(oldIndex, ShoppingListData.IndexOf(obj));

            await firebaseHelper.UpdateIngredienceAmount(obj.Category, obj.Name, result);
        }

        /// <summary>
        /// Funkce vyhledá položky dle shody a uloží je do SearchResults
        /// </summary>
        /// <param name="querry"></param>
        public void GetSearch(string querry)
        {
            ObservableCollection<IngredientModel> filtered = new ObservableCollection<IngredientModel>();

            foreach (var item in JoinedIngredients.Where(x => x.Name.ToLower().Contains(querry.ToLower()) || 
                                                              x.Category.ToLower().Contains(querry.ToLower()))) 
            {
                if (item.Buy == false)
                    filtered.Add(item);
            }

            SearchResults.Clear();

            SearchResults = filtered;
        }

        /// <summary>
        /// Funkce přidá vybranou položku do nákupního seznamu a odtraní ji z výsledků vyhledávání
        /// </summary>
        /// <param name="obj"></param>
        public async void AddToShoppingList(IngredientModel obj)
        {
            await firebaseHelper.UpdateIngredience("buy", obj.Category, obj.Name, true);
            obj.Buy = true;
            shoppingListCount++;
            IsEmpty = false;
            SearchResults.Remove(obj);
            ShoppingListData.Add(obj);

            ShoppingListData = new ObservableCollection<IngredientModel>(ShoppingListData.Distinct());
        }

        public void ExpandSearch()
        {
            if (IsCollapsed == true)
                IsCollapsed = false;
            else
                IsCollapsed = true;
        }
    }
}

/**
 * BaseClass.cs
 * Autor: Ondřej Janečka (xjanec33)
 * Autor: Michal Ľaš (xlasmi00)
 *
*/

//using Android.Text.Format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    /// <summary>
    /// Třída slouží pro sdílení proměnných v celém řešení
    /// Implementace IsBusy stavu, na kterém je závyslí activity indicator
    /// Dále se zde uskutečňuje načtení dat z Firebase
    /// </summary>
    public partial class BaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Command GetIngredientCommand { get; set; }
        public Command GetProfilData { get; set; }
        public Command GetKitchenCommand { get; set; }
        public Command GetFoodTypeCommand { get; set; }

        static int count;
        bool isBusy;
        bool isEmpty;

        public static ObservableCollection<IngredientModel> FruitsData { get; set; } = new();
        public static ObservableCollection<IngredientModel> VegetablesData { get; set; } = new();
        public static ObservableCollection<IngredientModel> MeatData { get; set; } = new();
        public static ObservableCollection<IngredientModel> FishData { get; set; } = new();
        public static ObservableCollection<IngredientModel> PastaData { get; set; } = new();
        public static ObservableCollection<IngredientModel> PastryData { get; set; } = new();
        public static ObservableCollection<IngredientModel> DairyproductsData { get; set; } = new();
        public static ObservableCollection<IngredientModel> MushroomsData { get; set; } = new();
        public static ObservableCollection<IngredientModel> OilsData { get; set; } = new();
        public static ObservableCollection<IngredientModel> NutsData { get; set; } = new();
        public static ObservableCollection<IngredientModel> SpicesData { get; set; } = new();
        public static ObservableCollection<IngredientModel> SweetenersData { get; set; } = new();
        public static ObservableCollection<IngredientModel> SaucesData { get; set; } = new();

        bool light;
        bool system;
        bool dark;
        static ObservableCollection<IngredientModel> shoppingListData = new();

        public ObservableCollection<IngredientModel> ShoppingListData 
        { 
            get 
            {
                return shoppingListData;
            }
            set
            {
                shoppingListData = value;
                OnPropertyChanged();
            }
        }
        static ObservableCollection<IngredientModel> joinedIngredients = new();

        public ObservableCollection<IngredientModel> JoinedIngredients
        { 
            get 
            {
                return joinedIngredients;
            }
            set
            {
                joinedIngredients = value;
                OnPropertyChanged();
            }
        }
        static ObservableCollection<IngredientModel> searchResults = new();

        public ObservableCollection<IngredientModel> SearchResults
        { 
            get 
            {
                return searchResults;
            }
            set
            {
                searchResults = value;
                OnPropertyChanged();
            }
        }

        public bool LightTheme { 
            get 
            {
                return light;
            } 
            set 
            { 
                light = value;
                OnPropertyChanged(); 
            } 
        }
        public bool DarkTheme
        {
            get
            {
                return dark;
            }
            set
            {
                dark = value;
                OnPropertyChanged();
            }
        }
        public bool SystemTheme
        {
            get
            {
                return system;
            }
            set
            {
                system = value;
                OnPropertyChanged();
            }
        }

        public string Greeting { get; set; }

        public ObservableCollection<IngredientModel> SavedIngredients {  get; set; }

        public static ProfilModel ProfilData { get; } = new();
        public static ObservableCollection<KitchenModel> KitchenTypeData { get; } = new();
        public static ObservableCollection<FoodTypeModel> FootTypeData { get; } = new();
        public static RecipeModel DetailRecipe { get; set; }

        public int shoppingListCount
        {
            get 
            {
                return count; 
            }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => isBusy;

            set
            {
                if (isBusy == value) 
                    return;

                isBusy = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;

        public bool IsEmpty
        {
            get => isEmpty;

            set
            {
                if (isEmpty == value)
                    return;

                isEmpty = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsNotEmpty));
            }
        }
        public bool IsNotEmpty => !IsEmpty;

        /// <summary>
        /// Nastaví viditeľnosť filtrovacieho menu hodnotenia
        /// </summary>
        bool ratingMenuExpanded;
        public bool RatingMenuExpanded
        {
            get => ratingMenuExpanded;

            set
            {
                if (ratingMenuExpanded == value)
                    return;

                ratingMenuExpanded = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(RatingMenuCollapsed));
            }
        }
        public bool RatingMenuCollapsed => !ratingMenuExpanded;

        /// <summary>
        /// Nastaví viditeľnosť filtrovacieho menu hodnotenia
        /// </summary>
        bool timeMenuExpanded;
        public bool TimeMenuExpanded
        {
            get => timeMenuExpanded;

            set
            {
                if (timeMenuExpanded == value)
                    return;

                timeMenuExpanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeMenuCollapsed));
            }
        }
        public bool TimeMenuCollapsed => !timeMenuExpanded;

        /// <summary>
        /// Nastaví viditeľnosť na Popis a Suroviny v detaile receptu
        /// </summary>
        bool showRecipeDescription;
        public bool ShowRecipeDescription
        {
            get => showRecipeDescription;

            set
            {
                if (showRecipeDescription == value) 
                    return;

                showRecipeDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Nastaví viditeľnosť na Postup v detaile receptu
        /// </summary>
        bool showRecipeGuide;
        public bool ShowRecipeGuide
        {
            get => showRecipeGuide;

            set
            {
                if (showRecipeGuide == value)
                    return;

                showRecipeGuide = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Nastaví viditeľnosť na Nutričné hodnoty v detaile receptu
        /// </summary>
        bool showRecipeNutritions;
        public bool ShowRecipeNutritions
        {
            get => showRecipeNutritions;

            set
            {
                if (showRecipeNutritions == value)
                    return;

                showRecipeNutritions = value;
                OnPropertyChanged();
            }
        }

        bool isCollapsed;
        public bool IsCollapsed
        {
            get => isCollapsed;

            set
            {
                if (isCollapsed == value)
                    return;

                isCollapsed = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsNotCollapsed));
            }
        }
        public bool IsNotCollapsed => !isCollapsed;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /* Funkcia stiahne dáta o profile z databázy */
        public async Task GetLocalProfileAsync()
        {

            var data = await firebaseHelper.GetProfil();

            ProfilData.ProfilName = data[0].ProfilName;
            ProfilData.ProfilImage = data[0].ProfilImage;
            ProfilData.Alergy = data[0].Alergy;
            ProfilData.Diets = data[0].Diets;
            ProfilData.Tools = data[0].Tools;
            ProfilData.Language = data[0].Language;
        }


        /* Funkcia stiahne typy kuchýň z databázy */
        public async Task GetKitchenData()
        {
            var data = await firebaseHelper.GetKitchen();

            foreach (var item in data)
            {
                KitchenTypeData.Add(item);
            }
        }

        /* Funkcia stiahne typy jedál z databázy */
        public async Task GetFoodTypes()
        {
            var data = await firebaseHelper.GetFoodTypes();

            foreach (var item in data)
            {
                FootTypeData.Add(item);
            }
        }

        /* Funkcia načíta jednotlívé kategórie z databázy */
        public async Task GetIngredietsAsync()
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

                if (FruitsData.Count != 0 || VegetablesData.Count != 0 || MeatData.Count != 0 || FishData.Count != 0 ||
                    PastaData.Count != 0 || PastryData.Count != 0 || DairyproductsData.Count != 0 || MushroomsData.Count != 0 ||
                    OilsData.Count != 0 || NutsData.Count != 0 || SpicesData.Count != 0 || SweetenersData.Count != 0 || SaucesData.Count != 0)
                {
                    FruitsData.Clear();
                    VegetablesData.Clear();
                    MeatData.Clear();
                    FishData.Clear();
                    PastaData.Clear();
                    PastryData.Clear();
                    DairyproductsData.Clear();
                    MushroomsData.Clear();
                    OilsData.Clear();
                    NutsData.Clear();
                    SpicesData.Clear();
                    SweetenersData.Clear();
                    SaucesData.Clear();
                }

                foreach (var item in fruits)
                {
                    FruitsData.Add(item);
                }
                FruitsData = new ObservableCollection<IngredientModel>(FruitsData.OrderBy(i => i.Name));

                foreach (var item in vagetables)
                {
                    VegetablesData.Add(item);
                }
                VegetablesData = new ObservableCollection<IngredientModel>(VegetablesData.OrderBy(i => i.Name));

                foreach (var item in meat)
                {
                    MeatData.Add(item);
                }
                MeatData = new ObservableCollection<IngredientModel>(MeatData.OrderBy(i => i.Name));

                foreach (var item in fish)
                {
                    FishData.Add(item);
                }
                FishData = new ObservableCollection<IngredientModel>(FishData.OrderBy(i => i.Name));

                foreach (var item in pasta)
                {
                    PastaData.Add(item);
                }
                PastaData = new ObservableCollection<IngredientModel>(PastaData.OrderBy(i => i.Name));

                foreach (var item in pastry)
                {
                    PastryData.Add(item);
                }
                PastryData = new ObservableCollection<IngredientModel>(PastryData.OrderBy(i => i.Name));

                foreach (var item in dairyproducts)
                {
                    DairyproductsData.Add(item);
                }
                DairyproductsData = new ObservableCollection<IngredientModel>(DairyproductsData.OrderBy(i => i.Name));

                foreach (var item in mushrooms)
                {
                    MushroomsData.Add(item);
                }
                MushroomsData = new ObservableCollection<IngredientModel>(MushroomsData.OrderBy(i => i.Name));

                foreach (var item in oils)
                {
                    OilsData.Add(item);
                }
                OilsData = new ObservableCollection<IngredientModel>(OilsData.OrderBy(i => i.Name));

                foreach (var item in nuts)
                {
                    NutsData.Add(item);
                }
                NutsData = new ObservableCollection<IngredientModel>(NutsData.OrderBy(i => i.Name));

                foreach (var item in spices)
                {
                    SpicesData.Add(item);
                }
                SpicesData = new ObservableCollection<IngredientModel>(SpicesData.OrderBy(i => i.Name));

                foreach (var item in sweeteners)
                {
                    SweetenersData.Add(item);
                }
                SweetenersData = new ObservableCollection<IngredientModel>(SweetenersData.OrderBy(i => i.Name));

                foreach (var item in sauces)
                {
                    SaucesData.Add(item);
                }
                SaucesData = new ObservableCollection<IngredientModel>(SaucesData.OrderBy(i => i.Name));

                JoinedIngredients = new ObservableCollection<IngredientModel>(FruitsData.Concat(VegetablesData.Concat(MeatData.Concat(FishData.Concat(PastaData.Concat(PastryData.Concat(DairyproductsData.Concat(MushroomsData.Concat(OilsData.Concat(NutsData.Concat(SpicesData.Concat(SweetenersData))))))))))));
                
                ShoppingListData.Clear();
                foreach (var item in JoinedIngredients.Where(x => x.Buy.Equals(true)))
                {
                    ShoppingListData.Add(item);
                }

                shoppingListCount = ShoppingListData.Where(x => x.InCart.Equals(false)).Count();

                if (shoppingListCount != 0)
                {
                    IsEmpty = false;
                }

                Preferences.Default.Set("ShoppingListCount", shoppingListCount);

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

/**
 * BaseClass.cs
 * Autor: Ondřej Janečka (xjanec33)
 *
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class BaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Command GetProfilData { get; set; }
        public Command GetKitchenCommand { get; set; }
        public Command GetFoodTypeCommand { get; set; }


        int count;
        bool isBusy;
        bool isEmpty;

        bool light;
        bool system;
        bool dark;

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


        public ObservableCollection<IngredientModel> SavedIngredients {  get; set; }

        public static ProfilModel ProfilData { get; } = new();
        public static List<uint> PreparationTimeData;
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
            ProfilData.ProfilImageSource = ImageSource.FromFile(ProfilData.ProfilImage);
        }

        /* Funkcia inicializuje PreparationTimeData */
        public void PreparationTimeInit()
        {
            PreparationTimeData = new List<uint> { 10, 20, 30, 45, 60, 90, 120 };
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
    }
}

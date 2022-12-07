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

        int count;
        bool isBusy;
        bool isEmpty;

        //public static bool LightTheme {  get; set; }
        //public static bool DarkTheme {  get; set; }
        //public static bool SystemTheme {  get; set; }

        public static ObservableCollection<IngredientModel> SavedIngredients {  get; set; }
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
    }
}

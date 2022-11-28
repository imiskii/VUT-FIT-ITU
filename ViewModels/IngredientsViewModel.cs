using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class IngredientsViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ObservableCollection<IngredientModel> Fruits { get; } = new();
        public ObservableCollection<IngredientModel> Vegetables { get; } = new();

        public Command GetIngredientCommand { get; }
        public IngredientsViewModel(FirebaseHelper firebaseHelper)
        {
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());
            GetIngredientCommand.Execute(this);
        }

        async Task GetIngredietsAsync()
        {

            try
            {
                var fruits = await firebaseHelper.GetIngredients("fruits");
                var vagetables = await firebaseHelper.GetIngredients("vegetables");

                if (Fruits.Count != 0 || Vegetables.Count != 0)
                {
                    Fruits.Clear();
                    Vegetables.Clear();
                }

                foreach (var fruit in fruits)
                {
                    Fruits.Add(fruit);
                }

                foreach (var item in vagetables)
                {
                    Vegetables.Add(item);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Nelze načíst ingredience: {ex.Message}", "OK");
            }

        }
    }
}

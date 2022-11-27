using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class IngredientsViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ObservableCollection<IngredientModel> ingredient { get; } = new();

        public Command GetIngredientCommand { get; }
        public IngredientsViewModel(FirebaseHelper firebaseHelper)                          //
        {
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());     //
            GetIngredientCommand.Execute(this);
        }
        
        //[ICommand]

        async Task GetIngredietsAsync()
        {

            try
            {
                var fruits = await firebaseHelper.GetIngredients("fruits");

                if (ingredient.Count != 0)
                {
                    ingredient.Clear();
                }

                foreach (var fruit in fruits)
                {
                    ingredient.Add(fruit);
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

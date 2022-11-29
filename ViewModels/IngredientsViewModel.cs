using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class IngredientsViewModel : INotifyPropertyChanged
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public event PropertyChangedEventHandler PropertyChanged; 

        /* Observable Collections for ingredients categories */

        public ObservableCollection<IngredientModel> Fruits { get; } = new();
        public ObservableCollection<IngredientModel> Vegetables { get; } = new();

        /* Commands */

        public ICommand SetFruitCommand => new Command<IngredientModel>(SetIngredientHave);
        public Command GetIngredientCommand { get; }

        /* VIEWMODEL */
        public IngredientsViewModel(FirebaseHelper firebaseHelper)
        {
            GetIngredientCommand = new Command(async () => await GetIngredietsAsync());
            GetIngredientCommand.Execute(this);

        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /* Set Ingredient "Have" property */
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

        /* Load Ingredients from database */
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
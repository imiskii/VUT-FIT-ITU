using yummyCook.Firebase;

namespace yummyCook.Views.Main;

public partial class HomePage : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public HomePage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var recipes = await firebaseHelper.GetRecipes();
        var ing = await firebaseHelper.GetIngredients("fruits");

        recipesList.ItemsSource = recipes;

        IngCollection.ItemsSource = ing;
    }
}
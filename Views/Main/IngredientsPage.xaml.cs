/**
 * IngredientPage.xaml.cs
 * Autor: Michal ¼aš (xlasmi00)
 *
*/

using yummyCook.ViewModels;
using yummyCook.Firebase;
namespace yummyCook.Views.Main;

public partial class IngredientsPage : ContentPage
{

    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public IngredientsPage(IngredientsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel; 
    }
}
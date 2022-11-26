using yummyCook.ViewModels;

namespace yummyCook.Views.Main;

public partial class RecipesPage : ContentPage
{
	public RecipesPage()
	{
		InitializeComponent();
		BindingContext = new RecipeViewModel();
	}
}
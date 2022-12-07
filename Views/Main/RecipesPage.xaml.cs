using yummyCook.ViewModels;

namespace yummyCook.Views.Main;

public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using yummyCook.ViewModels;

namespace yummyCook.Views.Others;

public partial class RecipeNutritionPage : ContentPage
{
	public RecipeNutritionPage(RecipeNutritionViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel.recipeModel;
	}
}
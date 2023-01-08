using yummyCook.ViewModels;

namespace yummyCook.Views.Others;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage(RecipeDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		RecipeDetailViewModel.DetailRecipe = viewModel.recipeModel;
	}
}
using yummyCook.ViewModels;

namespace yummyCook.Views.Others;

public partial class RecipeCreatePage : ContentPage
{
	public RecipeCreatePage(ProfilViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
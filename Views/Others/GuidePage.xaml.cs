using yummyCook.ViewModels;

namespace yummyCook.Views.Others;

public partial class GuidePage : ContentPage
{
	public GuidePage(GuideViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel.recipeModel;
	}
}
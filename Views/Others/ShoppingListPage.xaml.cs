using yummyCook.Firebase;
using yummyCook.ViewModels;

namespace yummyCook.Views.Others;

public partial class ShoppingListPage : ContentPage
{
	public ShoppingListPage(IngredientsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
using yummyCook.ViewModels;
using System.ComponentModel;

namespace yummyCook.Views.Main;

public partial class HomePage : ContentPage
{
    public HomePage(RecipeViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

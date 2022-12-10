using yummyCook.ViewModels;

namespace yummyCook.Views.Main;


public partial class MorePage : ContentPage
{
    public MorePage(ProfilViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
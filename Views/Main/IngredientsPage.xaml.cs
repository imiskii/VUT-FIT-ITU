using yummyCook.ViewModels;
using System.ComponentModel;
using yummyCook.Firebase;

namespace yummyCook.Views.Main;

public partial class IngredientsPage : ContentPage
{
	int count = 0;
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public IngredientsPage(IngredientsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
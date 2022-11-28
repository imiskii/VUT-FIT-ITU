using yummyCook.ViewModels;
using System.ComponentModel;
using yummyCook.Firebase;

namespace yummyCook.Views.Main;

public partial class IngredientsPage : ContentPage
{

    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public IngredientsPage(IngredientsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel; 
    }
}
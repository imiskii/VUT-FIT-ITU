using yummyCook.Views.Main;
using yummyCook.Views.Others;

namespace yummyCook;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("shoppingList", typeof(ShoppingListPage));
		Routing.RegisterRoute("recipeDetail", typeof(RecipeDetailPage));
		Routing.RegisterRoute("recipeCreate", typeof(RecipeCreatePage));
		Routing.RegisterRoute("recipesList", typeof(RecipesPage));
	}
}

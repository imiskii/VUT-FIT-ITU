using banditoth.MAUI.Multilanguage;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
using yummyCook.Firebase;
using yummyCook.Resources.Translations;
using yummyCook.ViewModels;
using yummyCook.Views.Main;
using yummyCook.Views.Others;

namespace yummyCook;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSkiaSharp()

            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                //nahrani Font Awesosome
                fonts.AddFont("FontAwesome6-Regular.otf", "FontRegular");
                fonts.AddFont("FontAwesome6Brands-Regular.otf", "FontBrands");
                fonts.AddFont("FontAwesome-Solid.otf", "FontSolid");
            })

            //inicializece pluginu Multilanguage
            .ConfigureMultilanguage(config =>
            {
                config.UseResource(AppTranslations.ResourceManager);

                config.UseDefaultCulture(new System.Globalization.CultureInfo("cz-CS"));

                config.StoreLastUsedCulture(true);

                config.ThrowExceptionIfTranslationNotFound(false);

                config.SetTranslationNotFoundText("Transl_Not_Found:", appendTranslationKey: true);
            });

        builder.Services.AddSingleton<RecipeViewModel>();
        builder.Services.AddTransient<RecipeDetailViewModel>();
        builder.Services.AddSingleton<BaseClass>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<FirebaseHelper>();

        builder.Services.AddSingleton<IngredientsViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<IngredientsPage>();

        builder.Services.AddSingleton<ProfilViewModel>();
        builder.Services.AddSingleton<MorePage>();


        builder.Services.AddSingleton<ShoppingListPage>();
        builder.Services.AddTransient<RecipesPage>();
        builder.Services.AddTransient<RecipeDetailPage>();
        builder.Services.AddTransient<RecipeCreatePage>();

        return builder.Build();
	}
}

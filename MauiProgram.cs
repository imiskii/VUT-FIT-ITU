﻿using banditoth.MAUI.Multilanguage;
using yummyCook.Resources.Translations;

namespace yummyCook;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
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



		return builder.Build();
	}
}

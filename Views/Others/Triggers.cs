/**
 * Triggers.cs
 * Autor: Michal Ľaš (xlasmi00)
 *
 * Úpravy pro různá AppTheme: Ondřej Janečka (xjanec33)
*/

namespace yummyCook.IngredientTriggers
{
    public class ChangeStyle : TriggerAction<Button>
    {
        protected override void Invoke(Button btn)
        {
            if(Application.Current!.RequestedTheme == AppTheme.Dark)
            {
                if (btn.Margin == 2)
                {
                    if (btn.BorderWidth == 3)
                    {
                        btn.BackgroundColor = Color.FromArgb("#212121");
                        btn.BorderWidth = 2;
                        btn.TextColor = Color.FromArgb("#E1E1E1");
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.TextColor = Color.FromArgb("#000000");
                        btn.BorderWidth = 3;
                    }
                }
                else
                {
                    if (btn.BorderWidth == 2)
                    {
                        btn.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                        btn.BorderWidth = 1;
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.BorderWidth = 2;
                    }
                }
            }
            else
            {
                if (btn.Margin == 2)
                {
                    if (btn.BackgroundColor.Red == 1 && btn.BackgroundColor.Blue == 1 && btn.BackgroundColor.Green == 1)
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#ffffff");
                        btn.BorderColor = Color.FromArgb("#FBAA13");
                    }
                }
                else
                {
                    if (btn.BorderWidth == 2)
                    {
                        btn.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                        btn.BorderWidth = 1;
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.BorderWidth = 2;
                    }
                }
            }
        }
    }

    public class ChangeStyleAndText : TriggerAction<Button>
    {
        protected override void Invoke(Button btn)
        {
            if (Application.Current!.RequestedTheme == AppTheme.Dark)
            {
                if (btn.Margin == 2)
                {
                    if (btn.BorderWidth == 3)
                    {
                        btn.BackgroundColor = Color.FromArgb("#212121");
                        btn.BorderWidth = 2;
                        btn.TextColor = Color.FromArgb("#E1E1E1");
                        btn.Text = "Zveřejnit";
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.TextColor = Color.FromArgb("#000000");
                        btn.BorderWidth = 3;
                        btn.Text = "Veřejný";
                    }
                }
                else
                {
                    if (btn.BorderWidth == 2)
                    {
                        btn.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                        btn.BorderWidth = 1;
                        btn.Text = "Zveřejnit";
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.BorderWidth = 2;
                        btn.Text = "Veřejný";
                    }
                }
            }
            else
            {
                if (btn.Margin == 2)
                {
                    if (btn.BackgroundColor.Red == 1 && btn.BackgroundColor.Blue == 1 && btn.BackgroundColor.Green == 1)
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.Text = "Veřejný";
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#ffffff");
                        btn.BorderColor = Color.FromArgb("#FBAA13");
                        btn.Text = "Zveřejnit";
                    }
                }
                else
                {
                    if (btn.BorderWidth == 2)
                    {
                        btn.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                        btn.BorderWidth = 1;
                        btn.Text = "Zveřejnit";
                    }
                    else
                    {
                        btn.BackgroundColor = Color.FromArgb("#FBAA13");
                        btn.BorderWidth = 2;
                        btn.Text = "Veřejný";
                    }
                }
            }
        }
    }
}

/**
* Triggers.cs
* Autor: Michal Ľaš (xlasmi00)
*
*/
namespace yummyCook.Views.IngredientTriggers
{
    public class ChangeStyle : TriggerAction<Button>
    {
        protected override void Invoke(Button btn)
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
    }
}

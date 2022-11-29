using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyCook.IngredientTriggers
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

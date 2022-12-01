using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Firebase;

namespace yummyCook.RecipeDetailTriggers
{
    public class RecipeDetailTrigger : TriggerAction<Button>
    {
        protected override void Invoke(Button btn)
        {
            var item = btn.Parent;

            List<RecipeModel> list = new List<RecipeModel>();

            //IEnumerable<string> strings = from a in list.Where(x => x.pa)

        }
    }
}

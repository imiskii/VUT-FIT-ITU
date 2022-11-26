using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using yummyCook.Models;

namespace yummyCook.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RecipeModel _recipe;

        public RecipeModel Recipe
        {
            get => _recipe;
            set { _recipe = value; OnPropertyChanged(); }
        }

        public RecipeViewModel() { } // TODO

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

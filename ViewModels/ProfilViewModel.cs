/* ProfilViewModel.cs */
/* Autor: Michal Ľaš */

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class ProfilViewModel : BaseClass
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ProfilModel Profil { get; } = new();
        public List<uint> PreparationTime { get; } = new();
        public ObservableCollection<KitchenModel> KitchenType { get; } = new();
        public ObservableCollection<FoodTypeModel> FoodTypes { get; } = new();
        public RecipeModel EditedRecipeData { get; set; } = new();

        /* Editor vstupy */

        
        /// Postup
        string procedure;
        public string Procedure
        {
            get => procedure;
            set
            {
                procedure = value;
            }
        }

        /// Meno receptu
        string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        /// Popis receptu
        string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
            }
        }


        /* COMMANDS */
        public ICommand SetAlergyHave => new Command<Alergies>(SetProfilAlergyHave);
        public ICommand SetDietHave => new Command<Diets>(SetProfileDietHave);
        public ICommand SetToolHave => new Command<Tools>(SetProfileToolHave);
        public ICommand SetNewName => new Command(SetProfilName);
        public ICommand SetNewImage => new Command(SetProfilImage);
        public ICommand ShowShoppingList => new Command(async () => await ShowShoppingListAsync());
        public ICommand ShowRecipeCreatePage => new Command(async () => await ShowRecipeCreateAsync());
        public ICommand NavigateBackCommand => new Command(CreateRecipeNavigateBack);
        public ICommand AddOrRemoveDietRecipe => new Command<Diets>(AddOrRemoveRecipeDiet);
        public ICommand AddOrRemoveToolRecipe => new Command<Tools>(AddOrRemoveRecipeTool);
        public ICommand SaveNewRecipe => new Command(saveNewRecipe);
        public ICommand ThrowRecipe => new Command(throwRecipe);
        public ICommand LightSelectedCommand => new Command(LightSelected);
        public ICommand DarkSelectedCommand => new Command(DarkSelected);
        public ICommand SystemSelectedCommand => new Command(SystemSelected);
        public Command GetProfilCommand { get; set; }
        public Command LoadThemeCommand { get; set; }

        public ProfilViewModel() 
        {
            Profil = ProfilData;
            PreparationTime = PreparationTimeData;
            KitchenType = KitchenTypeData;
            FoodTypes = FootTypeData;

            EditedRecipeData = new RecipeModel();
            OnPropertyChanged(nameof(Profil));

            GetProfilCommand = new Command(async () => await GetLocalProfileAsync());
            GetProfilCommand.Execute(this);

            LoadThemeCommand = new Command(async () => LoadTheme());
            LoadThemeCommand.Execute(this);
        }

        /* FUNCTIONS */

        /* Otvor stránku na vytvorenie nákupného zoznamu */
        async Task ShowShoppingListAsync()
        {
            await Shell.Current.GoToAsync("shoppingList");
        }

        /* Otvor stránku na vytváranie receptov a inicializuje hodnoty */
        async Task ShowRecipeCreateAsync()
        {
            /// Set default picture
            /// EditedRecipeData.Photo = "Icons/image.png";
            /// EditedRecipeData.Steps = new List<Steps>();
            /// EditedRecipeData.Steps.Add(new Steps { Step = "", Index= 0 });
            EditedRecipeData.Diets = new List<Diets>();
            await Shell.Current.GoToAsync("recipeCreate");
        }

        /* Vráť sa späť o jednu stránku */
        async void CreateRecipeNavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        /* Nastaví premennú konkrétnej alergie na true/false */
        async void SetProfilAlergyHave(Alergies alergy)
        {
            await firebaseHelper.UpdateAlergyHave(alergy.Alergy, !alergy.Have, alergy.Index);
            alergy.Have = !alergy.Have;
        }

        /* Nastaví premennú konkrétnej diéty na true/false */
        async void SetProfileDietHave(Diets diet)
        {
            await firebaseHelper.UpdateDietHave(diet.Diet, !diet.Have, diet.Index);
            diet.Have = !diet.Have;
        }

        /* Nastaví premennú konkrétneho náradia na true/false */
        async void SetProfileToolHave(Tools tool)
        {
            await firebaseHelper.UpdateToolHave(tool.Tool, !tool.Have, tool.Index);
            tool.Have = !tool.Have;
        }

        /* Funkcia nastaví meno profilu */
        async void SetProfilName()
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Zmena mena", "Nové meno (maximálne 10 znakov):");

            if (result == null) 
            {
                return;
            }
            if (result.Length > 10)
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Príliš dlhé meno", "OK");
                return;
            }

            await firebaseHelper.UpdateProfilName(result);
            ProfilData.ProfilName = result;
            OnPropertyChanged("Profil");
        }

        /* Nastaví obrázok profilu */
        async void SetProfilImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            }); 

            if (result == null)
            {
                return;
            }

            var stream = await result.OpenReadAsync();
            ProfilData.ProfilImageSource = ImageSource.FromStream(() => stream);
            await firebaseHelper.UpdateProfilImage(result.FullPath);
            OnPropertyChanged("Profil");
        }
        /* Funkcia pridá alebo odoberie dietu z receptu */
        void AddOrRemoveRecipeDiet(Diets diet)
        {
            if (EditedRecipeData.Diets.Contains(diet))
            {
                EditedRecipeData.Diets.Remove(diet);
            }
            else
            {
                EditedRecipeData.Diets.Add(diet);
            }
        }

        /* Funkcia pridá alebo odoberie vybavenie z receptu */
        void AddOrRemoveRecipeTool(Tools tool)
        {
            if (EditedRecipeData.Tools.Contains(tool))
            {
                EditedRecipeData.Tools.Remove(tool);
            }
            else
            {
                EditedRecipeData.Tools.Add(tool);
            }
        }

        /* Funkcia skontroluje a uloží novo vytvorený recept do databázi */
        async void saveNewRecipe()
        {
            // Meno
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mať Meno", "OK");
                return;
            }
            else
            {
                EditedRecipeData.Name = Name;
            }

            // Obrázok

            // Popis (nie je vyžadovaný)
            EditedRecipeData.Description = Description;

            // Postup
            if (string.IsNullOrWhiteSpace(Procedure))
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mať Postup", "OK");
                return;
            }
            else
            {
                EditedRecipeData.Steps = new List<Steps>();
                var splitProcedure = Procedure.Split(Environment.NewLine);
                foreach (var item in splitProcedure)
                {
                    EditedRecipeData.Steps.Add( new Steps { Step = item, Index = EditedRecipeData.Steps.Count});
                }
            }
        }

        public bool LightTheme;
        public bool DarkTheme;
        public bool SystemTheme;
        public void LoadTheme()
        {
            DarkTheme = false;
            LightTheme = false;
            SystemTheme = false;

            switch (Application.Current.UserAppTheme)
            {
                case AppTheme.Dark:
                    DarkTheme = true;
                    break;

                case AppTheme.Light:
                    LightTheme = true;
                    break;

                case AppTheme.Unspecified:
                    SystemTheme = true;
                    break;
            }
        }


        public void LightSelected()
        {
            LightTheme = true;
            Application.Current.UserAppTheme = AppTheme.Light;
            Preferences.Default.Set("AppTheme", 1);
            LoadTheme();
        }
        public void DarkSelected()
        {
            DarkTheme = true;
            Application.Current.UserAppTheme = AppTheme.Dark;
            Preferences.Default.Set("AppTheme", 2);
            LoadTheme();
        }
        public void SystemSelected()
        {
            SystemTheme = true;
            Application.Current.UserAppTheme = AppTheme.Unspecified;
            Preferences.Default.Set("AppTheme", 0);
            LoadTheme();
        }
        /* Funkcia nanovo inicializuje premenné potrebné na tvorbu receptu a vráti aplikáciu o jednu stránku naspäť */
        void throwRecipe()
        {
            Name = string.Empty;
            Description = string.Empty;
            Procedure = string.Empty;
            EditedRecipeData.Diets.Clear();

            CreateRecipeNavigateBack();
        }
    }
}

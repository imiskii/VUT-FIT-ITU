﻿/* ProfilViewModel.cs */
/* Autor: Michal Ľaš */

using CommunityToolkit.Maui.Views;
using Firebase.Database;
using System.Collections.ObjectModel;
using System.Windows.Input;
using yummyCook.Firebase;
using yummyCook.Views.Others;

namespace yummyCook.ViewModels
{
    public partial class ProfilViewModel : BaseClass
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        /* Ingrediencie */
        public ObservableCollection<IngredientModel> Fruits { get; } = new();
        public ObservableCollection<IngredientModel> Vegetables { get; } = new();
        public ObservableCollection<IngredientModel> Meat { get; } = new();
        public ObservableCollection<IngredientModel> Fish { get; } = new();
        public ObservableCollection<IngredientModel> Pasta { get; } = new();
        public ObservableCollection<IngredientModel> Pastry { get; } = new();
        public ObservableCollection<IngredientModel> Dairyproducts { get; } = new();
        public ObservableCollection<IngredientModel> Mushrooms { get; } = new();
        public ObservableCollection<IngredientModel> Oils { get; } = new();
        public ObservableCollection<IngredientModel> Nuts { get; } = new();
        public ObservableCollection<IngredientModel> Spices { get; } = new();
        public ObservableCollection<IngredientModel> Sweeteners { get; } = new();
        public ObservableCollection<IngredientModel> Sauces { get; } = new();

        /*****/

        public List<ObservableCollection<IngredientModel>> Categories { get; set; } = new();
        public ProfilModel Profil { get; } = new();
        public List<uint> PreparationTime { get; } = new();
        public List<string> FoodTypes { get; } = new();
        public List<string> KitchenType { get; } = new();
        public RecipeModel EditedRecipeData { get; set; } = new();
        public IngrediencePopUp newPopUpIngrediencePage { get; set; }
        public ObservableCollection<RecipeModel> LocalRecipes { get; set; } = new();

        /* RECIPE CREATE VALUES */

        
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
        /// Čaš prípravy
        int time;
        public int Time
        {
            get => time;
            set
            {
                time = value;
            }
        }

        /// Typ jedla
        string ftype;
        public string FType
        {
            get => ftype;
            set
            {
                ftype = value;
            }
        }

        /// Druh kuchyne
        string kitchenT;
        public string KitchenT
        {
            get => kitchenT;
            set
            {
                kitchenT = value;
            }
        }

        private ObservableCollection<string> _NewRecipeIngredience;
        public ObservableCollection<string> NewRecipeIngredience
        {
            get { return _NewRecipeIngredience; }
            set
            {
                this._NewRecipeIngredience = value;
                OnPropertyChanged(nameof(NewRecipeIngredience));
            }
        }

        /* COMMANDS */
        public ICommand SetAlergyHave => new Command<Alergies>(SetProfilAlergyHave);
        public ICommand SetDietHave => new Command<Diets>(SetProfileDietHave);
        public ICommand SetToolHave => new Command<Tools>(SetProfileToolHave);
        public ICommand SetNewName => new Command(SetProfilName);
        public ICommand SetNewImage => new Command(SetProfilImage);
        public ICommand ShowShoppingList => new Command(async () => await ShowShoppingListAsync());
        public ICommand GoToDetailCommand => new Command<RecipeModel>(GoToDetailAsync);
        public ICommand ShowRecipeCreatePage => new Command(async () => await ShowRecipeCreateAsync());
        public ICommand NavigateBackCommand => new Command(CreateRecipeNavigateBack);
        public ICommand AddOrRemoveDietRecipe => new Command<Diets>(AddOrRemoveRecipeDiet);
        public ICommand AddOrRemoveToolRecipe => new Command<Tools>(AddOrRemoveRecipeTool);
        public ICommand OpenIngrediencePopUpPageCommand => new Command(OpenIngrediencePopUp);
        public ICommand CloseIngrediencePopUpPageCommand => new Command(CloseIngrediencePopUp);
        public ICommand AddOrRemoveIngredienceRecipe => new Command<IngredientModel>(AddOrRemoveIngRecipe);
        public ICommand RemoveIngredienceRecipe => new Command<string>(RemoveIngRecipe);
        public ICommand SaveNewRecipe => new Command(saveNewRecipe);
        public ICommand ThrowRecipe => new Command(throwRecipe);
        public ICommand DeleteLocalRecipeCommand => new Command<string>(DeleteLocalRecipe);
        public ICommand AddOrDeleteRecipeToGlobalDatabase => new Command<string>(AddOrRemoveRecipeToGlobal);
        public ICommand EditRecipeCommand => new Command<RecipeModel>(EditRecipe);
        public Command GetLocalRecipesCommand { get; set; }
        public ICommand LightSelectedCommand => new Command(LightSelected);
        public ICommand DarkSelectedCommand => new Command(DarkSelected);
        public ICommand SystemSelectedCommand => new Command(SystemSelected);
        public Command GetProfilCommand { get; set; }
        public Command LoadThemeCommand { get; set; }

        public ProfilViewModel() 
        {
            Fruits = FruitsData;
            Categories.Add(Fruits);
            Vegetables = VegetablesData;
            Categories.Add(Vegetables);
            Meat = MeatData;
            Categories.Add(Meat);
            Fish = FishData;
            Categories.Add(Fish);
            Pasta = PastaData;
            Categories.Add(Pasta);
            Pastry = PastryData;
            Categories.Add(Pastry);
            Dairyproducts = DairyproductsData;
            Categories.Add(Dairyproducts);
            Mushrooms = MushroomsData;
            Categories.Add(Mushrooms);
            Oils = OilsData;
            Categories.Add(Oils);
            Nuts = NutsData;
            Categories.Add(Nuts);
            Spices = SpicesData;
            Categories.Add(Spices);
            Sweeteners = SweetenersData;
            Categories.Add(Sweeteners);
            Sauces = SaucesData;
            Categories.Add(Sauces);

            Profil = ProfilData;
            PreparationTime = PreparationTimeData;
            foreach (var item in FootTypeData)
            {
                FoodTypes.Add(item.Type);
            }
            foreach (var item in KitchenTypeData)
            {
                KitchenType.Add(item.Kitchen);
            }

            NewRecipeIngredience = new ObservableCollection<string>();
            EditedRecipeData = new RecipeModel();
            EditedRecipeData.Ingredients = new List<Ingredients>();
            GetLocalRecipesCommand = new Command(async () => await GetLocalRecipes());
            GetLocalRecipesCommand.Execute(this);
            OnPropertyChanged(nameof(Profil));
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
            if (EditedRecipeData.Diets == null) { EditedRecipeData.Diets = new List<Diets>(); }
            if (EditedRecipeData.Tools == null) { EditedRecipeData.Tools = new List<Tools>(); }
            if (EditedRecipeData.Ingredients == null) { EditedRecipeData.Ingredients = new List<Ingredients>(); }

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
            string result = await App.Current!.MainPage!.DisplayPromptAsync("Zmena mena", "Nové meno (maximálne 10 znakov):");

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

        /* Funkcia otvorí IngrediencePopUpPage */
        void OpenIngrediencePopUp()
        {
            newPopUpIngrediencePage = new IngrediencePopUp();
            Application.Current!.MainPage!.ShowPopup(newPopUpIngrediencePage);
        }

        /* Funkcia zatvorí IngrediencePopUpPage */
        void CloseIngrediencePopUp()
        {
            newPopUpIngrediencePage.Close();
        }

        /* Funkcia pridá/odoberie ingredienciu do EditedRecipeData.Ingredients */
        void AddOrRemoveIngRecipe(IngredientModel Ing)
        {
            Ingredients newIng = new Ingredients { Name = Ing.Name, Category = Ing.Category };

            foreach(var item in EditedRecipeData.Ingredients)
            {
                if (item.Name == newIng.Name)
                {
                    Ing.InNewRecipe = false;
                    NewRecipeIngredience.Remove(Ing.Name);
                    EditedRecipeData.Ingredients.Remove(item);
                    return;
                }
            }
            Ing.InNewRecipe = true;
            NewRecipeIngredience.Add(newIng.Name);
            EditedRecipeData.Ingredients.Add(newIng);
        }

        /* Funkcia odoberie ingredienciu z EditedRecipeData.Ingredients */
        void RemoveIngRecipe(string Ing)
        {
            EditedRecipeData.Ingredients.Remove(EditedRecipeData.Ingredients.Where(x => x.Name == Ing).FirstOrDefault());
            NewRecipeIngredience.Remove(Ing);
            foreach (var list in Categories)
            {
                foreach(var item in list)
                {
                    if (item.Name == Ing)
                    {
                        item.InNewRecipe = false;
                        return;
                    }
                }
            }
        }

        /* Funkcia skontroluje a uloží novo vytvorený recept do databázi */
        async void saveNewRecipe()
        {

            // Meno
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mít Jméno", "OK");
                return;
            }
            else
            {
                EditedRecipeData.Name = Name;
            }

            // Obrázok

            // Popis (nie je vyžadovaný)
            EditedRecipeData.Description = Description;

            // Ingrediencie
            if (EditedRecipeData.Ingredients.Count == 0)
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mít nějaké ingredience", "OK");
                return;
            }

            // Postup
            if (string.IsNullOrWhiteSpace(Procedure))
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mít Postup", "OK");
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

            // Čas prípravy
            if (Time == 0)
            {
                await Shell.Current.DisplayAlert("Chyba!", $"Recept musí mít daný Čas přípravy", "OK");
                return;
            }
            else
            {
                EditedRecipeData.Time = Time;
            }

            // Typ jedla (nie je vyžadovaný)
            EditedRecipeData.Type = FType;

            // Druh kuchyne (nie je vyžadovaný)
            EditedRecipeData.Kitchen = KitchenT;

            EditedRecipeData.Public = false;

            try
            {
                if (LocalRecipes.Where(x => x.Name == savedName).FirstOrDefault() != null)
                {
                    if (LocalRecipes.Where(x => x.Name == savedName).FirstOrDefault()!.Public)
                    {
                        await firebaseHelper.UpdateGlobalRecipe(EditedRecipeData ,savedName);
                    }
                    await firebaseHelper.UpdateLocalRecipe(EditedRecipeData ,savedName);
                }
                else
                {
                    await firebaseHelper.PushNewRecipe(EditedRecipeData);
                }
            }
            finally
            {
                LocalRecipes.Clear();
                await GetLocalRecipes();

                throwRecipe();

                OnPropertyChanged(nameof(LocalRecipes));
            }
        }

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
            Application.Current!.UserAppTheme = AppTheme.Light;
            Preferences.Default.Set("AppTheme", 1);
            LoadTheme();
        }
        public void DarkSelected()
        {
            DarkTheme = true;
            Application.Current!.UserAppTheme = AppTheme.Dark;
            Preferences.Default.Set("AppTheme", 2);
            LoadTheme();
        }
        public void SystemSelected()
        {
            SystemTheme = true;
            Application.Current!.UserAppTheme = AppTheme.Unspecified;
            Preferences.Default.Set("AppTheme", 0);
            LoadTheme();
        }

        /* Funkcia nanovo inicializuje premenné potrebné na tvorbu receptu a vráti aplikáciu o jednu stránku naspäť */
        void throwRecipe()
        {
            Name = string.Empty;
            // TODO: del image
            Description = string.Empty;
            Procedure = string.Empty;
            Time = 0;
            FType = string.Empty;
            KitchenT = string.Empty;
            EditedRecipeData.Diets.Clear();
            EditedRecipeData.Tools.Clear();
            EditedRecipeData.Ingredients.Clear();
            NewRecipeIngredience.Clear();
            foreach (var list in Categories)
            {
                foreach (var item in list)
                {
                    item.InNewRecipe = false;
                }
            }
            foreach (var item in Profil.Diets)
            {
                item.InNewRecipe = false;
            } 
            foreach (var item in Profil.Diets)
            {
                item.InNewRecipe = false;
            }

            CreateRecipeNavigateBack();
        }

        /* Premenná savedName uchováva hodnotu mena meneného receptu aby sa recept mohol aj po zmene jeho mena vyhľadať a upraviť */
        string savedName;
        /* Funkcia otvorí stránku na vytváranie receptov vyplnenú informáciami o recepte, ktorý chce užívateľ upraviť */
        async void EditRecipe(RecipeModel recipe)
        {
            /* Vyplniť informácie o recepte */

            EditedRecipeData.Name = recipe.Name;
            Name = recipe.Name;
            savedName = recipe.Name;
            // TODO: add image
            EditedRecipeData.Description= recipe.Description;
            Description = recipe.Description;
            EditedRecipeData.Steps = recipe.Steps;
            foreach (var step in recipe.Steps)
            {
                Procedure = Procedure + step.Step + "\n";
            }
            EditedRecipeData.Diets = recipe.Diets;
            if (recipe.Diets != null)
            {
                foreach (var item in recipe.Diets)
                {
                    Profil.Diets.Where(x => x.Diet == item.Diet).FirstOrDefault()!.InNewRecipe = true;
                }
            }
            EditedRecipeData.Tools = recipe.Tools;
            if (recipe.Tools != null)
            {
                foreach (var item in recipe.Tools)
                {
                    Profil.Tools.Where(x => x.Tool == item.Tool).FirstOrDefault()!.InNewRecipe = true;
                }
            }
            EditedRecipeData.Time = recipe.Time;
            Time = recipe.Time;
            EditedRecipeData.Type = recipe.Type;
            FType = recipe.Type;
            EditedRecipeData.Kitchen = recipe.Kitchen;
            KitchenT = recipe.Kitchen;
            EditedRecipeData.Ingredients = recipe.Ingredients;
            foreach (var ing in recipe.Ingredients)
            {
                NewRecipeIngredience.Add(ing.Name);
            }
            foreach (var list in Categories)
            {
                foreach (var item in list)
                {
                    if (NewRecipeIngredience.Contains(item.Name))
                    {
                        item.InNewRecipe = true;
                    }
                }
            }
            /* Zobraziť recept */
            await ShowRecipeCreateAsync();
        }

        /* Funkcia načíta privátne recepty užívateľa z databázi */
        async Task GetLocalRecipes()
        {
            var recipes = await firebaseHelper.GetPrivateRecipes();

            foreach (var item in recipes)
            {
                LocalRecipes.Add(item);
            }
        }

        /* Funkcia otvorí detail receptu */
        async void GoToDetailAsync(RecipeModel recipe)
        {
            DetailRecipe = recipe;

            await Shell.Current.GoToAsync("recipeDetail");
        }

        /* Funkcia vymaže lokálny recept užívateľa */
        async void DeleteLocalRecipe(string name)
        {
            await firebaseHelper.RemoveLocalRecipe(name);
            LocalRecipes.Remove(LocalRecipes.Where(x => x.Name == name).FirstOrDefault());
            OnPropertyChanged(nameof(LocalRecipes));
        }

        /* Funkcia pridá/odobrie recept do/z globálnej databázy receptov podľa toho či recept už je v globálnej databáze alebo nie */
        /* Ak sa recept nachádza v globálnej databáze, tak ho odstráni inak ho pidá do globálnej databázi receptov */
        async void AddOrRemoveRecipeToGlobal(string name)
        {
            if (LocalRecipes.Where(x => x.Name == name).FirstOrDefault()!.Public)
            {
                LocalRecipes.Where(x => x.Name == name).FirstOrDefault()!.Public = false;
                await firebaseHelper.RemoveGlobalRecipe(name);
            }
            else
            {
                LocalRecipes.Where(x => x.Name == name).FirstOrDefault()!.Public = true;
                await firebaseHelper.LocalRecipeToGlobal(name);
            }
        }
    }
}

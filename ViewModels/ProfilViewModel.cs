/* ProfilViewModel.cs */
/* Autor: Michal Ľaš */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using yummyCook.Firebase;

namespace yummyCook.ViewModels
{
    public partial class ProfilViewModel : INotifyPropertyChanged
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfilModel ProfilData { get; } = new();

        /* COMMANDS */
        public ICommand SetAlergyHave => new Command<Alergies>(SetProfilAlergyHave);
        public ICommand SetDietHave => new Command<Diets>(SetProfileDietHave);
        public ICommand SetToolHave => new Command<Tools>(SetProfileToolHave);
        public ICommand SetNewName => new Command(SetProfilName);
        public ICommand SetNewImage => new Command(SetProfilImage);
        public ICommand LightSelectedCommand => new Command(LightSelected);
        public ICommand DarkSelectedCommand => new Command(DarkSelected);
        public ICommand SystemSelectedCommand => new Command(SystemSelected);
        public Command GetProfilCommand { get; set; }
        public Command LoadThemeCommand { get; set; }

        public ProfilViewModel() 
        {
            GetProfilCommand = new Command(async () => await GetLocalProfileAsync());
            GetProfilCommand.Execute(this);

            LoadThemeCommand = new Command(async () => LoadTheme());
            LoadThemeCommand.Execute(this);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async Task GetLocalProfileAsync()
        {

            var data = await firebaseHelper.GetProfil();

            ProfilData.ProfilName = data[0].ProfilName;
            ProfilData.ProfilImage = data[0].ProfilImage;
            ProfilData.Alergy = data[0].Alergy;
            ProfilData.Diets = data[0].Diets;
            ProfilData.Tools = data[0].Tools;
            ProfilData.Language = data[0].Language;
            ProfilData.ProfilImageSource = ImageSource.FromFile(ProfilData.ProfilImage);
            OnPropertyChanged("ProfilData");
        }


        public async void SetProfilAlergyHave(Alergies alergy)
        {
            await firebaseHelper.UpdateAlergyHave(alergy.Alergy, !alergy.Have, alergy.Index);
            alergy.Have = !alergy.Have;
        }

        public async void SetProfileDietHave(Diets diet)
        {
            await firebaseHelper.UpdateDietHave(diet.Diet, !diet.Have, diet.Index);
            diet.Have = !diet.Have;
        }
        public async void SetProfileToolHave(Tools tool)
        {
            await firebaseHelper.UpdateToolHave(tool.Tool, !tool.Have, tool.Index);
            tool.Have = !tool.Have;
        }

        public async void SetProfilName()
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
            OnPropertyChanged("ProfilData");
        }

        public async void SetProfilImage()
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
            OnPropertyChanged("ProfilData");
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
    }
}

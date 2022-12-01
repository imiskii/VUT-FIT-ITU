using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Command GetProfilCommand { get; set; }

        public ProfilViewModel() 
        {
            GetProfilCommand = new Command(async () => await GetLocalProfileAsync());
            GetProfilCommand.Execute(this);
        }

        async Task GetLocalProfileAsync()
        {

            var data = await firebaseHelper.GetProfil();


            ProfilData.ProfilName = data[0].ProfilName;
            ProfilData.ProfilImage = data[0].ProfilImage;
            ProfilData.Alergy = data[0].Alergy;
            ProfilData.Diets = data[0].Diets;
            ProfilData.Language = data[0].Language;


            /*
            foreach (var item in data)
            {
                ProfilData.Add(item);
            }
            */

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void SetProfilAlergyHave(Alergies alergy)
        {
            //await profilDataMapper.SetProfilAlergy(alergy);
        }

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;

namespace WerfLogApp.ViewModels
{
    public partial class TijdregistratieViewModel : ObservableObject
    {
        //De volledige collection. -> wordt geobserveerd voor wijzigingen (2Way)
        [ObservableProperty]
        private ObservableCollection<string> _maanden;

        [ObservableProperty]
        private ObservableCollection<string> _jaren;

        [ObservableProperty]
        private ObservableCollection<TijdregistratieDto> _tijdregistraties;

        //Individuele selectie 
        [ObservableProperty]
        private string _geselecteerdJaar;

        [ObservableProperty]
        private string _geselecteerdeMaand;

        [ObservableProperty]
        private int _totaalUren;

        [ObservableProperty]
        private TijdregistratieDto _geselecteerdeTijdregistratie;

        private ITijdregistratieManager _tijdregistratieManager;





        public TijdregistratieViewModel(ITijdregistratieManager tijdregistratieManager)
        {
            _tijdregistratieManager = tijdregistratieManager;

            //Maanden toevoegen aan Picker. --> Deze worden doorgegeven naar BL en DAL voor dynamisch opvragen van registratiedata
            _maanden = new ObservableCollection<string>(Enumerable.Range(1, 12).Select(m => m.ToString("00"))); // Maanden 
            //Jaren toevoegen aan Picker.
            _jaren = new ObservableCollection<string>(Enumerable.Range(DateTime.Now.Year, 12).Select(year => year.ToString()));
        }

        public Command OverzichtCommand => new Command(async () => await RegistratiesLaden());

        //METHODE AANMAKEN WERF
        public async Task RegistratiesLaden()
        {
            // Controleer of de geselecteerde maand en jaar niet leeg zijn
            if (string.IsNullOrEmpty(GeselecteerdeMaand) || string.IsNullOrEmpty(GeselecteerdJaar))
            {
                // Doe hier wat je wilt als de maand of jaar niet zijn geselecteerd
                return;
            }

            // Converteer de geselecteerde maand en jaar naar int
            if (!int.TryParse(GeselecteerdeMaand, out int maand) || !int.TryParse(GeselecteerdJaar, out int jaar))
            {
                // Doe hier wat je wilt als de conversie mislukt
                return;
            }

            // Laad de tijdregistraties op basis van de geselecteerde maand en jaar
            var tijdregistraties = await _tijdregistratieManager.GetAlleTijdRegistratiesMaand(maand, jaar);

            // Converteer de lijst naar een ObservableCollection
            Tijdregistraties = new ObservableCollection<TijdregistratieDto>(tijdregistraties);

            if(tijdregistraties is not null) 
            
            {
               var totaalUren =  await _tijdregistratieManager.GetTotaalTijdRegistratiesMaand(maand, jaar);

                TotaalUren = totaalUren;
            }
        }

    }
}



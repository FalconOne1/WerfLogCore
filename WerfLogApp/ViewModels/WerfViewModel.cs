
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogBl.Managers;
using static System.Net.Mime.MediaTypeNames;

namespace WerfLogApp.ViewModels
{
    public partial class WerfViewModel : ObservableObject
    {

        private IWerfManager _werfManager;

        [ObservableProperty]
        private string _nieuweWerfText; //achterliggend wordt een public NieuweWerfText aangemaakt. 

        //private field
        private ObservableCollection<WerfDto> _werven;

        //public getter & setter 
        public ObservableCollection<WerfDto> Werven
        {
            get => _werven;
            set
            {
                _werven = value;
                OnPropertyChanged(); //automatisch updaten UI/logica -> toegankelijk door ObservableObject.
            }
        }

        //constructor met dummy gegevens (tijdelijk)

        public WerfViewModel(IWerfManager werfManager)
        {
            _werfManager = werfManager;

            Werven = new ObservableCollection<WerfDto>()
            {
                new WerfDto {Id= 1, Naam = "test1"},
                new WerfDto {Id= 2, Naam = "test2"},
                new WerfDto {Id= 3, Naam = "test3"}
            };
         
        }


        //aanmaken 
        public Command VoegWerfToeCommand => new Command(WerfAanmaken);

        //public void WerfAanmaken()
        //{
        //    if (!string.IsNullOrEmpty(NieuweWerfText))
        //    {
        //        Werven.Insert(0, new WerfDto { Id = Guid.NewGuid().ToString(), Naam = NieuweWerfText });   //insert om nieuwe entry vanboven in de lijst te krijgen ipv onder 
        //        NieuweWerfText = string.Empty;
        //    }
        //}

        public void WerfAanmaken()
        {
            if (!string.IsNullOrEmpty(NieuweWerfText))
            {
                var werfDto = new WerfDto { Naam = NieuweWerfText, Id = null};

                // Voeg de Werf toe en krijg de bijgewerkte WerfDto terug (met de nieuwe ID)
                var toegevoegdeWerfDto = _werfManager.AddWerf(werfDto);

                if (toegevoegdeWerfDto != null)
                {
                    // De werf is succesvol toegevoegd aan de database
                    Werven.Insert(0, toegevoegdeWerfDto); // Voeg de bijgewerkte DTO toe aan de ObservableCollection

                    // Maak het invoerveld leeg
                    NieuweWerfText = string.Empty;
                }
                else
                {
                    // Handel de situatie af als het toevoegen mislukt (toon een foutmelding, etc.)
                }
            }
        }


        //command voor verwijderen van een werf, dus geen EventHandler.
        //Linkt met databinding in xaml mainpage.
        public Command<WerfDto> VerwijderCommand => new Command<WerfDto>(async (werf) =>
        {
            var bevestigVerwijderen = await App.Current.MainPage.DisplayAlert("Bevestigen", "Weet u zeker dat u deze werf wilt verwijderen?", "Ja", "Nee");

            if (bevestigVerwijderen)
            {
                // Voer de verwijderactie uit
                WerfVerwijderen(werf);
            }
        });

        public void WerfVerwijderen(WerfDto werf)
        {
            Werven.Remove(werf);
        }

        //navigatie notitie 

        //public Command<int> NotitieCommand => new Command<int>(TapWerf); // was string, tijdelijk naar int 
        //public async void TapWerf(int werfId)
        //{
        //    await Shell.Current.GoToAsync($"{nameof(NotitiePage)}?text={werfId}"); //vermoedelijk is werfnaam ID geworden !!!
        //    Console.WriteLine();
        //}


        public Command<WerfDto> NotitieCommand => new Command<WerfDto>(TapWerf);

        public async void TapWerf(WerfDto werf)
        {
            if (werf != null)
            {
                await Shell.Current.GoToAsync($"{nameof(NotitiePage)}?werfNavigatieId={werf.Id}&werfNaam={werf.Naam}");
            }
        }




    }
}
 
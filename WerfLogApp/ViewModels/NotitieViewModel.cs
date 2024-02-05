using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;

namespace WerfLogApp.ViewModels
{

    //[QueryProperty("Text", "text")]

    [QueryProperty("WerfNavigatieId", "werfNavigatieId")]
    [QueryProperty("WerfNaam", "werfNaam")]
    public partial class NotitieViewModel : ObservableObject 
    {
        private INotitieManager _notitieManager;

        //[ObservableProperty]
        //string text; //id van werf

        [ObservableProperty]
        string werfNavigatieId;

        [ObservableProperty]
        string werfNaam;

        [ObservableProperty]
        private string _nieuweNotitieText; //achterliggend wordt een public NieuweNotitieText aangemaakt. 


        //private field
        private ObservableCollection<NotitieDto> _notities;

        //public getter & setter 
        public ObservableCollection<NotitieDto> Notities
        {
            get => _notities;
            set
            {
                _notities = value;
                OnPropertyChanged(); //automatisch updaten UI/logica -> toegankelijk door BindableObject.
            }
        }


        public NotitieViewModel(INotitieManager notitieManager)
        {
            _notitieManager = notitieManager;

            Notities = new ObservableCollection<NotitieDto>()
            {
                new NotitieDto {Id= 1, WerfId = 6,Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
                new NotitieDto {Id= 2, WerfId = 7,Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
                new NotitieDto {Id= 3, WerfId = 8,Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
            };
        }

        public NotitieViewModel()
        {

        }


        //aanmaken 
        public Command VoegNotitieToeCommand => new Command(NotitieAanmaken);

        public void NotitieAanmaken()
        {
            if (!string.IsNullOrEmpty(NieuweNotitieText))
            {
                var notitieDto = new NotitieDto { Id= null, WerfId = int.Parse(WerfNavigatieId), Datum = DateTime.Now, Tekst = NieuweNotitieText }; //Text is Id
                var toegevoegdeNotitieDto = _notitieManager.AddNotitie(notitieDto);

                if(toegevoegdeNotitieDto != null )
                {
                    Notities.Insert(0, toegevoegdeNotitieDto);
                    NieuweNotitieText = string.Empty; // Clear the text after adding
                }
               
            }
        }

        //verwijderen

        //public Command DeleteNotitieCommand => new Command<Notitie>(NotitieVerwijderen);
        //public  void NotitieVerwijderen(Notitie notitie)
        //{
        //    Notities.Remove(notitie);
        //}

        public Command DeleteNotitieCommand => new Command<NotitieDto>(NotitieVerwijderen);
        public void NotitieVerwijderen(NotitieDto notitie)
        {
            Notities.Remove(notitie);
        }

        //navigatie vorige pagina
        public async void GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        public ICommand GoBackCommand => new Command(GoBack);

    
    }
}

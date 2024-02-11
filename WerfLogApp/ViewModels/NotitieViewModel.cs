using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;

namespace WerfLogApp.ViewModels
{
    
    [QueryProperty("WerfNavigatieId", "werfNavigatieId")]
    [QueryProperty("WerfNaam", "werfNaam")]
    public partial class NotitieViewModel : ObservableObject 
    {
        private INotitieManager _notitieManager;

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
            Notities = new ObservableCollection<NotitieDto>();
        }

        //COMMAND NOTITIE AANMAKEN
        public Command VoegNotitieToeCommand => new Command(async() => await NotitieAanmakenAsync());

        //METHODE NOTITIE AANMAKEN
        public async Task NotitieAanmakenAsync()
        {
            if (!string.IsNullOrEmpty(NieuweNotitieText))
            {
                var notitieDto = new NotitieDto
                {
                    Id = null,
                    WerfId = int.Parse(WerfNavigatieId),
                    Datum = DateTime.Now,
                    Tekst = NieuweNotitieText
                };

                try
                {
                    // Voeg de notitie toe en krijg de bijgewerkte NotitieDto terug (met de nieuwe ID)
                    var toegevoegdeNotitieDto = await _notitieManager.AddNotitieAsync(notitieDto);

                    if (toegevoegdeNotitieDto != null)
                    {
                        // De notitie is succesvol toegevoegd aan de database.
                        Notities.Insert(0, toegevoegdeNotitieDto);

                        // Maak het invoerveld leeg
                        NieuweNotitieText = string.Empty;
                    }
                    else
                    {
                        // Handel de situatie af als het toevoegen mislukt.
                        await ShowErrorMessage("Het toevoegen van de notitie is mislukt.");
                    }
                }
                catch (DatabaseException ex)
                {
                    // Specifieke afhandeling voor databasegerelateerde fouten.
                    await ShowErrorMessage($"Databasefout: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Algemene foutafhandeling.
                    await ShowErrorMessage($"Er is een fout opgetreden: {ex.Message}");
                }
            }
            else
            {
                await ShowErrorMessage("Notitietekst mag niet leeg zijn.");
            }
        }

        //METHODE ALLE NOTITIES VAN DESBETREFFENDE WERF LADEN, BIJ NAVIGATIE. (Zie code behind -> onappearing)
        public async Task LoadNotitiesAsync()
        {
            if (!string.IsNullOrEmpty(WerfNavigatieId))
            {
                try
                {
                    var werfId = int.Parse(WerfNavigatieId);
                    var notitiesVanWerf = await _notitieManager.GetAllNotities(werfId);

                    // Wis de bestaande notities en voeg de nieuwe notities toe
                    Notities.Clear();
                    foreach (var notitie in notitiesVanWerf)
                    {
                        Notities.Add(notitie);
                    }
                }
                catch (DatabaseException ex)
                {
                    // Specifieke afhandeling voor databasegerelateerde fouten.
                    await ShowErrorMessage($"Databasefout: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Algemene foutafhandeling.
                    await ShowErrorMessage($"Er is een fout opgetreden: {ex.Message}");
                }
            }
            else
            {
                await ShowErrorMessage("WerfNavigatieId is niet toegewezen.");
            }
        }

        //COMMAND NOTITIE VERWIJDEREN
        public Command DeleteNotitieCommand => new Command<NotitieDto>(NotitieVerwijderen);

        //METHODE NOTITIE VERWIJDEREN
        public void NotitieVerwijderen(NotitieDto notitie)
        {
            Notities.Remove(notitie);
        }

        ////COMMAND NAVIGATIE NAAR MAINPAGE
        //public Command GoBackCommand => new Command(GoBack);
        ////METHODE NAVIGATIE NAAR MAINPAGE
        //public async void GoBack()
        //{
        //    try
        //    {
        //        await Shell.Current.GoToAsync("..");
        //    }
        //    catch (Exception ex)
        //    {
        //        await ShowErrorMessage("Fout bij navigeren naar mainpage.");
        //    } 
        //}
 
        //ERROR POPUP IN VIEW
        private async Task ShowErrorMessage(string message)
        {
            await App.Current.MainPage.DisplayAlert("Fout", message, "OK");
        }
    }
}

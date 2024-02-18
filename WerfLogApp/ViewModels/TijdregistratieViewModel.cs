using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;

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
            try
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

                if (tijdregistraties.Count == 0 || tijdregistraties is null)
                {
                    await ShowErrorMessage($"Geen opgeslagen tijdregistraties voor maand en jaar.");
                }
                else
                {
                    // Converteer de lijst naar een ObservableCollection
                    Tijdregistraties = new ObservableCollection<TijdregistratieDto>(tijdregistraties);
                    var totaalUren = await _tijdregistratieManager.GetTotaalTijdRegistratiesMaand(maand, jaar);
                    TotaalUren = totaalUren;
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


        public Command<TijdregistratieDto> EditCommand => new Command<TijdregistratieDto>(async (tijdregistratieDto) => await EditTijdregistratie(tijdregistratieDto));


        private async Task EditTijdregistratie(TijdregistratieDto tijdregistratie)
        {
            try
            {
                if (tijdregistratie != null)
                {
                    try
                    {
                        await Shell.Current.GoToAsync($"{nameof(TijdregistratieEditPage)}?tijdregistratieId={tijdregistratie.Id}");
                        // Voeg eventuele andere parameters toe die je nodig hebt
                    }
                    catch (Exception ex)
                    {
                        // Algemene foutafhandeling.
                        //await ShowErrorMessage($"Er is een fout opgetreden: {ex.Message}");
                    }
                }
                else
                {
                    //await ShowErrorMessage("Fout bij het navigeren naar de edit pagina.");
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

        //ERROR POPUP IN VIEW
        private async Task ShowErrorMessage(string message)
        {
            await App.Current.MainPage.DisplayAlert("Fout", message, "OK");
        }
    }
}






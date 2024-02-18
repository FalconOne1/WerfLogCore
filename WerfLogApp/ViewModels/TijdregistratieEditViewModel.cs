using CommunityToolkit.Mvvm.ComponentModel;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;


namespace WerfLogApp.ViewModels
{

    [QueryProperty("TijdregistratieId", "tijdregistratieId")]
    public partial class TijdregistratieEditViewModel : ObservableObject
    {
        [ObservableProperty]
        private TijdregistratieDto _tijdregistratie;

        [ObservableProperty]
        private int _tijdregistratieId;

        private ITijdregistratieManager _tijdregistratieManager;

        public TijdregistratieEditViewModel(ITijdregistratieManager tijdregistratieManager)
        {
            _tijdregistratieManager = tijdregistratieManager;
        }

        public async Task LoadTijdregistratie()
        {
            try
            {
                if (TijdregistratieId != 0)
                {
                    Tijdregistratie = await _tijdregistratieManager.GetTijdregistratieById(TijdregistratieId);
                }
                else
                {
                    await ShowErrorMessage("Probleem bij het laden van de tijdregistratie");
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

        public Command SaveCommand => new Command(async () => await SaveTijdregistratie());
        public async Task SaveTijdregistratie()
        {
            try
            {
                await _tijdregistratieManager.UpdateTijdregistratie(TijdregistratieId, Tijdregistratie);
                await App.Current.MainPage.DisplayAlert("Gelukt", "Aangepaste tijdregistratie opgeslagen", "OK");


                await App.Current.MainPage.Navigation.PopAsync();
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

        public Command DeleteCommand => new Command(async () => await DeleteTijdregistratie());

        public async Task DeleteTijdregistratie()
        {
            try
            {
                await _tijdregistratieManager.DeleteTijdregistratieAsync(Tijdregistratie);

                await App.Current.MainPage.DisplayAlert("Gelukt", "Tijdregistratie werd verwijderd.", "OK");

                await App.Current.MainPage.Navigation.PopAsync();

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

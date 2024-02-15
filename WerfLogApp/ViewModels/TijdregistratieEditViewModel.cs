using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;
using WerfLogDal.Models;


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

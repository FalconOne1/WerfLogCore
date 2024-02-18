
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;

namespace WerfLogApp.ViewModels
{
    public partial class WerfViewModel : ObservableObject
    {
        public event Action<WerfDto> OnWerfToegevoegd;

        private IWerfManager _werfManager;
        private ITijdregistratieManager _tijdregistratieManager;

        private bool _isDataGeladen = false;
        private int _actieveTijdRegistratieId;
        private bool _actieveWerf = false;


        private string _actieveWerfNaam;

        [ObservableProperty]
        private string _timerLabelText = "Afwezig";

        [ObservableProperty]
        private string _nieuweWerfText; //achterliggend wordt een public NieuweWerfText aangemaakt. 

        [ObservableProperty]
        private WerfDto _geselecteerdeWerf;

        [ObservableProperty]
        private ObservableCollection<WerfDto> _werven;


        public WerfViewModel(IWerfManager werfManager, ITijdregistratieManager tijdregistratieManager)
        {
            _werfManager = werfManager;
            _tijdregistratieManager = tijdregistratieManager;
            Werven = new ObservableCollection<WerfDto>();
        }


        //COMMAND AANMAKEN WERF
        public Command VoegWerfToeCommand => new Command(async () => await WerfAanmakenAsync());

        //METHODE AANMAKEN WERF
        public async Task WerfAanmakenAsync()
        {
            if (!string.IsNullOrEmpty(NieuweWerfText))
            {
                var werfDto = new WerfDto { Naam = NieuweWerfText, Id = null, IsActief = 1 };

                try
                {
                    // Voeg de Werf toe en krijg de bijgewerkte WerfDto terug (met de nieuwe ID)
                    var toegevoegdeWerfDto = await _werfManager.AddWerfAsync(werfDto);

                    if (toegevoegdeWerfDto != null)
                    {
                        // De werf is succesvol toegevoegd aan de database.
                        Werven.Insert(0, toegevoegdeWerfDto);

                        // Maak het invoerveld leeg
                        NieuweWerfText = string.Empty;

                        OnWerfToegevoegd?.Invoke(toegevoegdeWerfDto);


                    }
                    else
                    {
                        //Handel de situatie af als het toevoegen mislukt.
                        await ShowErrorMessage("Het toevoegen van de werf is mislukt.");
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
                await ShowErrorMessage("Werfnaam mag niet leeg zijn.");
            }
        }


        //COMMAND VERWIJDEREN WERF (command vervangt eventhandler)
        //Linkt met databinding in xaml mainpage.
        public Command<WerfDto> VerwijderCommand => new Command<WerfDto>(async (werf) =>
        {
            var bevestigVerwijderen = await App.Current.MainPage.DisplayAlert("Bevestigen", "Weet u zeker dat u deze werf wilt verwijderen?", "Ja", "Nee");

            if (bevestigVerwijderen)
            {
                // Voer de verwijderactie uit
                await WerfVerwijderenAsync(werf);
            }
        });

        //METHODE VERWIJDEREN WERF
        public async Task WerfVerwijderenAsync(WerfDto werf)
        {
            try
            {
                await _werfManager.DeleteWerfAsync(werf);



                Werven.Remove(werf);



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


        //COMMAND NAVIGATIE NAAR NOTITIES VAN WERF
        public Command<WerfDto> NotitieCommand => new Command<WerfDto>(async (werfDto) => await TapWerf(werfDto));

        //METHODE NAVIGATIE
        public async Task TapWerf(WerfDto werf)
        {
            if (werf != null)
            {
                try
                {
                    await Shell.Current.GoToAsync($"{nameof(NotitiePage)}?werfNavigatieId={werf.Id}&werfNaam={werf.Naam}");
                }

                catch (Exception ex)
                {
                    // Algemene foutafhandeling.
                    await ShowErrorMessage($"Er is een fout opgetreden: {ex.Message}");
                }
            }
            else
            {
                await ShowErrorMessage($"Fout bij het navigeren naar notities.");
            }
        }


        //ERROR POPUP IN VIEW
        private async Task ShowErrorMessage(string message)
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Fout", message, "OK");
            }
            catch (Exception)
            {
                throw;
            }
        }


        //COMMAND TIJDSREGISTRATIE START
        public Command GroeneKnopCommand => new Command(async () =>
        {
            try
            {
                if (GeselecteerdeWerf != null)
                {
                    // Voer de logica uit voor het opslaan van de datum en tijd
                    await SlaDateTimeOp(GeselecteerdeWerf);
                }
                else
                {
                    await ShowErrorMessage($"Selecteer een werf.");
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

        });


        // Methode om de datum en tijd op te slaan
        private async Task SlaDateTimeOp(WerfDto werf)
        {
            try
            {
                if (!_actieveWerf)
                {
                    _actieveTijdRegistratieId = await _tijdregistratieManager.VoegStarttijdWerfToe(werf);
                    _actieveWerf = true;
                    TimerLabelText = "Aanwezig";
                }
                else
                {
                    await ShowErrorMessage($"Laatste werf is nog actief, gelieve deze eerst af te sluiten.");
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

        //COMMAND TIJDSREGISTRATIE STOP
        public Command RodeKnopCommand => new Command(async () =>
        {
            try
            {
                if (_actieveWerf)
                {
                    // Voer de logica uit voor het opslaan van de datum en tijd
                    await SlaStopDateTimeOp(_actieveTijdRegistratieId);
                }
                else
                {
                    // Toon foutmelding
                    await ShowErrorMessage($"Geen werf actief.");
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
        });

        // Methode om de datum en tijd op te slaan
        private async Task SlaStopDateTimeOp(int actieveTijdId)
        {
            try
            {
                if (actieveTijdId != 0)
                {
                    await _tijdregistratieManager.VoegStoptijdWerfToe(actieveTijdId);
                    _actieveWerf = false;
                    TimerLabelText = "Afwezig";
                }
                else
                {
                    await ShowErrorMessage($"Er is een fout opgetreden bij het opslaan.");
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

        private async Task IsErNogEenLopendeTijdRegistratieVanWerf()
        {
            try
            {
                // Haal de actieve tijdregistratie op (waar StopTijd nog NULL is)
                var actieveTijdregistratie = await _tijdregistratieManager.GetActieveTijdregistratieId();

                //Indien geen actieve tijdregistratie meer, doe niets.
                if (actieveTijdregistratie == null)
                {
                    return;
                }

                //Bij opstarten, toon aanwezigheid op werf en biedt de mogelijkheid om werf te beeindigen.
                _actieveTijdRegistratieId = (int)actieveTijdregistratie.Id;
                _actieveWerf = true;
                TimerLabelText = "Aanwezig";

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

        public async Task HaalAlleWervenOpAsync()
        {
            try
            {
                if (!_isDataGeladen)
                {
                    var wervenDto = await _werfManager.HaalAlleWervenOp();

                    foreach (var werfDto in wervenDto)
                    {
                        Werven.Add(werfDto);
                    }

                    _isDataGeladen = true;

                    if (Werven.Count > 0)
                    {
                        await IsErNogEenLopendeTijdRegistratieVanWerf();
                    }
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
        public Command TijdRegistratieCommand => new Command(async () => await TapTijdRegistratie());


        // Aangepaste methode zonder parameter
        public async Task TapTijdRegistratie()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(TijdregistratiePage)}");
            }
            catch (Exception ex)
            {
                await ShowErrorMessage($"Er is een fout opgetreden bij het navigeren naar tijdregistraties.");
            }
        }

    }

}


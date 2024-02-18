using WerfLogApp.ViewModels;
using WerfLogBl.DTOS;
namespace WerfLogApp
{
    public partial class MainPage : ContentPage //partial class ->  [ObservableProperty] in Modelview. (MVVM: eventhandler in codebehind vermijden en vervangen door command in viewmodel.)

    {
        private WerfViewModel _viewModel;

        public MainPage(WerfViewModel werfViewModel) //DI
        {
            InitializeComponent();
            _viewModel = werfViewModel;
            this.BindingContext = _viewModel;


            // Voeg Touch-events toe aan de toevoegenButton
            ToevoegenButton.Pressed += OnButtonPressed;
            ToevoegenButton.Released += OnButtonReleased;

        }


        private void ViewModel_OnWerfToegevoegd(WerfDto werf)
        {
            // Veronderstelt dat je CollectionView de naam WervenCollectionView heeft
            WervenCollectionView.ScrollTo(werf, position: ScrollToPosition.Start, animate: true);


        }

        /* "Klikgevoel" Werf toevoeg button */
        private void OnButtonPressed(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                var button = (Button)sender;
                button.ScaleTo(0.95, 50, Easing.CubicOut);  // Verklein de knop iets
                button.BackgroundColor = Colors.LightSlateGray; // Verander de achtergrondkleur

            }
        }
        private void OnButtonReleased(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                var button = (Button)sender;
                button.ScaleTo(1, 50, Easing.CubicIn);  // Breng de knop terug naar normale grootte
                button.BackgroundColor = Colors.DarkSlateGray; // Herstel de oorspronkelijke achtergrondkleur

            }
        }

        /* "Klikgevoel" notitie navigatie button */
        //private async void NotitieButton_Clicked(object?sender, EventArgs e)
        //{
        //    if (sender is not null)
        //    {
        //        var layout = (View)sender;
        //        // Verklein de layout om het indrukken te simuleren
        //        await layout.ScaleTo(0.85, 50, Easing.CubicOut);
        //        // Vergroot de layout om terug te keren naar de oorspronkelijke grootte
        //        await layout.ScaleTo(1, 50, Easing.CubicIn);
        //    }
        //}

        private async void OnChronoTapped(object sender, EventArgs e)
        {
            if (sender is View view)
            {
                // Simuleer het indrukken door de afbeelding te verkleinen
                await view.ScaleTo(0.70, 80, Easing.CubicOut);

                // Breng de afbeelding terug naar de oorspronkelijke grootte
                await view.ScaleTo(1, 50, Easing.CubicIn);

                // Voer hier eventuele extra acties uit die moeten gebeuren na de "tap"
            }
        }


        protected override async void OnAppearing()
        {

            base.OnAppearing();
            await _viewModel.HaalAlleWervenOpAsync();

            _viewModel.OnWerfToegevoegd += ViewModel_OnWerfToegevoegd;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnWerfToegevoegd -= ViewModel_OnWerfToegevoegd;
        }
    }

}

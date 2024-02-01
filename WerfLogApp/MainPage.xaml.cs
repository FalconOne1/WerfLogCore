
using WerfLogApp.ViewModels;

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

        /* "Klikgevoel" Werf toevoeg button */
        private void OnButtonPressed(object?sender, EventArgs e)
        {
            if (sender is not null)
            {
                var button = (Button)sender;
                button.ScaleTo(0.95, 50, Easing.CubicOut);  // Verklein de knop iets
                button.BackgroundColor = Colors.LightSlateGray; // Verander de achtergrondkleur

            }
        }
        private void OnButtonReleased(object?sender, EventArgs e)
        {
            if (sender is not null)
            {
                var button = (Button)sender;
                button.ScaleTo(1, 50, Easing.CubicIn);  // Breng de knop terug naar normale grootte
                button.BackgroundColor = Colors.DarkSlateGray; // Herstel de oorspronkelijke achtergrondkleur
            }
        }

        /* "Klikgevoel" notitie navigatie button */
        private async void NotitieButton_Clicked(object?sender, EventArgs e)
        {
            if (sender is not null)
            {
                var layout = (View)sender;
                // Verklein de layout om het indrukken te simuleren
                await layout.ScaleTo(0.85, 50, Easing.CubicOut);
                // Vergroot de layout om terug te keren naar de oorspronkelijke grootte
                await layout.ScaleTo(1, 50, Easing.CubicIn);
            }
        }

        
    }


}

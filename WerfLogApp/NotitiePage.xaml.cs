using WerfLogApp.ViewModels;
using WerfLogBl.DTOS;

namespace WerfLogApp;

public partial class NotitiePage : ContentPage
{

    private NotitieViewModel _viewModel;
    public NotitiePage(NotitieViewModel notitieViewModel)
    {
        InitializeComponent();
        BindingContext = notitieViewModel;
        _viewModel = notitieViewModel;

        NotitieToevoegenButton.Pressed += OnButtonPressed;
        NotitieToevoegenButton.Released += OnButtonReleased;
    }

    private void ViewModel_OnNotitieToegevoegd(NotitieDto notitie)
    {
        // Veronderstelt dat je CollectionView de naam WervenCollectionView heeft
        NotitiesCollectionView.ScrollTo(notitie, position: ScrollToPosition.Start, animate: true);


    }

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadNotitiesAsync();
        _viewModel.OnNotitieToegevoegd += ViewModel_OnNotitieToegevoegd;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.OnNotitieToegevoegd -= ViewModel_OnNotitieToegevoegd;
    }
}
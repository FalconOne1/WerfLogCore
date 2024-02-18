using WerfLogApp.ViewModels;

namespace WerfLogApp;


public partial class TijdregistratieEditPage : ContentPage
{
    private TijdregistratieEditViewModel _viewModel;

    public TijdregistratieEditPage(TijdregistratieEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadTijdregistratie();
    }
}









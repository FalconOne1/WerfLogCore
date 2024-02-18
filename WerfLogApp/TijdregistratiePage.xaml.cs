using WerfLogApp.ViewModels;

namespace WerfLogApp;

public partial class TijdregistratiePage : ContentPage
{
    private TijdregistratieViewModel _viewModel;
    public TijdregistratiePage(TijdregistratieViewModel tijdregistratieViewModel)
    {
        InitializeComponent();
        BindingContext = tijdregistratieViewModel;
        _viewModel = tijdregistratieViewModel;
    }
}



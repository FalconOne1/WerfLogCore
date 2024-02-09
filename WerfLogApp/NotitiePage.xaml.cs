using WerfLogApp.ViewModels;

namespace WerfLogApp;

public partial class NotitiePage : ContentPage
{

    private NotitieViewModel _viewModel;
    public NotitiePage(NotitieViewModel notitieViewModel)
	{
        InitializeComponent();
        BindingContext = notitieViewModel;
        _viewModel = notitieViewModel;
     
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadNotitiesAsync();
    }
}
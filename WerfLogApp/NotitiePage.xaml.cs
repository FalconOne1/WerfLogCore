using WerfLogApp.ViewModels;

namespace WerfLogApp;

public partial class NotitiePage : ContentPage
{
	public NotitiePage(NotitieViewModel notitieViewModel)
	{
        InitializeComponent();
        BindingContext = notitieViewModel;
     
    }
}
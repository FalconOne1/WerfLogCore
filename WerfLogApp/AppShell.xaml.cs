namespace WerfLogApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NotitiePage), typeof(NotitiePage));
            Routing.RegisterRoute(nameof(TijdregistratiePage), typeof(TijdregistratiePage));
            Routing.RegisterRoute(nameof(TijdregistratieEditPage), typeof(TijdregistratieEditPage));


        }
    }
}

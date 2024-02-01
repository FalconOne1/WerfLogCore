namespace WerfLogApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NotitiePage), typeof(NotitiePage));
        }
    }
}

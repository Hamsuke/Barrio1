using Barrio1.Views;

namespace Barrio1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterForRoute<AddVentaPage>();
            RegisterForRoute<UpdateVentaPage>();
            RegisterForRoute<LoginView>();
        }

        protected void RegisterForRoute<T>()
        {
            Routing.RegisterRoute(typeof(T).Name, typeof(T));
        }
    }
}

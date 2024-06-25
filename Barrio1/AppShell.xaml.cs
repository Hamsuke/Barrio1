using Barrio1.ViewModels;
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
            RegisterForRoute<InventarioPage>();
            RegisterForRoute<DetallesPage>();
        }

        protected void RegisterForRoute<T>()
        {
            Routing.RegisterRoute(typeof(T).Name, typeof(T));
        }
    }
}

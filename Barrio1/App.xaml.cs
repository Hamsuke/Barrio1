using Barrio1.Services;
using Barrio1.Views;

namespace Barrio1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}

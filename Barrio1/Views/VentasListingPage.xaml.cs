using Barrio1.ViewModels;

namespace Barrio1.Views;

public partial class VentasListingPage : ContentPage
{
    public VentasListingPage(VentasListingViewModel ventasListingViewModel)
    {
        InitializeComponent();
        BindingContext = ventasListingViewModel;
    }
}
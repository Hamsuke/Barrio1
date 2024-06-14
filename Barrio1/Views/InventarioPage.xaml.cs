using Barrio1.ViewModels;
namespace Barrio1.Views;

public partial class InventarioPage : ContentPage
{
    public InventarioPage(InventarioViewModel inventarioViewModel)
    {
        InitializeComponent();
        BindingContext = inventarioViewModel;
    }
}
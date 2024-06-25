using Barrio1.ViewModels;
namespace Barrio1.Views;

public partial class UpdateVentaPage : ContentPage
{
    public UpdateVentaPage(UpdateVentaViewModel updateVentaViewModel)
    {
        InitializeComponent();
        BindingContext = updateVentaViewModel;
    }
}
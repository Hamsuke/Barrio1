using Barrio1.ViewModels;
namespace Barrio1.Views;

public partial class AddVentaPage : ContentPage
{
    public AddVentaPage(AddVentaViewModel addVentaViewModel)
    {
        InitializeComponent();
        BindingContext = addVentaViewModel;
    }
}
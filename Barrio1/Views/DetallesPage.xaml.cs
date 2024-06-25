using Barrio1.ViewModels;

namespace Barrio1.Views;

public partial class DetallesPage : ContentPage
{
    public DetallesPage(DetallesViewModel detallesViewModel)
    {
        InitializeComponent();
        BindingContext = detallesViewModel;
    }
}
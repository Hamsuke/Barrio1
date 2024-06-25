using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Barrio1.ViewModels;

[QueryProperty(nameof(Venta), "VentaObject")]
public partial class UpdateVentaViewModel : ObservableObject
{
    private readonly IDataServices _dataService;

    [ObservableProperty]
    private Ventas _venta;

    [ObservableProperty]
    private float _tmp;

    public UpdateVentaViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    private async Task UpdateVenta()
    {
        if (Tmp > 0)
        {
            Venta.pago = Tmp;
            await _dataService.UpdateVenta(Venta);

            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "0 no es un valor valido!", "OK");
        }
    }
}
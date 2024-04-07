using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Barrio1.ViewModels;

public partial class AddVentaViewModel : ObservableObject
{
    private readonly IDataServices _dataService;

    [ObservableProperty]
    private string _ventaCliente;
    [ObservableProperty]
    private string _ventaVendedor;
    [ObservableProperty]
    private DateTime _ventaFechaC;
    [ObservableProperty]
    private DateTime _ventaFechaP;
    [ObservableProperty]
    private bool _ventaEstado;
    [ObservableProperty]
    private float _ventaPago;
    [ObservableProperty]
    private float _ventaCosto;

    public AddVentaViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    private async Task AddVenta()
    {
        try
        {
            if (!string.IsNullOrEmpty(VentaCliente))
            {
                if ((VentaCosto - VentaPago) > 0)
                {
                    VentaEstado = false;
                    VentaFechaP = DateTime.Now;
                }
                Ventas venta = new()
                {
                    name = VentaCliente,
                    vendor = VentaVendedor,
                    dateC = DateTime.Now,
                    dateP = VentaFechaP,
                    est = VentaEstado,
                    pago = VentaPago,
                    costo = VentaCosto
                };
                Salidas salidas = new()
                {
                };

                await _dataService.CreateVenta(venta);
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No hay Cliente!", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
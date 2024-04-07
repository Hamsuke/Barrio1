using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Barrio1.ViewModels;

public partial class VentasListingViewModel : ObservableObject
{
    private readonly IDataServices _dataService;

    public ObservableCollection<Ventas> Ventas { get; set; } = new();
    public VentasListingViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    public async Task GetVentas()
    {
        Ventas.Clear();

        try
        {
            var ventas = await _dataService.GetVentas();

            if (ventas.Any())
            {
                foreach (var venta in ventas)
                {
                    Ventas.Add(venta);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task AddVenta() => await Shell.Current.GoToAsync("AddVentaPage");

    [RelayCommand]
    private async Task UpdateVenta(Ventas venta) => await Shell.Current.GoToAsync("UpdateVentaPage", new Dictionary<string, object>
    {
        {"VentaObject", venta }
    });
}
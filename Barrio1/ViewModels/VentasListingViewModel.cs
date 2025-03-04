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
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }
    public DateTime inicioMes { get; set; }

    [ObservableProperty]
    private DateTime _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public VentasListingViewModel(IDataServices dataService)
    {
        _dataService = dataService;
        MinDate = new DateTime(2024, 12, 01);
        MaxDate = DateTime.Now;
        inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

    }

    [RelayCommand]
    public async Task GetVentas()
    {
        Ventas.Clear();
        try
        {
            // Obtener todas las ventas del mes
            var ventas = await _dataService.GetVentas(inicioMes, SelectedDate);

            if (ventas.Any())
            {
                // Filtrar las ventas del mes actual


                var ventasDelMes = ventas.ToList();

                foreach (var venta in ventasDelMes)
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
    public async Task FiltrarVentasAsync()
    {
        DateTime inicioDeMes = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);

        // Filtrar ventas por mes y año
        var ventas = await _dataService.GetVentas(inicioDeMes, SelectedDate);

        // Limpiar y actualizar la lista visible
        Ventas.Clear();
        foreach (var venta in ventas)
        {
            Ventas.Add(venta);
        }
    }

    [RelayCommand]
    private async Task AddVenta() => await Shell.Current.GoToAsync("AddVentaPage");

    [RelayCommand]
    private async Task GetInventario() => await Shell.Current.GoToAsync("InventarioPage");

    [RelayCommand]
    private async Task UpdateVenta(Ventas venta) => await Shell.Current.GoToAsync("UpdateVentaPage", new Dictionary<string, object>
    {
        {"VentaObject", venta }
    });

    [RelayCommand]
    private async Task DetallesNota(Ventas venta) => await Shell.Current.GoToAsync("DetallesPage", new Dictionary<string, object>
    {
        {"VentaObject", venta }
    });
}

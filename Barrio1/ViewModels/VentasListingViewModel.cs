using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Barrio1.ViewModels;

public partial class VentasListingViewModel : ObservableObject
{
    private readonly IDataServices _dataService;

    public ObservableCollection<Ventas> ventas { get; set; } = new();
    public DateTime minDate { get; set; }
    public DateTime maxDate { get; set; }
    public DateTime inicioMes { get; set; }

    [ObservableProperty]
    private DateTime _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    [ObservableProperty] 
    private int _notaABuscar;

    public VentasListingViewModel(IDataServices dataService)
    {
        _dataService = dataService;
        minDate = new DateTime(2024, 12, 01);
        maxDate = DateTime.Now;
        inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }

    [RelayCommand]
    public async Task BuscarNotas()
    {

        try
        {
            var nota = await _dataService.GetNota(NotaABuscar);
            if (nota == null || !nota.Any())
            {
                await Shell.Current.DisplayAlert("Error", "No se encontraron notas con ese numero", "OK");
            }
            else
            {
                ventas.Clear();
                ventas.Add(nota.FirstOrDefault());
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    public async Task GetVentas()
    {
        ventas.Clear();
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
                    this.ventas.Add(venta);
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
        this.ventas.Clear();
        foreach (var venta in ventas)
        {
            this.ventas.Add(venta);
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

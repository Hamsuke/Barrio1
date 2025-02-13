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

    private List<Ventas> _ventasOriginal = new();

    public List<string> Meses { get; } = new List<string>
    {
        "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
        "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    };

    public List<int> Anos { get; } = Enumerable.Range(2024, DateTime.Now.Year - 2024 + 1).ToList();

    [ObservableProperty]
    private string _mesSeleccionado;

    [ObservableProperty]
    private int _anoSeleccionado = DateTime.Now.Year;

    public VentasListingViewModel(IDataServices dataService)
    {
        _dataService = dataService;
        _mesSeleccionado = Meses[DateTime.Now.Month - 1];
    }

    [RelayCommand]
    public async Task GetVentas()
    {
        Ventas.Clear();
        _ventasOriginal.Clear();

        try
        {
            // Obtener todas las ventas
            var ventas = await _dataService.GetVentas();

            if (ventas.Any())
            {
                // Filtrar las ventas del mes actual
                var mesActual = DateTime.Now.Month;
                var anoActual = DateTime.Now.Year;

                var ventasDelMes = ventas.Where(v => v.dateC.Month == mesActual && v.dateC.Year == anoActual).ToList();

                // Guardar lista original (para filtros posteriores) y agregar al observable
                _ventasOriginal.AddRange(ventasDelMes);

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
    public void FiltrarVentas()
    {

        // Obtener el número del mes seleccionado
        int mes = Meses.IndexOf(MesSeleccionado) + 1;

        // Filtrar ventas por mes y año
        var ventasFiltradas = _ventasOriginal
            .Where(v => v.dateC.Month == mes && v.dateC.Year == AnoSeleccionado)
            .ToList();

        // Limpiar y actualizar la lista visible
        Ventas.Clear();
        foreach (var venta in ventasFiltradas)
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

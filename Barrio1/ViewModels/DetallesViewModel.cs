using System.Collections.ObjectModel;
using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Barrio1.ViewModels;

[QueryProperty(nameof(Venta), "VentaObject")]

public partial class DetallesViewModel : ObservableObject
{
    private readonly IDataServices _dataServices;

    public ObservableCollection<SalidasBotella> SalidasBotella { get; set; } = new();
    public ObservableCollection<SalidasBarril> SalidasBarril { get; set; } = new();

    [ObservableProperty]
    private Ventas _venta;
    [ObservableProperty]
    private DateOnly _fecha1;
    [ObservableProperty]
    private DateOnly _fecha2;

    public DetallesViewModel(IDataServices dataService)
    {
        _dataServices = dataService;
    }

    [RelayCommand]
    public async Task GetNota()
    {
        Fecha1 = new DateOnly(Venta.dateC.Year, Venta.dateC.Month, Venta.dateC.Day);
        Fecha2 = new DateOnly(Venta.dateP.Year, Venta.dateP.Month, Venta.dateP.Day);
        SalidasBarril.Clear();
        SalidasBotella.Clear();

        if (Venta == null)
        {
            Console.WriteLine("Venta is not set. Exiting command.");
            return;
        }

        try
        {
            // Get Botellas data
            var botellas = await _dataServices.GetBotellasNota(Venta.id);
            if (botellas == null || !botellas.Any())
            {
                Console.WriteLine("No botellas data found for the given Venta ID.");
            }
            else
            {
                var lista = botellas.ToList();
                // Add Botellas to the collection
                foreach (var item in lista)
                {
                    SalidasBotella.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving botellas data: {ex.Message}");
            // Optional: Log the exception or notify the user about the failure
        }

        try
        {
            // Get Barriles data
            var barriles = await _dataServices.GetBarrilesNota(Venta.id);
            if (barriles == null || !barriles.Any())
            {
                Console.WriteLine("No barriles data found for the given Venta ID.");
            }
            else
            {
                var lista = barriles.ToList();
                // Add Barriles to the collection
                foreach (var item in lista)
                {
                    SalidasBarril.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving barriles data: {ex.Message}");
            // Optional: Log the exception or notify the user about the failure
        }

        // Safely update properties after null check (if needed)
    }

}


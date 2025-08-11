using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Barrio1.ViewModels;

public partial class AddVentaViewModel : ObservableObject
{

    private readonly IDataServices _dataService;
    //Nota de venta
    [ObservableProperty]
    private int _nota;

    //Datos de venta
    [ObservableProperty]
    private string _ventaCliente;
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
    [ObservableProperty]
    private string _metodo;

    public AddVentaViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }
    public ObservableCollection<Botellas> BotellasDisponibles { get; set; } = new();
    public ObservableCollection<Barriles> BarrilesDisponibles { get; set; } = new();
    public ObservableCollection<SalidasBotella> SalidasBotellas { get; set; } = new();
    public ObservableCollection<SalidasBarril> SalidasBarriles { get; set; } = new();

    private static async Task RetryAsync(Func<Task> action, int maxRetries = 3, int delayMs = 500)
    {
        int retries = 0;
        while (true)
        {
            try
            {
                await action();
                break; // Éxito → salimos del bucle
            }
            catch (Exception ex)
            {
                retries++;
                if (retries > maxRetries)
                    throw; // Si se superan los intentos → re-lanzar error

                await Task.Delay(delayMs);
            }
        }
    }


    [RelayCommand]
    public async Task getBotellas()
    {
        BotellasDisponibles.Clear();
        SalidasBotellas.Clear();

        var botellas = await _dataService.GetBotellasDisp();

        foreach (var item in botellas)
        {
            BotellasDisponibles.Add(item);
            SalidasBotellas.Add(new SalidasBotella
            {
                id = 0,
                botella = item.nombreBo,
                cant = 0
            });
        }
    }

    [RelayCommand]
    public async Task getBarriles()
    {
        BarrilesDisponibles.Clear();
        SalidasBarriles.Clear();

        var barriles = await _dataService.GetBarrilesDisp();

        foreach (var item in barriles.AsEnumerable().Reverse())
        {
            BarrilesDisponibles.Add(item);
            SalidasBarriles.Add(new SalidasBarril
            {
                id = 0,
                barril = item.nombreBa,
                cant = 0
            });
        }
    }

    [RelayCommand]
    private async Task AddVenta2()
    {
        try
        {
            if (string.IsNullOrEmpty(VentaCliente))
            {
                await Shell.Current.DisplayAlert("Error", "No hay Cliente!", "OK");
                return;
            }

            if (VentaCosto == VentaPago)
            {
                VentaEstado = true;
                VentaFechaP = DateTime.Now;
            }

            var venta = new Ventas
            {
                id = Nota,
                name = VentaCliente,
                vendor = _dataService.GetUsername(),
                dateC = DateTime.Now,
                dateP = VentaFechaP,
                est = VentaEstado,
                pago = VentaPago,
                costo = VentaCosto,
                metodo = Metodo
            };

            await RetryAsync(() => _dataService.CreateVenta(venta));


            var salidasBotellasList = SalidasBotellas
                .Where(item => item.cant > 0)
                .Select(item => new SalidasBotella
                {
                    id = Nota,
                    botella = item.botella,
                    cant = item.cant
                })
                .ToList();

            if (salidasBotellasList.Any())
            {
                await RetryAsync(() => _dataService.BulkCreateSalidaBotella(salidasBotellasList));
                foreach (var salida in salidasBotellasList)
                    await RetryAsync(() => _dataService.updateBotella(salida));
            }

            var salidasBarrilesList = SalidasBarriles
                .Where(item => item.cant > 0)
                .Select(item => new SalidasBarril
                {
                    id = Nota,
                    barril = item.barril,
                    cant = item.cant
                })
                .ToList();

            if (salidasBarrilesList.Any())
            {
                await RetryAsync(() => _dataService.BulkCreateSalidaBarril(salidasBarrilesList));
                foreach (var salida in salidasBarrilesList)
                    await RetryAsync(() => _dataService.updateBarril(salida));
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Error tras varios intentos: {ex.Message}", "OK");
        }
    }

}

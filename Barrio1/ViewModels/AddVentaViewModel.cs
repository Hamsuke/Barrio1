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

    [RelayCommand]
    public async Task getBotellas()
    {
        BotellasDisponibles.Clear();
        SalidasBotellas.Clear();
        // Cargar botellas disponibles
        var botellas = await _dataService.GetBotellasDisp();
        
        if (botellas != null)
        {
            var botellasDisponibles = botellas.Where(b => b.cantidadBo > 0).Reverse().ToList();
            foreach (var item in botellasDisponibles)
            {
                BotellasDisponibles.Add(item);
                var nuevasalida = new SalidasBotella()
                {
                    id = 0,
                    botella = item.nombreBo,
                    cant = 0
                };

                SalidasBotellas.Add(nuevasalida);
            }
        }
    }

    [RelayCommand]
    public async Task getBarriles()
    {
        
        BarrilesDisponibles.Clear();
        SalidasBarriles.Clear();
        // Cargar barriles disponibles
        var barriles = await _dataService.GetBarriles();
        if (barriles != null)
        {
            var barrilesDisponibles = barriles.Where(b => b.cantidadBa > 0).Reverse().ToList();
            
            foreach (var item in barrilesDisponibles)
            {
                BarrilesDisponibles.Add(item);
                var nuevasalida = new SalidasBarril()
                {
                    id = 0,
                    barril = item.nombreBa,
                    cant = 0
                };
                SalidasBarriles.Add(nuevasalida);
            }
        }
    }

    [RelayCommand]
    private async Task AddVenta()
    {

        try
        {
            if (!string.IsNullOrEmpty(VentaCliente))
            {

                if (VentaCosto == VentaPago)
                {
                    VentaEstado = true;
                    VentaFechaP = DateTime.Now;
                }
                Ventas venta = new()
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

                SalidasBotella salidaDeBotella = new();
                foreach (var item in SalidasBotellas)
                {
                    if(item.cant > 0)
                    {
                        salidaDeBotella.id = Nota;
                        salidaDeBotella.botella = item.botella;
                        salidaDeBotella.cant = item.cant;
                        await _dataService.CreateSalidaBotella(salidaDeBotella);
                        await _dataService.updateBotella(salidaDeBotella);
                    }          
                }

                SalidasBarril salidaDeBarril = new();
                foreach (var item in SalidasBarriles)
                {
                    if(item.cant > 0)
                    {

                        salidaDeBarril.id = Nota;
                        salidaDeBarril.barril = item.barril;
                        salidaDeBarril.cant = item.cant;
                        await _dataService.CreateSalidaBarril(salidaDeBarril);
                        await _dataService.updateBarril(salidaDeBarril);
                    }
                }

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


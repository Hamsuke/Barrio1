using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Firebase.CloudMessaging;
using System.Collections.ObjectModel;

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
    [ObservableProperty]
    private int _salidaLlane;
    [ObservableProperty]
    private int _salidaSJ;
    [ObservableProperty]
    private int _salidaMale;
    [ObservableProperty]
    private int _salidaBarra;
    [ObservableProperty]
    private int _salidaTolo;
    [ObservableProperty]
    private int _salidaB21;
    [ObservableProperty]
    private int _salidaGen;
    [ObservableProperty]
    private int _salidaGuasanta;
    [ObservableProperty]
    private int _salidaAguac;

    //Inventario
    [ObservableProperty]
    private int _stockLlane;
    [ObservableProperty]
    private int _stockSJ;
    [ObservableProperty]
    private int _stockMale;
    [ObservableProperty]
    private int _stockBarra;
    [ObservableProperty]
    private int _stockTolo;
    [ObservableProperty]
    private int _stockB21;
    [ObservableProperty]
    private int _stockGen;
    [ObservableProperty]
    private int _stockGuasanta;
    [ObservableProperty]
    private int _stockAguac;

    public AddVentaViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }
    public ObservableCollection<Inventario> Inventario { get; set; } = new();

    [RelayCommand]
    public async Task GetInventario()
    {
        Inventario.Clear();

        var inventario = await _dataService.GetInventario();

        StockLlane = inventario.llane;
        StockSJ = inventario.SJ;
        StockMale = inventario.male;
        StockBarra = inventario.barra;
        StockTolo = inventario.tolo;
        StockB21 = inventario.b21;
        StockGen = inventario.gen;
        StockGuasanta = inventario.guasanta;
        StockAguac = inventario.aguac;

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
                    llane = SalidaLlane,
                    sj = SalidaSJ,
                    male = SalidaMale,
                    barra = SalidaBarra,
                    tolo = SalidaTolo,
                    b21 = SalidaB21,
                    gen = SalidaGen,
                    guasanta = SalidaGuasanta,
                    aguac = SalidaAguac
                };


                //await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
                //var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
                //Console.WriteLine($"FCM token: {token}");

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
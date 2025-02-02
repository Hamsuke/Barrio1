﻿using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Firebase.CloudMessaging;
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

    //Salida Botellas
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
    private int _salidaCeli;


    //InventarioBotellas
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
    private int _stockCeli;

    //Salida Barriles
    [ObservableProperty]
    private int _salidaBaLlane;
    [ObservableProperty]
    private int _salidaBaSJ;
    [ObservableProperty]
    private int _salidaBaMale;
    [ObservableProperty]
    private int _salidaBaBarra;
    [ObservableProperty]
    private int _salidaBaTolo;
    [ObservableProperty]
    private int _salidaBaB21;
    [ObservableProperty]
    private int _salidaBaGen;
    [ObservableProperty]
    private int _salidaBaGuasanta;
    [ObservableProperty]
    private int _salidaBaCeli;

    //InventarioBotellas
    [ObservableProperty]
    private int _stockBaLlane;
    [ObservableProperty]
    private int _stockBaSJ;
    [ObservableProperty]
    private int _stockBaMale;
    [ObservableProperty]
    private int _stockBaBarra;
    [ObservableProperty]
    private int _stockBaTolo;
    [ObservableProperty]
    private int _stockBaB21;
    [ObservableProperty]
    private int _stockBaGen;
    [ObservableProperty]
    private int _stockBaGuasanta;
    [ObservableProperty]
    private int _stockBaCeli;


    public AddVentaViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }
    public ObservableCollection<Botellas> InventarioBo { get; set; } = new();
    public ObservableCollection<Barriles> InventarioBa { get; set; } = new();

    [RelayCommand]
    public async Task GetInventario()
    {
        InventarioBo.Clear();

        var inventario = await _dataService.GetInventario();

        StockLlane = inventario.llane;
        StockSJ = inventario.SJ;
        StockMale = inventario.male;
        StockBarra = inventario.barra;
        StockTolo = inventario.tolo;
        StockB21 = inventario.b21;
        StockGen = inventario.gen;
        StockGuasanta = inventario.guasanta;
        StockCeli = inventario.celi;
    }

    [RelayCommand]
    public async Task GetBarriles()
    {
        InventarioBa.Clear();

        var inventario = await _dataService.GetBarriles();

        StockBaLlane = inventario.llane;
        StockBaSJ = inventario.SJ;
        StockBaMale = inventario.male;
        StockBaBarra = inventario.barra;
        StockBaTolo = inventario.tolo;
        StockBaB21 = inventario.b21;
        StockBaGen = inventario.gen;
        StockBaGuasanta = inventario.guasanta;
        StockBaCeli = inventario.celi;
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
                SalidasBotella salidas = new()
                {
                    id = Nota,
                    llane = SalidaLlane,
                    sj = SalidaSJ,
                    male = SalidaMale,
                    barra = SalidaBarra,
                    tolo = SalidaTolo,
                    b21 = SalidaB21,
                    gen = SalidaGen,
                    guasanta = SalidaGuasanta,
                    celi = SalidaCeli,
                };

                SalidasBarril salidasBa = new()
                {
                    id = Nota,
                    llane = SalidaBaLlane,
                    sj = SalidaBaSJ,
                    male = SalidaBaMale,
                    barra = SalidaBaBarra,
                    tolo = SalidaBaTolo,
                    b21 = SalidaBaB21,
                    gen = SalidaBaGen,
                    guasanta = SalidaBaGuasanta,
                    celi = SalidaBaCeli,
                };

                Botellas ajuste = new()
                {
                    llane = (StockLlane - SalidaLlane),
                    SJ = (StockSJ - SalidaSJ),
                    male = (StockMale - SalidaLlane),
                    barra = (StockBarra - SalidaBarra),
                    tolo = (StockTolo - SalidaTolo),
                    b21 = (StockB21 - SalidaB21),
                    gen = (StockGen - SalidaGen),
                    guasanta = (StockGuasanta - SalidaGuasanta),
                    celi = (StockCeli - SalidaCeli),
                };

                Barriles ajustesBa = new()
                {
                    llane = (StockBaLlane - SalidaBaLlane),
                    SJ = (StockBaSJ - SalidaBaSJ),
                    male = (StockBaMale - SalidaBaLlane),
                    barra = (StockBaBarra - SalidaBaBarra),
                    tolo = (StockBaTolo - SalidaBaTolo),
                    b21 = (StockBaB21 - SalidaBaB21),
                    gen = (StockBaGen - SalidaBaGen),
                    guasanta = (StockBaGuasanta - SalidaBaGuasanta),
                    celi = (StockBaCeli - SalidaBaCeli),
                };


                //await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
                //var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
                //Console.WriteLine($"FCM token: {token}");

                await _dataService.CreateVenta(venta);
                await _dataService.CreateSalida(salidas, salidasBa);
                await _dataService.UpdateAlmacen(ajuste, ajustesBa);

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
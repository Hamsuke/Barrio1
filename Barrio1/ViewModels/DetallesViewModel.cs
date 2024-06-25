using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace Barrio1.ViewModels;

[QueryProperty(nameof(Venta), "VentaObject")]

public partial class DetallesViewModel : ObservableObject
{
    private readonly IDataServices _dataServices;

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

    [ObservableProperty]
    private Ventas _venta;

    public DetallesViewModel(IDataServices dataService)
    {
        _dataServices = dataService;
    }

    [RelayCommand]
    public async Task SalidaBo()
    {
        SalidasBotella botellas = new SalidasBotella();

        botellas = await _dataServices.GetBotellasNota(Venta.id);

        StockLlane = botellas.llane;
        StockSJ = botellas.sj;
        StockMale = botellas.male;
        StockBarra = botellas.barra;
        StockTolo = botellas.tolo;
        StockB21 = botellas.b21;
        StockGen = botellas.gen;
        StockGuasanta = botellas.guasanta;
        StockCeli = botellas.celi;
    }

    [RelayCommand]
    public async Task SalidaBa()
    {
        SalidasBarril barriles = new SalidasBarril();

        barriles = await _dataServices.GetBarrilesNota(Venta.id);

        StockBaLlane = barriles.llane;
        StockBaSJ = barriles.sj;
        StockBaMale = barriles.male;
        StockBaBarra = barriles.barra;
        StockBaTolo = barriles.tolo;
        StockBaB21 = barriles.b21;
        StockBaGen = barriles.gen;
        StockBaGuasanta = barriles.guasanta;
        StockBaCeli = barriles.celi;
    }
}


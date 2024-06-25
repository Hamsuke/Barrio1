using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Barrio1.ViewModels;
public partial class InventarioViewModel : ObservableObject
{
    private readonly IDataServices _dataService;

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
    private int _stockCeli;
    [ObservableProperty]
    private int _stockGuasanta;

    //InventarioBarriles
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
    private int _stockBaCeli;
    [ObservableProperty]
    private int _stockBaGuasanta;


    public InventarioViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    public async Task GetInventario()
    {
        var inventario = await _dataService.GetInventario();

        StockLlane = inventario.llane;
        StockSJ = inventario.SJ;
        StockMale = inventario.male;
        StockBarra = inventario.barra;
        StockTolo = inventario.tolo;
        StockB21 = inventario.b21;
        StockGen = inventario.gen;
        StockCeli = inventario.celi;
        StockGuasanta = inventario.guasanta;
    }

    [RelayCommand]
    public async Task GetBarriles()
    {

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
}
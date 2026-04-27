using Barrio1.Models;
using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Barrio1.ViewModels;
public partial class InventarioViewModel : ObservableObject
{
    private readonly IDataServices _dataService;
    public InventarioViewModel(IDataServices dataService)
    {
        _dataService = dataService;
    }

    public ObservableCollection<Barriles> inventarioBa { get; set; } = new();
    public ObservableCollection<Botellas> inventarioBo { get; set; } = new();

    [RelayCommand]
    public async Task GetInventario()
    {
        inventarioBo.Clear();
        inventarioBa.Clear();
        var stockBo = await _dataService.GetBotellas();
        var stockBa = await _dataService.GetBarriles();

        var stockBarriles = stockBa.ToList();
        var stockBotellas = stockBo.ToList();

        foreach (var item in stockBotellas)
        {
            inventarioBo.Add(item);
        }

        foreach(var item in stockBarriles)
        {
            inventarioBa.Add(item);
        }

    }
}